using UnityEngine;

public class Player : BaseShip
{
    private int experience;    

    //Creamos la propiedad

    public int Experience
    {
        //Aqui se podria escribir logica adicional al obtener o asignar el valor
        get { return experience; }
        set { 
            int levelBefore = Level; // Guardamos el nivel antes de asignar la experiencia
            experience = value;

            Debug.Log($"¡Ganaste EXP! total: {experience}");

            if (Level > levelBefore) // Verificamos si el nivel ha aumentado
            {
                Debug.Log($"¡Subiste de nivel! Ahora eres nivel {Level}");
            }
        }
    }

    public int Level
    {
        get { return experience / 1000; }
        set { experience = value * 1000; }
    }

    protected override void Die()
    {
        //Aqui agrego la logica de muerte del player.
        Debug.Log("Player has died");
        base.Die();
    }
}
