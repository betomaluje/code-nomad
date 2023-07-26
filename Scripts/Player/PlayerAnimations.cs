using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Tooltip("Time to pass during cross fade animations")]
    [SerializeField] private float crossFadeDuration = .1f;

    private readonly int _animIdle = Animator.StringToHash("Player_Idle");
    private readonly int _animAttack = Animator.StringToHash("Player_Attack");

    private readonly int _horizontalMovement = Animator.StringToHash("horizontalMovement");
    private readonly int _isMovingHash = Animator.StringToHash("isMoving");
    private readonly int _isAttackingHash = Animator.StringToHash("isAttacking");


    private int _lastUsedAnimation;
    private bool _isAttacking;

    private void Awake()
    {
        _lastUsedAnimation = _animIdle;
        SetWalk(1);
    }

    public void HandleMoveX(Vector2 move)
    {
        bool isIdle = move.x == 0 && move.y == 0;

        _animator.SetBool(_isMovingHash, !isIdle);

        if (move.x != 0)
        {
            SetWalk(move.x);
        }
    }

    public void SetWalk(float speed)
    {
        if (!_isAttacking)
        {
            _animator.SetFloat(_horizontalMovement, speed);
        }
    }

    public void SetAttack()
    {
        _isAttacking = true;
        _animator.SetBool(_isAttackingHash, _isAttacking);
    }

    public void StopAttacking()
    {
        _isAttacking = false;
        _animator.SetBool(_isAttackingHash, _isAttacking);
    }

    public void PlayCrossfaded(int animation)
    {
        if (_lastUsedAnimation != animation)
        {
            _lastUsedAnimation = animation;
            _animator.CrossFadeInFixedTime(animation, crossFadeDuration);
        }
    }
}
