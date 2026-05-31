using UnityEngine;
using UnityEngine.UI;

public class ShipWorldUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
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
        healthBar.value = (float) _myShip.Health / _myShip.MaxHealth;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0); // Para que el UI mire hacia la camara pero no al reves
    }
}
