using UnityEngine;

namespace BerserkPixel.Health
{
    [RequireComponent(typeof(Health))]
    public class HealthParticles : MonoBehaviour
    {
        [SerializeField] private Transform damageFX;
        [SerializeField] private Transform[] dieFXs;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnTakeDamage += DamageParticles;
            _health.OnDie += DieParticles;
        }

        private void OnDisable()
        {
            _health.OnTakeDamage -= DamageParticles;
            _health.OnDie -= DieParticles;
        }

        private void DieParticles()
        {
            foreach (var dieFX in dieFXs)
            {
                Instantiate(dieFX, transform.position, Quaternion.identity);
            }
        }

        private void DamageParticles(int amount, int health)
        {
            Instantiate(damageFX, transform.position, Quaternion.identity);
        }
    }
}