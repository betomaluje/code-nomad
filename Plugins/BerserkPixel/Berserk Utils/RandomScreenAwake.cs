using UnityEngine;

public class RandomScreenAwake : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float minX = -bounds.x;
        float maxX = bounds.x;

        float minY = -bounds.y;
        float maxY = bounds.y;

        Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        target.position = position;
    }

    private void OnValidate()
    {
        if (target == null)
        {
            target = transform;
        }
    }
}
