using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GatherResource : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _detectionRadius = 2;
    [SerializeField] private Transform[] _detectionPositions;

    [Header("Debug")]
    [SerializeField] private Color _debugNormalColor = new Color(.1f, .1f, .1f, .4f);
    [SerializeField] private Color _debugDetectedColor = Color.red;

    public UnityEvent<Collider2D, Vector3> OnTargetDetected;
    public UnityEvent OnTargetDismissed;

    private bool _hasDetectedBefore;

    private Color _debugColor;

    private void OnValidate()
    {
        if(_debugColor == null)
            _debugColor = _debugNormalColor;
    }

    private void Update()
    {
        List<Collider2D> hitColliders = new List<Collider2D>();
        foreach (Transform item in _detectionPositions)
        {
            RaycastHit2D hit = Physics2D.Raycast(item.position, item.up, _detectionRadius, _targetMask);
            if (hit.collider != null && !hitColliders.Contains(hit.collider))
            {
                hitColliders.Add(hit.collider);
            }
        }
        Collider2D collider = null;
        if (hitColliders.Count > 0)
        {
            collider = hitColliders[0];
        }
        if (collider)
        {
            var dir = (collider.transform.position - transform.position).normalized;
            dir.z = 0;

            _debugColor = _debugDetectedColor;

            _hasDetectedBefore = true;
            OnTargetDetected?.Invoke(collider, dir);
        }
        else
        {
            if (_hasDetectedBefore)
            {
                _debugColor = _debugNormalColor;
                _hasDetectedBefore = false;
                OnTargetDismissed?.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_detectionPositions != null && _detectionPositions.Length > 0)
        {
            foreach (Transform item in _detectionPositions)
            {
                Vector2 end = (Vector2)item.position + (Vector2)item.up * _detectionRadius;
                Debug.DrawLine(item.position, end, _debugColor);
            }
        }
    }
}
