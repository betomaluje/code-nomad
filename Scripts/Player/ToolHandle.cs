using BerserkPixel.Health;
using BerserkPixel.Sound;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

[DisallowMultipleComponent]
public class ToolHandle : MonoBehaviour
{
    [Tooltip("How much damage or strength we perform on a resource")]
    [SerializeField] private MinMaxCurve _strengthCurve;
    [SerializeField] private GameObject _toolIndicator;

    [Header("Animations")]
    [SerializeField] private PlayerAnimations _playerAnimations;

    private Collider2D _resourceDetected;

    private bool _wasAttacking;
    private bool _isAttackPressed;

    private void Awake()
    {
        _toolIndicator.SetActive(false);
    }

    private void OnAttack(InputValue input)
    {
        _isAttackPressed = input.isPressed;
    }

    private void Update()
    {
        if (_isAttackPressed)
        {
            if (!_wasAttacking)
            {
                _playerAnimations.SetAttack();
                _wasAttacking = true;
            }

            if (_resourceDetected != null)
            {
                PerformUseTool();
            }
        }
        else
        {
            if (_wasAttacking)
            {
                _playerAnimations.StopAttacking();
                _wasAttacking = false;
            }
        }
    }

    private void PerformUseTool()
    {
        // we calculate how much power we use to take the resource down
        var amountToDamage = Mathf.CeilToInt(_strengthCurve.Evaluate(0, Random.value));

        var dir = (_resourceDetected.transform.position - transform.position).normalized;
        dir.z = 0;

        _resourceDetected.DealDamage(amountToDamage, dir,
            onMiss: () => { PopupTextSpawner.GetInstance().PopupText("Miss", _resourceDetected.transform.position); },
            onDamage: () =>
            {
                SoundManager.instance.Play("pickaxe");
                CinemachineCameraShake.GetInstance().ShakeCamera(4, .5f);
                if (_resourceDetected.gameObject.TryGetComponentInChildren<HealthBarAnimator>(out var healthBarAnimator))
                {
                    healthBarAnimator.ShowHealth();
                }
            });
    }

    #region From Editor
    public void DoDetection(Collider2D resource, Vector3 direction)
    {
        _resourceDetected = resource;
        _toolIndicator.transform.position = resource.transform.position;
        _toolIndicator.SetActive(true);
    }

    public void ReleaseDetection()
    {
        _resourceDetected = null;
        _toolIndicator.transform.position = transform.position;
        _toolIndicator.SetActive(false);
    }
    #endregion
}
