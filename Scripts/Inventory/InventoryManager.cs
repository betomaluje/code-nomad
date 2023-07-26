using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int _slotAmount = 5;
    [SerializeField] private RectTransform _slotContainer;
    [SerializeField] private InventorySlot _slotItemPrefab;
    [SerializeField] private Transform _popupPosition;

    private ResourceManager _resourceManager;

    private Dictionary<ResourceType, InventorySlot> _inventory;

    private int _currentSlotAmount;

    private void Awake()
    {
        _resourceManager = ResourceManager.GetInstance();
        _inventory = new Dictionary<ResourceType, InventorySlot>();
    }

    private void OnEnable()
    {
        _resourceManager.OnResourceAdded += OnItemAdded;
        _resourceManager.OnResourceRemoved += OnItemRemoved;
    }

    private void Start()
    {
        for (int i = 0; i < _slotAmount; i++)
        {
            InventorySlot slot = Instantiate(_slotItemPrefab, _slotContainer);
            slot.Setup(ResourceType.None, null, 0);
        }

        List<ResourceItem> savedResources = _resourceManager.GetGatheredResources();

        if (savedResources.Count > 0)
        {
            foreach (var item in savedResources)
            {
                OnItemAdded(item);
            }
        }
    }

    private void OnDisable()
    {
        _resourceManager.OnResourceAdded -= OnItemAdded;
        _resourceManager.OnResourceRemoved -= OnItemRemoved;
    }

    /// Gets the first available slot marked as ResourceType.None
    private InventorySlot GetFirstAvailableSlot(ResourceItem item)
    {
        foreach (Transform child in _slotContainer.transform)
        {
            if (child.TryGetComponent<InventorySlot>(out var slot))
            {
                if (slot.Type == ResourceType.None)
                {
                    return slot;
                }
            }
        }
        return null;
    }

    private void OnItemAdded(ResourceItem item)
    {
        if (_inventory.ContainsKey(item.Type))
        {
            // update amount
            Debug.Log($"Update Item {item.Type} -> {item.Amount}");
            UpdateSlot(item);
        }
        else
        {
            Debug.Log($"New Item {item.Type} -> {item.Amount}");
            // create a new slot
            CreateNewSlot(item);
            _currentSlotAmount++;
        }
        PopupTextSpawner.GetInstance().PopupText($"{item.Type}[{item.Amount}]", _popupPosition.position);
    }

    private void OnItemRemoved(ResourceItem item)
    {
        if (item.Amount > 0)
        {
            InventorySlot slot = _inventory[item.Type];
            slot.UpdateAmount(item.Amount);
            Debug.Log($"Item removed {item.Type} -> {item.Amount}");
        }
        else
        {
            // remove
            foreach (var slot in _inventory)
            {
                if (slot.Key == item.Type)
                {
                    slot.Value.Reset();
                    _currentSlotAmount--;
                    _inventory.Remove(item.Type);
                    Debug.Log($"Item removed {item.Type} -> {item.Amount}");
                    break;
                }
            }
        }
    }

    private void UpdateSlot(ResourceItem item)
    {
        InventorySlot slot = _inventory[item.Type];
        slot.UpdateAmount(item.Amount);
    }

    private void CreateNewSlot(ResourceItem item)
    {
        InventorySlot slot = GetFirstAvailableSlot(item);
        if (slot != null)
        {
            slot.Setup(item.Type, item.Sprite, item.Amount);
            _inventory[item.Type] = slot;
        }
        else
        {
            PopupTextSpawner.GetInstance().PopupText($"No more slot spaces", _popupPosition.position);
        }
    }
}
