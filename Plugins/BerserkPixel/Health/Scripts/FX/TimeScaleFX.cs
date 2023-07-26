using System.Collections;
using UnityEngine;

namespace BerserkPixel.Health.FX
{
    public class TimeScaleFX : MonoBehaviour, IFX
    {
        [Tooltip("Time scale factor to switch to")]
        [SerializeField] private float timeFactor = 0f;

        [Tooltip("The chance of failure this effect has")]
        [SerializeField, Range(0f, 1f)] private float chanceOfFailure = .3f;

        [Tooltip("Duration of the effect")]
        [SerializeField] private float duration = .3f;

        [Tooltip("Duration of the effect")]
        [SerializeField] private float coolDown = 3f;

        private Coroutine _timeScaleRoutine;
        private WaitForSeconds _waitingTime;
        private bool _isBusy;

        private void Awake()
        {
            _waitingTime = new WaitForSeconds(duration);
        }

        public void DoFX(Vector2 direction)
        {
            if (_isBusy) return;

            if (DamageExt.GetChance(1 - chanceOfFailure)) return;

            StartCoroutine(Inmune());

            // If the _timeScaleRoutine is not null, then it is currently running.
            if (_timeScaleRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple TimeScaleRoutine the same time would cause bugs.
                StopCoroutine(_timeScaleRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            _timeScaleRoutine = StartCoroutine(TimeScaleRoutine());
        }

        private IEnumerator TimeScaleRoutine()
        {
            // Swap the time scales
            Time.timeScale = timeFactor;

            // Pause the execution of this function for "duration" seconds.
            yield return _waitingTime;

            // After the pause, swap back to the original time scale.
            Time.timeScale = 1;

            // Set the routine to null, signaling that it's finished.
            _timeScaleRoutine = null;
        }

        private IEnumerator Inmune()
        {
            _isBusy = true;
            yield return new WaitForSeconds(coolDown);
            _isBusy = false;
        }
    }
}