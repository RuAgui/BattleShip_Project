using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform[] firePoint;

    [Header("Fire Rate Settings")]
    [SerializeField] private float fireRate = 0.25f; // Tiempo entre disparos
    [SerializeField] private float nextFireTime = 0f; // Tiempo para el próximo disparo

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
        if (Time.time < nextFireTime) return; // No disparar si no ha pasado el tiempo de recarga
        nextFireTime = Time.time + fireRate; // Actualizar el tiempo para el próximo disparo

        foreach (Transform point in firePoint)
        {
            GameObject laserGO = Instantiate(laserPrefab, point.position, point.rotation); //UTEEEE!!

            SimpleLaser laserScript = laserGO.GetComponent<SimpleLaser>();
            if (laserScript != null)
            {
                laserScript.ownerShooter = GetComponent<BaseShip>(); // Asignar el propietario del disparo
            }
        } 
    }
}
