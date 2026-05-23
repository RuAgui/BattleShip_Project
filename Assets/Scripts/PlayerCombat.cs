using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private Transform[] firePoint;

    [Header("Fire Rate Settings")]
    [SerializeField] private float fireRate = 0.25f; // Tiempo entre disparos
    [SerializeField] private float nextFireTime = 0f; // Tiempo para el próximo disparo

    private LaserShooter _laserShooter;
    private BaseShip _baseShip;

    void Awake()
    {
        _laserShooter = GetComponent<LaserShooter>();
        _baseShip = GetComponent<BaseShip>();
    }

    public void OnShoot (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SimpleLaserShoot();
            Debug.Log("Disparando");
        }
    }

    public void SimpleLaserShoot()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        foreach (Transform point in firePoint)
            _laserShooter.Shoot(point, _baseShip);
    }
}
