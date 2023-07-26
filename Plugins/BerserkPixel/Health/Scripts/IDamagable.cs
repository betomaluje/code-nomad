using UnityEngine;

namespace BerserkPixel.Health
{
    interface IDamagable
    {
        /// <summary>
        /// Damages something. Returns true if successful, false otherwise
        /// </summary>
        bool Damage(int amount);

        void Die();
    }
}