using System.Collections;
using UnityEngine;

namespace BerserkPixel.Health.FX
{
    public class SkewFX : MonoBehaviour, IFX
    {
        [SerializeField] private Transform _target;
        [Tooltip("Skew factor to switch to")]
        [SerializeField] private float skewFactor = .2f;

        [Tooltip("Duration of the flash.")]
        [SerializeField] private float duration;

        private Vector3 _originalScale;

        // The currently running coroutine.
        private Coroutine _skewRoutine;
        private WaitForSeconds _waitingTime;

        private void OnValidate()
        {
            if (_target == null)
            {
                _target = transform;
            }
        }

        private void Awake()
        {
            _originalScale = _target.localScale;
            _waitingTime = new WaitForSeconds(duration);
        }

        public void DoFX(Vector2 direction)
        {
            // If the _skewRoutine is not null, then it is currently running.
            if (_skewRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple SkewRoutines the same time would cause bugs.
                StopCoroutine(_skewRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            _skewRoutine = StartCoroutine(SkewRoutine());
        }

        private IEnumerator SkewRoutine()
        {
            // Swap to the skewFactor.
            float x = _target.localScale.x + Random.Range(0f, skewFactor);
            float y = _target.localScale.y + Random.Range(0f, skewFactor);
            float z = _target.localScale.z + Random.Range(0f, skewFactor);

            _target.localScale = new Vector3(x, y, z);

            // Pause the execution of this function for "duration" seconds.
            yield return _waitingTime;

            // After the pause, swap back to the original scale.
            _target.localScale = _originalScale;

            // Set the routine to null, signaling that it's finished.
            _skewRoutine = null;
        }
    }
}