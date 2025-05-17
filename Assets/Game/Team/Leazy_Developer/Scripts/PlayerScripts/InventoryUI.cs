using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;

    private PlayerInventory _playerInventory;
    private InventorySlotUI[] _slots;
    private int _activeIndex = 0;

    private void Awake()
    {
        _playerInventory = FindFirstObjectByType<PlayerInventory>();

        _slots = new InventorySlotUI[_playerInventory.InventorySize];
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = Instantiate(_slotPrefab, gameObject.transform).GetComponent<InventorySlotUI>();
        }

        UpdateActiveSlot(0);
    }

    private void Start()
    {
        _playerInventory.ActivateItemAction += UpdateActiveSlot;
        _playerInventory.AddItemAction += AddSlot;
        _playerInventory.RemoveActiveItemAction += ClearActiveSlot;
    }

    private void OnDestroy()
    {
        _playerInventory.ActivateItemAction -= UpdateActiveSlot;
        _playerInventory.AddItemAction -= AddSlot;
        _playerInventory.RemoveActiveItemAction -= ClearActiveSlot;
    }

    public void UpdateActiveSlot(int index)
    {
        _slots[_activeIndex].Deactivate();
        _activeIndex = index;
        _slots[_activeIndex].Activate();
    }

    public void AddSlot(int index, Sprite sprite)
    {
        _slots[index].AddItem(sprite);
    }

    public void ClearActiveSlot()
    {
        _slots[_activeIndex].RemoveItem();
    }
}
