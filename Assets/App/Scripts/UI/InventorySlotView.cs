using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class InventorySlotView : BaseView
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Sprite _defaultBackgroundSprite;
        [SerializeField] private Sprite _activeBackgroundSprite;

        public void Activate()
        {
            _backgroundImage.sprite = _activeBackgroundSprite;
        }

        public void Deactivate()
        {
            _backgroundImage.sprite = _defaultBackgroundSprite;
        }

        public void AddItem(Sprite sprite)
        {
            _itemImage.sprite = sprite;
            _itemImage.enabled = true;
        }

        public void RemoveItem()
        {
            _itemImage.enabled = false;
            _itemImage.sprite = null;
        }
    }
}