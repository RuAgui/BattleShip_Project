using UnityEngine;

public class BaseShip : MonoBehaviour
{
    protected int health;

    public int Health
    {
        get { return health; }
        set { health = value;
            if (health <= 0)
            {
                Die();
                Debug.Log($"{gameObject.name} ha sido destruida.");
            }
        }
    }

    protected virtual void Die()
    {
        //Aqui agrego la lociga de muerte comun a todas las naves.(No player)
        Destroy(gameObject);
    }
}
