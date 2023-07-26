using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MenuPage
{
    [SerializeField] private RectTransform _slotContainer;

    public override Transform PageObject { get => transform; }

    private void OnEnable()
    {
        Button firstSelectedButton = _slotContainer.transform.GetChild(0).GetComponent<Button>();
        firstSelectedButton.Select();
    }
}
