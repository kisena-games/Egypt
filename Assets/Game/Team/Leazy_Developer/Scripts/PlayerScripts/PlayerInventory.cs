using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Action<int> ActivateItemAction;
    public Action<int, Sprite> AddItemAction;
    public Action RemoveActiveItemAction;

    [SerializeField] private int _inventorySize = 6;

    public int InventorySize => _inventorySize;

    private PuzzleSO[] _items;
    private int _activeIndex = 0;

    private void Awake()
    {
        _items = new PuzzleSO[_inventorySize];
    }

    private void Start()
    {
        MoveActiveItem(0);
    }

    private void Update()
    {
        UpdateActiveItem();
    }

    private void UpdateActiveItem()
    {
        float scrollDelta = Input.mouseScrollDelta.y;

        if (scrollDelta == 1f)
        {
            MoveActiveItem(_activeIndex + 1);
        }
        else if (scrollDelta == -1f)
        {
            MoveActiveItem(_activeIndex - 1);
        }
    }

    private void MoveActiveItem(int index)
    {
        _activeIndex = (index + _items.Length) % _items.Length;
        ActivateItemAction?.Invoke(_activeIndex);
    }

    public bool Add(PuzzleSO puzzleSO)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = puzzleSO;
                AddItemAction?.Invoke(i, puzzleSO.image);
                return true;
            }
        }

        return false;
    }

    public PuzzleSO GetActiveItem()
    {
        return _items[_activeIndex];
    }

    public void RemoveActiveItem()
    {
        if (_activeIndex >= 0 && _activeIndex < _items.Length)
        {
            _items[_activeIndex] = null;

            RemoveActiveItemAction?.Invoke();
        }
    }
}
