using UnityEngine;

public class Enemy : BaseShip
{
    [SerializeField] private int experienceReward = 100; // Experiencia que el jugador recibirá al derrotar a este enemigo
    
    private void Start()
    {
        Health = 50; // Establece la salud inicial del enemigo
    }

    protected override void Die()
    {
        //Buscar jugaodr para otorgar experiencia
        Player player = Object.FindFirstObjectByType<Player>(); // Encuentra al jugador en la escena    

        if (player != null)
        {
            player.Experience += experienceReward; // Otorga experiencia al jugador
            Debug.Log($"Enemigo destruido. Jugador gana {experienceReward} EXP. Total: {player.Experience}");
        }
        // Logica de muerte
        base.Die();
    }

}
