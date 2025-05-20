using App.Scripts.Data;
using App.Scripts.Services.View;
using UnityEngine;

namespace App.Scripts.UI
{
    public class InventoryView : BaseView
    {
        [SerializeField] private Transform _transformSlotsParent;

        private PlayerInventory _playerInventory;
        private InventorySlotUI[] _slots;
        private int _activeIndex = 0;

        public void Initialize(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
            
            _slots = new InventorySlotUI[_playerInventory.InventorySize];
            var inventorySlotView = Resources.Load<InventorySlotUI>(DirectoryConstants.InventorySlotUI);
            
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i] = Instantiate(
                    inventorySlotView, 
                    gameObject.transform.position,
                    Quaternion.identity,
                    _transformSlotsParent);
            }

            UpdateActiveSlot(0);
            
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
}
