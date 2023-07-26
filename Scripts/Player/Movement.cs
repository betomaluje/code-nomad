using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 60f;
    [SerializeField] private bool _snapingMovement = true;

    public UnityEvent<Vector2> OnMove;

    private Vector2 _movement;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue input)
    {
        _movement = input.Get<Vector2>();
        OnMove?.Invoke(_movement);
    }

    private void FixedUpdate()
    {
        if (_snapingMovement)
        {
            _rb.velocity = _movement * _speed * Time.fixedDeltaTime;
        }
        else
        {
            _rb.velocity = Vector2.Lerp(_rb.velocity, _movement * _speed, Time.fixedDeltaTime);
        }

    }
}
