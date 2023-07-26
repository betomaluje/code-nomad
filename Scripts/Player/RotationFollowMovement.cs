using UnityEngine;

public class RotationFollowMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void OnValidate()
    {
        if (_target == null)
            _target = transform;
    }

    /// Called from the Movement script in the Editor
    public void HandleMove(Vector2 movement)
    {
        if (movement != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            Rotate(toRotation);
        }
    }

    private void Rotate(Quaternion towards)
    {
        Quaternion current = _target.rotation;

        while (current.z != towards.z)
        {
            _target.rotation = Quaternion.RotateTowards(_target.rotation, towards, 720f * Time.deltaTime);
            current = _target.rotation;
        }

        _target.rotation = towards;
    }
}
