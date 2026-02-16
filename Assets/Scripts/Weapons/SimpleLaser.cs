using UnityEngine;

public class SimpleLaser : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        LaserBullet();
    }

    private void LaserBullet()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Comprobar con que choca el laser
        Debug.Log("Laser a chocado con: " + other.gameObject.name);

        BaseShip ship = other.GetComponent<BaseShip>();
        if (ship != null)
        {
            ship.Health -= (int)damage;
        }
        Destroy(gameObject);
    }
}
