using System.Collections;
using UnityEngine;

public class ShineMaterial : MonoBehaviour
{
    [SerializeField] private float _animDuration = .5f;
    private Material _material;

    private int _shinyProperty = Shader.PropertyToID("_ShineLocation");

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;

        StartCoroutine(ChangeNumberTo(0));
    }

    private IEnumerator ChangeNumberTo(float initialTime = 0)
    {
        yield return new WaitForSeconds(initialTime);

        // float interpolation = 1f;

        // _material.SetFloat(_shinyProperty, interpolation);

        // while (interpolation < 0.01f)
        // {
        //     interpolation = Mathf.Lerp(1, 0, _animDuration * Time.deltaTime);
        //     Debug.Log($"Location: {interpolation}");
        //     _material.SetFloat(_shinyProperty, interpolation);
        //     yield return null;
        // }

        // _material.SetFloat(_shinyProperty, 0);
        float interpolation = 1f;

        for (float t = 0f; t < _animDuration; t += Time.deltaTime)
        {
            interpolation = Mathf.Lerp(1, 0, t / _animDuration);
            _material.SetFloat(_shinyProperty, interpolation);
            yield return null;
        }
        _material.SetFloat(_shinyProperty, 0);

        StartCoroutine(ChangeNumberTo(3));
    }
}
