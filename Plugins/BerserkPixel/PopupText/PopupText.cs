using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] private float animDuration = .25f;
    [SerializeField] private float yDelta = 3;

    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI[] _popupTexts;
    private Canvas _containerCanvas;

    private void Awake()
    {
        _popupTexts = GetComponentsInChildren<TextMeshProUGUI>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _containerCanvas = GetComponentInChildren<Canvas>();

        _containerCanvas.worldCamera = Camera.main;

        _canvasGroup.alpha = 0;
    }

    public void Show(string msg)
    {
        foreach (var popupText in _popupTexts)
        {
            popupText.text = msg;
        }

        transform.DOMoveY(transform.position.y + yDelta, animDuration);
        _canvasGroup.DOFade(1, animDuration).OnComplete(() =>
        {
            _canvasGroup.DOFade(0, animDuration / 2).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });

    }
}
