using UnityEngine;

public class MagnetTowardsObject : MonoBehaviour
{    
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Transform _detectionPosition;
    [SerializeField] private float _detectionRadius = 2;
    [SerializeField] private float _speed = 5.0f;

    [Header("Debug")]
    [SerializeField] private Color _debugColor = new Color(.1f, .1f, .1f, .4f);

    private void OnValidate()
    {
        if (_detectionPosition == null)
        {
            _detectionPosition = transform;
        }
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(_detectionPosition.position, _detectionRadius, _targetMask);
        if (collider)
        {
            var dir = (collider.transform.position - transform.position).normalized;
            dir.z = 0;

            float step = _speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, collider.transform.position, step);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _debugColor;
        Gizmos.DrawWireSphere(_detectionPosition.position, _detectionRadius);
    }
}
