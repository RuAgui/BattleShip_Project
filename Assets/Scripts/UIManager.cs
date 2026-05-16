using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI victoryText;



    // EVENTS //

    private void OnEnable()
    {
        GameManager.OnVictory += ShowVictoryScreen;
        GameManager.OnDefeat += ShowGameOverScreen;
        BaseShip.OnHealthChanged += UpdateHealthUI;
        BaseShip.OnExperienceChanged += UpdateExperienceUI;
    }

    private void OnDisable()
    {
        GameManager.OnVictory -= ShowVictoryScreen;
        GameManager.OnDefeat -= ShowGameOverScreen;
        BaseShip.OnHealthChanged -= UpdateHealthUI;
        BaseShip.OnExperienceChanged -= UpdateExperienceUI;
    }

    private void ShowVictoryScreen()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true);
        }
    }

    private void ShowGameOverScreen()
    {

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    private void UpdateHealthUI(BaseShip ship)
    {
        if (ship is Player player)
        {
            healthBar.value = (float)player.Health / player.MaxHealth;
            levelText.text = $"Level: {player.Level}";
        }
    }

    private void UpdateExperienceUI(BaseShip ship)
    {
        if (ship is Player player)
        {
            expBar.value = (float)(player.Experience % 1000) / 1000; // Barra de experiencia para el nivel actual
        }
    }


}
