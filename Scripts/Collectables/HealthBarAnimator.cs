using DG.Tweening;
using UnityEngine;

public class HealthBarAnimator : MonoBehaviour
{
    [SerializeField] private CanvasGroup _target;
    [SerializeField] private float _animDuration = .2f;

    private void Awake()
    {
        _target.DOFade(0, 0);
    }

    public void ShowHealth()
    {
        _target.DOFade(1, _animDuration);
    }
}
