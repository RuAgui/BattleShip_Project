using UnityEngine;

public class UnitAI : MonoBehaviour
{
    public MotherShip targetMotherShip; //Se asigna al nacer

    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float stopDistance = 50f; // Distancia a la que la nave se detendrá para atacar

    void Update()
    {
        if (targetMotherShip == null) return; // Si no hay un objetivo asignado, no hacer nada

        // Calcular la dirección hacia la MotherShip enemiga

        Vector3 direction = (targetMotherShip.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetMotherShip.transform.position);

        if (distance > stopDistance)
        {
            // Mover hacia la MotherShip enemiga
            transform.position += direction * speed * Time.deltaTime;

            // Rotar para mirar hacia la MotherShip enemiga

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
