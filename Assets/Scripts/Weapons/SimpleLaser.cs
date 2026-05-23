using System;
using System.Collections;
using UnityEngine;

public class SimpleLaser : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector3 startPosition;

    public BaseShip ownerShooter;
    public float Damage => damage;

    public Action onExpire;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    public void Launch()
    {
        startPosition = transform.position;
        LaserBullet();
    }

    private void LaserBullet()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        StartCoroutine(ExpireAfter(lifeTime));
    }

    private IEnumerator ExpireAfter(float time)
    {
        yield return new WaitForSeconds(time);
        onExpire?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseShip target = other.GetComponent<BaseShip>();

        // 1. Verificamos que chocamos con una nave y que no somos nosotros mismos
        if (target != null && target.gameObject.tag != ownerShooter.gameObject.tag)
        {
            target.Health -= (int)damage;
            Debug.Log("Laser ha chocado con: " + other.gameObject.name);

            // 2. RECOMPENSA: Si la víctima muere por este impacto
            if (target.Health <= 0 && ownerShooter != null)
            {
                // El que disparó recibe la experiencia
                ownerShooter.Experience += 200;
                Debug.Log($"{ownerShooter.name} recibió EXP por destruir a {target.name}");
            }

            StopAllCoroutines();
            onExpire?.Invoke();
        }
    }
}