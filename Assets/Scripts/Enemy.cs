using UnityEngine;

public class Enemy : BaseShip
{    
    private void Start()
    {
        Health = 50; // Establece la salud inicial del enemigo
    }

    protected override void Die()
    {
        base.Die();
    }

}
