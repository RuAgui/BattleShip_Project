using Unity.VisualScripting;
using UnityEngine;

public class UnitAI : MonoBehaviour
{

    //IA STATES
    public enum State { Traveling, Attacking, Retreat }
    public State currentState = State.Traveling;


    public MotherShip targetMotherShip; //Se asigna al nacer
    public MotherShip myMotherShip;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float bankingAmount = 30f; // Cuanto se inclina la nave al girar

    [Header("Sensors and Radar Settings")]
    [SerializeField] private float detectionRange = 60f;
    [SerializeField] private float shootRange = 40f;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime;

    [Header("Armament Settings")]

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform[] firePoints;

    [Header("Survival Settings")]

    [SerializeField] private int retreatHealthThreshold = 20; // Si la salud baja de este valor, la nave intentará retirarse
    private BaseShip myShipStats;
    private bool isRetreating = false;

    [Header("Anti-Choques (Flocking)")]
    [SerializeField] private float separationRadius = 15f;
    [SerializeField] private float separationWeight = 10f; //Multiplicador de desvío

    private Rigidbody rb;
    private BaseShip currentTarget; // Puede ser la MotherShip enemiga o una nave enemiga detectada
    private Vector3 flightDirection; // Direccion de vuelo de la nave.
    private float currentRoll; // Para el efecto de inclinacion al girar.

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivamos la gravedad para que la nave pueda volar libremente
        rb.linearDamping = 1.5f; // Damping para evitar que la nave se descontrole a altas velocidades
        rb.angularDamping = 2f; // Damping para controlar la rotación y evitar giros bruscos

        //Cada nave elige su camino

        flightDirection = Random.insideUnitSphere * 40f; // Direccion aleatoria dentro de un circulo de radio X unidades (40)

        //Probabilidad de que la nave huya
        isRetreating = Random.value > 0.6f;
        myShipStats = GetComponent<BaseShip>();
    }

    void Update()
    {
        //Check de supervivencia
       if (isRetreating && myShipStats != null && myShipStats.Health <= retreatHealthThreshold)
        {
            currentState = State.Retreat;
        }
        else
        {
            //Sino puede huir (o tiene vida) ataca o viaja segun el estado
            if (currentTarget == null) FindNearbyEnemy();

            //Si el enemigo muere, currentTarget se vuelve null y la nave continua.

            if (currentTarget != null) currentState = State.Attacking;
            else if (targetMotherShip != null) currentState = State.Traveling;
        }

        Transform targetToShoot = null;
        if (currentState == State.Attacking && currentTarget != null) targetToShoot = currentTarget.transform;
        else if (currentState == State.Traveling && targetMotherShip != null) targetToShoot = targetMotherShip.transform;

        if (targetToShoot != null && currentState != State.Retreat)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetToShoot.position);
            Vector3 dirToTarget = (targetToShoot.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

            // Si disparamos a la Nodriza, empezamos a disparar desde más lejos porque es enorme
            float currentShootRange = (currentState == State.Traveling) ? shootRange * 1.5f : shootRange;

            if (distanceToTarget <= currentShootRange && angleToTarget < 30f)
            {
                TryShoot();
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = Vector3.zero;
        bool hasDestination = false;

        if (currentState == State.Retreat && myMotherShip != null)
        {
            targetPosition = myMotherShip.transform.position;
            hasDestination = true;
        }
        else if (currentState == State.Attacking && currentTarget != null)
        {
            targetPosition = currentTarget.transform.position;
            hasDestination = true;
        }
        else if (currentState == State.Traveling && targetMotherShip != null)
        {
            targetPosition = targetMotherShip.transform.position;
            hasDestination = true;
        }

        if (hasDestination)
        {
            ExecuteFlightManeuvers(targetPosition);
        }
               
    }

    void ExecuteFlightManeuvers(Vector3 destination)
    {
        Vector3 directionToTarget = (destination - transform.position).normalized;
        float distanceToDest = Vector3.Distance(transform.position, destination);

        float breakDistance = (currentState == State.Attacking) ? 15f : 50f;

        if (currentState != State.Retreat && distanceToDest < breakDistance)
        {
            // Vector para apartarnos del objetivo (esquiva)
            Vector3 evasionDirection = (transform.position - destination).normalized;
            // Mezclamos seguir adelante con apartarnos
            directionToTarget = (transform.forward + evasionDirection).normalized;
        }

        // Rotamos hacia el objetivo
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        //Calculamos los grados de diferencia entre adónde miramos y adónde queremos ir
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        float angleDifference = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
        currentRoll = Mathf.Lerp(currentRoll, -angleDifference * (bankingAmount / 45f), Time.fixedDeltaTime * turnSpeed);

        Quaternion finalRot = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, currentRoll);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, finalRot, Time.fixedDeltaTime * turnSpeed));

        // Acelerador inteligente.
        float currentSpeed = (currentState == State.Retreat) ? speed * 1.5f : speed;

        // Si tenemos que dar una curva muy cerrada (el objetivo está a más de 45 grados de nuestra vista),
        // levantamos el pie del acelerador para que el giro sea rápido y cerrado.
        if (angleToTarget > 45f && currentState != State.Retreat)
        {
            currentSpeed *= 0.2f; // Reducimos la velocidad al 20% para girar rápido sin alejarnos
        }

        rb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        // Anti-Apegotonamiento
        Vector3 physicalRepulsion = Vector3.zero;
        Collider[] neighbors = Physics.OverlapSphere(transform.position, separationRadius);

        foreach (var neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject && neighbor.GetComponent<BaseShip>() != null)
            {
                Vector3 awayFromNeighbor = transform.position - neighbor.transform.position;
                float dist = awayFromNeighbor.magnitude;

                if (dist < separationRadius && dist > 0.1f)
                {
                    physicalRepulsion += awayFromNeighbor.normalized * (separationRadius - dist);
                }
            }
        }

        if (physicalRepulsion != Vector3.zero)
        {
            physicalRepulsion = Vector3.ClampMagnitude(physicalRepulsion, 20f);
            rb.AddForce(physicalRepulsion * separationWeight, ForceMode.Acceleration);
        }
    }

    private void TryShoot()
    {
        if (Time.time > nextFireTime && laserPrefab != null)
        {
            nextFireTime = Time.time + fireRate;

            foreach (Transform firePoint in firePoints)
            {
                GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

                //Asigno dueño para que esta nave gane exp y suba de nivel

                SimpleLaser script = laser.GetComponent<SimpleLaser>();
                if (script != null) script.ownerShooter = GetComponent<BaseShip>();
            }    
        }
    }

    void FindNearbyEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        float closestDistance = Mathf.Infinity;
        BaseShip closestEnemy = null;

        foreach (var hit in hits)
        {
            BaseShip ship = hit.GetComponent<BaseShip>();

            // Buscamos la nave enemiga MÁS CERCANA
            if (ship != null && ship.gameObject.tag != gameObject.tag)
            {
                float distance = Vector3.Distance(transform.position, ship.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = ship;
                }
            }
        }

        currentTarget = closestEnemy;
    }
        
}
