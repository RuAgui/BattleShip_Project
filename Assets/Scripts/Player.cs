using UnityEngine;

public class Player : BaseShip
{
    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        Debug.Log($"Player a subido de nivel a: {Level}");
    }

    protected override void Die()
    {
        //Aqui agrego la logica de muerte del player.
        Debug.Log("Player has died");
        base.Die();
    }
}
