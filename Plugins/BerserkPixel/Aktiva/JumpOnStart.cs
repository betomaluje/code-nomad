using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)] 
[RequireComponent(typeof(Collider2D))]
public class JumpOnStart : MonoBehaviour
{
    [SerializeField] private float _jumpPower = 3f;
    [SerializeField] private float _jumpPositionMultiplier = 1.5f;
    [SerializeField] private float _animDuration = .3f;

    private Rigidbody2D _rb;
    private Collider2D _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void DoJump(Action callback)
    {
        Vector2 dir = UnityEngine.Random.value > .5 ? Vector2.left : Vector2.right;
        _rb.DOJump(_rb.position + dir * _jumpPositionMultiplier, _jumpPower, 1, _animDuration)
            .OnComplete(() =>
            {
                _collider.enabled = true;
                callback?.Invoke();
            });
    }
}
