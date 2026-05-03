using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player myPlayer;

    [Header("MOTHER SHIP SETTINGS")]

    [SerializeField] private MotherShip motherShipAllied;
    [SerializeField] private MotherShip motherShipEnemy;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        MotherShip.OnMotherShipDestroy += CheckIfDestroy;
        Player.OnPlayerDeath += CheckIfPlayerDeath;
    }

    private void OnDisable()
    {
        MotherShip.OnMotherShipDestroy -= CheckIfDestroy;
        Player.OnPlayerDeath -= CheckIfPlayerDeath;
    }

    private void CheckIfDestroy(MotherShip destroyedShip)
    {
        if (destroyedShip == motherShipAllied)
        {
            Debug.Log("Has perdido, tu nave nodriza ha sido destruida.");
        }
        else if (destroyedShip == motherShipEnemy)
        {
            Debug.Log("Has ganado, la nave nodriza enemiga ha sido destruida.");
        }
    }

    private void CheckIfPlayerDeath (Player player)
    {
        if (player == myPlayer)
        {
            Debug.Log("Has perdido, tu nave ha sido destruida.");
        }
    }
}