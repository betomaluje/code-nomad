using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ResourceCollectable : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private int _totalAmount;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _otherResourceMask;
    [SerializeField] private float _animDuration = .3f;
    [SerializeField] private MagnetTowardsObject _magnet;

    [HideInInspector]
    public ResourceType Type => _type;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private JumpOnStart _jumpOnStart;

    private bool _hasBeenBigger;

    private void Awake()
    {
        _jumpOnStart = GetComponent<JumpOnStart>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _jumpOnStart.enabled = false;
        _collider.enabled = false;
        _magnet.enabled = false;

        if (TryGetComponent<AmountDisplay>(out var display))
        {
            display.UpdateAmount(_totalAmount);
        }
    }

    public void Setup(ResourceType type, int amount, bool isBiggerResource = false)
    {
        _type = type;
        _totalAmount = amount;

        if (!isBiggerResource)
        {
            DoJump();
        }
        else
        {
            _collider.enabled = false;
            _magnet.enabled = false;
            StartCoroutine(EnableComponents());
        }

        if (TryGetComponent<AmountDisplay>(out var display))
        {
            display.UpdateAmount(_totalAmount);
        }
    }

    private IEnumerator EnableComponents()
    {
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
        _magnet.enabled = true;
    }

    private void DoJump()
    {
        _jumpOnStart.enabled = true;
        _jumpOnStart.DoJump(() => _magnet.enabled = true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_targetMask.LayerMatchesObject(other.gameObject))
        {
            ResourceManager.GetInstance().Add(_type, _totalAmount, _spriteRenderer.sprite);

            // TODO: only destroy if there's enough space on the Inventory

            Destroy(gameObject);
        }
        else if (_otherResourceMask.LayerMatchesObject(other.gameObject))
        {
            if (other.TryGetComponent<ResourceCollectable>(out var otherCollectable) && otherCollectable.Type == _type)
            {
                _collider.enabled = false;
                _magnet.enabled = false;

                int amountA = _totalAmount;
                int amountB = otherCollectable._totalAmount;
                int addedAmount = amountA + amountB;

                transform.DOMove(other.transform.position, _animDuration).OnComplete(() =>
                            {
                                Destroy(other.gameObject);
                                Destroy(gameObject);

                                ResourceManager.GetInstance().Duplicate(this, addedAmount, transform.position);
                            });
            }
        }
    }
}
