using System;
using System.Collections;
using UnityEngine;

namespace BerserkPixel.Health
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private float inmuneTime = 1f;

        public Action<int, int> OnTakeDamage;
        public Action OnDie;
        public Action<int> OnSetupHealth;

        private bool _isDead => _health == 0;

        private int _health;
        private WaitForSeconds _waitingTime;
        private bool _isInmune;

        private void Awake()
        {
            _health = _maxHealth;
            _waitingTime = new WaitForSeconds(inmuneTime);
        }

        private void Start()
        {
            OnSetupHealth?.Invoke(_health);
        }

        public void Setup(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;

            OnSetupHealth?.Invoke(_health);
        }

        public void Heal(int amount)
        {
            _health = Mathf.Min(_health + amount, _maxHealth);
            OnTakeDamage?.Invoke(0, _health);
        }

        public bool Damage(int amount)
        {
            if (_isDead || _isInmune) { return false; }

            _health = Mathf.Max(_health - amount, 0);

            OnTakeDamage?.Invoke(amount, _health);

            StartCoroutine(Inmune());

            if (_isDead)
            {
                Die();
            }

            return true;
        }

        public void Die()
        {
            _isInmune = true;
            OnDie?.Invoke();
        }

        private IEnumerator Inmune()
        {
            _isInmune = true;
            yield return _waitingTime;
            _isInmune = false;
        }
    }
}