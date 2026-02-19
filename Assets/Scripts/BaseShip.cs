using UnityEngine;

public class BaseShip : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int experience;

    public int Level => (experience / 1000) + 1;

    public int Health
    {
        get { return health; }
        set { health = value;

            Debug.Log($"{gameObject.name} ahora tiene: {health} de vida"); // Log para verificar el cambio de vida

            if (health <= 0)
            {
                Die();
                Debug.Log($"{gameObject.name} ha sido destruida.");
            }
        }
    }

    public int Experience
    {
        get => experience;
        set
        {
            int levelBefore = Level;
            experience = value;
            if (Level > levelBefore) OnLevelUp();
        }
    }

    protected virtual void OnLevelUp()
    {
        Debug.Log($"{gameObject.name} ha subido de nivel a: {Level}");
    }

    protected virtual void Die()
    {
        //Aqui agrego la lociga de muerte comun a todas las naves.(No player)
        Destroy(gameObject);
    }
}
