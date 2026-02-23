using UnityEngine;

public class HealingAura : MonoBehaviour
{

    [Header("Healing Aura Settings")]
    [SerializeField] private float healRadius = 10f;
    [SerializeField] private int healAmount = 5;
    [SerializeField] private float healCooldown = 1f;
    [SerializeField] private float timeBetweenHeals;
    [SerializeField] private string factionTag; //Tag para identificar a las naves amigas


    private void Update()
    {
        //Sumar el tiempo que ha pasado desde el ultimo pulso de curación
        timeBetweenHeals += Time.deltaTime;

        //Si el tiempo entre curaciones es mayor o igual al cooldown, se activa el pulso de curación y se resetea el tiempo
        if (timeBetweenHeals >= healCooldown)
        {
            HealingPulse();
            timeBetweenHeals = 0f;
        }
    }

    private void HealingPulse()
    {
        //Se crea el array para que guarde las naves dentro del rango de curación
        Collider[] shipsInRange = Physics.OverlapSphere(transform.position, healRadius);

        //Ahora hay que crear el bucle para recorrer el array y curar a las naves que estén dentro del rango

        foreach (Collider ship in shipsInRange)
        {
            if (ship.CompareTag(factionTag))
            {
               BaseShip shipToHeal = ship.GetComponent<BaseShip>();

                if (shipToHeal != null && shipToHeal.Health > 0) 
                {

                    //Suma esto, pero no permitas que el resultado se salga de este rango (entre 0 y MaxHealth)
                    shipToHeal.Health = Mathf.Clamp(shipToHeal.Health + healAmount, 0, shipToHeal.MaxHealth);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, healRadius);
    }
}
