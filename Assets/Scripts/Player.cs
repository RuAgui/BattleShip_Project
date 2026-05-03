using UnityEngine;
using UnityEngine.Events;

public class Player : BaseShip
{

    public static event UnityAction <Player> OnPlayerDeath;

    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        Debug.Log($"Player a subido de nivel a: {Level}");
    }

    protected override void Die()
    {
        OnPlayerDeath?.Invoke(this); // Disparo el evento de muerte del jugador
        base.Die();
    }
}