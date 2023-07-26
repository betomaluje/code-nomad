using UnityEngine;
using UnityEngine.UI;

namespace BerserkPixel.Health
{
    [RequireComponent(typeof(Health))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private Health _health;

        private int _maxHealth;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnSetupHealth += HandleHealthSetup;
            _health.OnTakeDamage += HandleTakeDamage;
        }

        private void OnDisable()
        {
            _health.OnSetupHealth -= HandleHealthSetup;
            _health.OnTakeDamage -= HandleTakeDamage;
        }

        private void HandleHealthSetup(int health)
        {
            _maxHealth = health;
            slider.value = GetHealthPercentage(_maxHealth);
        }

        private void HandleTakeDamage(int amount, int health)
        {
            slider.value = GetHealthPercentage(health);
        }

        private float GetHealthPercentage(int currentHealth) => (float)currentHealth / _maxHealth;
    }
}