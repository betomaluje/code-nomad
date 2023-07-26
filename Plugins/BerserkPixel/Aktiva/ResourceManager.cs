using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<ResourceType, ResourceItem> _collectedResources;

    public Action<ResourceItem> OnResourceAdded = delegate { };
    public Action<ResourceItem> OnResourceRemoved = delegate { };

    private bool _busyDuplicating;

    private void Awake()
    {
        _collectedResources = new Dictionary<ResourceType, ResourceItem>();
    }

    public List<ResourceItem> GetGatheredResources()
    {
        List<ResourceItem> resources = new List<ResourceItem>();
        foreach (var item in _collectedResources)
        {
            if (item.Value.Amount > 0)
            {
                resources.Add(item.Value);
            }
        }

        return resources;
    }

    public void Add(ResourceType type, int amount, Sprite sprite)
    {
        if (_collectedResources.ContainsKey(type))
        {
            int current = _collectedResources[type].Amount;
            current += amount;

            _collectedResources[type].Amount = current;
        }
        else
        {
            ResourceItem newItem = new ResourceItem();
            newItem.Type = type;
            newItem.Amount = amount;
            newItem.Sprite = sprite;
            _collectedResources.Add(type, newItem);
        }

        OnResourceAdded?.Invoke(_collectedResources[type]);
    }

    public void Remove(ResourceType type, int amount)
    {
        if (_collectedResources.ContainsKey(type))
        {
            int current = _collectedResources[type].Amount;
            current -= amount;

            if (current <= 0)
            {
                // remove
                _collectedResources[type].Amount = 0;
                OnResourceRemoved?.Invoke(_collectedResources[type]);
                _collectedResources.Remove(type);
            }
            else
            {
                _collectedResources[type].Amount = current;
                OnResourceRemoved?.Invoke(_collectedResources[type]);
            }
        }
    }

    public void Duplicate(ResourceCollectable prefab, int amount, Vector2 position)
    {
        if (_busyDuplicating) return;

        ResourceCollectable bigger = Instantiate(prefab, position, Quaternion.identity);
        bigger.Setup(prefab.Type, amount, true);
        bigger.name = $"Bigger {prefab.Type}({amount})";
        StartCoroutine(WaitForBusyDuplicating());
    }

    private IEnumerator WaitForBusyDuplicating()
    {
        _busyDuplicating = true;
        yield return new WaitForSeconds(1);
        _busyDuplicating = false;
    }
}