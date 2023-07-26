using System;
using BerserkPixel.Health.FX;
using UnityEngine;

namespace BerserkPixel.Health
{
    public static class DamageExt
    {
        public static void DealDamage(
            this Collider2D other,
            int damage,
            Vector3 direction,
            Action onMiss,
            Action onDamage,
            float missChance = 0.0f,
            bool onlyPerformFxOnHit = true
        )
        {
            DealDamage(
                other.gameObject,
                damage,
                direction,
                onMiss,
                onDamage,
                missChance,
                onlyPerformFxOnHit
            );
        }

        public static void DealDamage(
            this GameObject other,
            int damage,
            Vector3 direction,
            Action onMiss,
            Action onDamage,
            float missChance = 0.0f,
            bool onlyPerformFxOnHit = true
        )
        {
            // if our chance is lower, we miss
            if (GetChance(missChance))
            {
                // attack miss!
                onMiss?.Invoke();

                return;
            }

            direction = direction.normalized;

            IDamagable damagable = other.GetComponentInChildren<IDamagable>();
            if (damagable != null)
            {
                bool successfulHit = damagable.Damage(damage);
                if (successfulHit)
                {
                    onDamage?.Invoke();
                }

                if (onlyPerformFxOnHit && successfulHit)
                {
                    PerformFx(other, direction);
                }
                else if (!onlyPerformFxOnHit)
                {
                    PerformFx(other, direction);
                }
            }
        }

        private static void PerformFx(GameObject other, Vector3 direction)
        {
            // in the end we play all the FX's
            var allFx = other.GetComponentsInChildren<IFX>();

            if (allFx != null)
            {
                foreach (var fx in allFx)
                {
                    fx.DoFX(direction);
                }
            }
        }

        public static bool GetChance(float probability)
        {
            return UnityEngine.Random.value < probability;
        }
    }
}