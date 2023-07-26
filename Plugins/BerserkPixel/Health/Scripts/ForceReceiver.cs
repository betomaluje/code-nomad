using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BerserkPixel.Health
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ForceReceiver : MonoBehaviour
    {
        [SerializeField] private float knockbackForce = 5f;
        [SerializeField] private float delay = .15f;

        public UnityEvent OnBegin;
        public UnityEvent OnDone;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private IEnumerator Reset()
        {
            yield return new WaitForSeconds(delay);
            _rb.velocity = Vector2.zero;
            OnDone?.Invoke();
        }

        public void Knockback(Vector2 dir)
        {
            OnBegin?.Invoke();
            StopAllCoroutines();
            _rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(Reset());
        }
    }
}