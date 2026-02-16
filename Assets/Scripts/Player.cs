using UnityEngine;

public class Player : BaseShip
{
    private int experience;    

    //Creamos la propiedad

    public int Experience
    {
        //Aqui se podria escribir logica adicional al obtener o asignar el valor
        get { return experience; }
        set { experience = value; }
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
