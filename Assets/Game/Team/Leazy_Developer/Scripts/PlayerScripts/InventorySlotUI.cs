using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Sprite _activeBackgroundSprite;

    private Sprite _defaultBackgroundSprite;

    private void Start()
    {
        _defaultBackgroundSprite = _backgroundImage.sprite;
    }

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
