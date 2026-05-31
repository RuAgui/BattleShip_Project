using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipWorldUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    private BaseShip _myShip;

    private void Awake()
    {
        _myShip = GetComponentInParent<BaseShip>();
    }

    private void OnEnable()
    {
        BaseShip.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        BaseShip.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(BaseShip ship)
    {
        if (ship != _myShip) return;
        float healthPercent = (float)ship.Health / ship.MaxHealth;
        healthBar.fillAmount = healthPercent;
        healthText.text = $"{ship.Health} / {ship.MaxHealth}";
    }
}
