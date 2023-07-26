using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _amountContainer;
    [SerializeField] private PopupText _popupPrefab;

    public ResourceType Type => _type;

    private Sprite _originalSprite;
    private ResourceType _type;
    private int _amount;

    private void Awake()
    {
        _originalSprite = GetComponent<Image>().sprite;
    }

    public void Setup(ResourceType type, Sprite icon, int amount)
    {
        _type = type;
        if (icon != null)
        {
            _image.sprite = icon;
            _image.enabled = true;
        }

        SetAmount(amount);
    }

    public void UpdateAmount(int amount)
    {
        SetAmount(amount);
    }

    public void Reset()
    {
        _image.sprite = _originalSprite;
        SetAmount(0);
        _type = ResourceType.None;
    }

    private void SetAmount(int value)
    {
        _amount = value;

        bool hasItem = _amount > 0;

        string amountText = hasItem ? $"{_amount}" : "";
        _amountText.text = amountText;
        _amountContainer.SetActive(hasItem);
        _image.enabled = hasItem;
    }

    public void Click_Use()
    {
        ResourceManager.GetInstance().Remove(_type, 1);

        PopupTextSpawner.GetInstance().PopupText(_popupPrefab, "-1", Camera.main.ScreenToWorldPoint(transform.position));
    }
}
