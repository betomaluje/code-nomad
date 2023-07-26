using UnityEngine;

namespace BerserkPixel.Health.FX
{
    public class KnockbackFX : MonoBehaviour, IFX
    {
        [Header("Knockback")]
        [SerializeField] private ForceReceiver forceReceiver;

        public void DoFX(Vector2 direction)
        {
            forceReceiver.Knockback(direction);
        }
    }
}