using UnityEngine;

public class BaseShip : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int experience;

    public int Level => (experience / 1000) + 1;

    public int MaxHealth => 100 + (Level - 1) * 20; // Ejemplo de fórmula para salud máxima basada en el nivel

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Die();
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