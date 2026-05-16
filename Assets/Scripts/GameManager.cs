using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player myPlayer;

    public static event UnityAction OnVictory;
    public static event UnityAction OnDefeat;

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
            OnDefeat?.Invoke();
        }
        else if (destroyedShip == motherShipEnemy)
        {
            OnVictory?.Invoke();
        }
    }

    private void CheckIfPlayerDeath (Player player)
    {
        if (player == myPlayer)
        {
            OnDefeat?.Invoke();
        }
    }
}