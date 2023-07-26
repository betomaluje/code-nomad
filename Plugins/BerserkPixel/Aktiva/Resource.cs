using BerserkPixel.Health;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private MinMaxCurve _resourceHealthCurve;
    [SerializeField] private MinMaxCurve _amountCurve;
    [SerializeField] private ResourceCollectable _resourceToCollect;
    [Space]
    [Header("Extra item")]
    [SerializeField, Range(0, 1)] private float _extraItemChance;
    [SerializeField] private ResourceCollectable _extraItem;

    [HideInInspector]
    public int Amount => _amountToCollect;

    private Health _health;
    private int _amountToCollect;

    private void Awake()
    {
        _amountToCollect = Mathf.CeilToInt(_amountCurve.Evaluate(0, Random.value));

        if (this.TryGetComponentInChildren<Health>(out _health))
        {
            var healthAmount = Mathf.CeilToInt(_resourceHealthCurve.Evaluate(0, Random.value));
            _health.Setup(healthAmount);
        }
    }

    private void OnEnable()
    {
        _health.OnDie += HandleDone;
    }

    private void OnDisable()
    {
        _health.OnDie -= HandleDone;
    }

    private void HandleDone()
    {
        var resource = Instantiate(_resourceToCollect, transform.position, Quaternion.identity);
        resource.Setup(_type, _amountToCollect);

        if (_extraItemChance.GetChance())
        {
            Instantiate(_extraItem, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
