using System.Collections;
using UnityEngine;

namespace BerserkPixel.Health.FX
{
    public class FlashFX : MonoBehaviour, IFX
    {
        [Tooltip("Material to switch to during the flash.")]
        [SerializeField] private Material flashMaterial;
        [SerializeField] private Renderer[] rend;

        [Tooltip("Duration of the flash.")]
        [SerializeField] private float duration;
        [SerializeField] private int numberOfFlashes = 1;

        // The material that was in use, when the script started.
        private Material[] _originalMaterial;

        // The currently running coroutine.
        private Coroutine _flashRoutine;

        private void Start()
        {
            _originalMaterial = new Material[rend.Length];
            for (int i = 0; i < rend.Length; i++)
            {
                _originalMaterial[i] = rend[i].material;
            }
        }

        public void DoFX(Vector2 direction)
        {
            // If the flashRoutine is not null, then it is currently running.
            if (_flashRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(_flashRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            _flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            float durationPerFlash = duration / numberOfFlashes;
            // we divide by 2 since we need to turn to flash and back to original with a pause
            WaitForSeconds waitingTime = new WaitForSeconds(durationPerFlash / 2);

            for (int i = 0; i < numberOfFlashes; i++)
            {
                // Swap to the flashMaterial.
                SetFlashMaterials();

                yield return waitingTime;

                // After the pause, swap back to the original material.
                SetOriginalMaterials();

                // so we show the original material for the same amount of time
                yield return waitingTime;
            }

            SetOriginalMaterials();

            // Set the routine to null, signaling that it's finished.
            _flashRoutine = null;
        }

        private void SetOriginalMaterials()
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material = _originalMaterial[i];
            }
        }

        private void SetFlashMaterials()
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material = flashMaterial;
            }
        }
    }
}