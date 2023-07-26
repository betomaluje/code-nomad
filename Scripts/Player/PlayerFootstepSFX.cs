using BerserkPixel.Sound;
using UnityEngine;

public class PlayerFootstepSFX : MonoBehaviour
{
    [SerializeField] private Vector2 _pitchRange;
    [SerializeField] private ParticleSystem _smokeParticles;

    /// Called from the animation trigger event
    public void PlayFootstep()
    {
        float pitchLevel = Random.Range(_pitchRange.x, _pitchRange.y);
        SoundManager.instance.PlayWithPitch("footstep", pitchLevel);

        _smokeParticles.Play();
    }
}
