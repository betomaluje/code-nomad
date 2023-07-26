using System.Collections;
using UnityEngine;

public class PopupTextSpawner : Singleton<PopupTextSpawner>
{
    [SerializeField] private PopupText prefab;
    [SerializeField] private float timeBuffer = .75f;

    private WaitForSeconds _timeBetweenPopups;
    private bool _canPopup = true;

    private void Awake()
    {
        _timeBetweenPopups = new WaitForSeconds(timeBuffer);
    }

    public void PopupTextScreenPosition(string msg, float xOffset, float yOffset)
    {
        Vector2 screenPos = new Vector2(Screen.width + xOffset, Screen.height + yOffset);

        PopupText(msg, screenPos);
    }

    public void PopupText(string msg, Vector2 position)
    {
        PopupText(prefab, msg, position);
    }

    public void PopupText(PopupText newPrefab, string msg, Vector2 position)
    {
        if (!_canPopup) return;

        _canPopup = false;

        var popup = Instantiate(newPrefab, position, Quaternion.identity);
        popup.Show(msg);

        StartCoroutine(DoBuffer());
    }

    private IEnumerator DoBuffer()
    {
        yield return _timeBetweenPopups;
        _canPopup = true;
    }
}
