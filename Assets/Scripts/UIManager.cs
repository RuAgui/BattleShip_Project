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
        // Aquí puedes agregar lógica para actualizar la interfaz de usuario de salud, como barras de salud o indicadores.
    }

    private void ShowGameOverScreen()
    {
        // Aquí puedes agregar lógica para actualizar la interfaz de usuario de experiencia, como barras de experiencia o indicadores.
    }

    private void UpdateHealthUI(BaseShip health)
    {
        // Aquí puedes agregar lógica para actualizar la interfaz de usuario de salud, como barras de salud o indicadores.
    }

    private void UpdateExperienceUI(BaseShip experience)
    {
        // Aquí puedes agregar lógica para actualizar la interfaz de usuario de experiencia, como barras de experiencia o indicadores.
    }


}
