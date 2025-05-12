using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Vector3 _activeItemScale = new Vector3(1.12f, 1.12f, 1.12f);
    [SerializeField] private Image _itemImage;

    private Vector3 _startScale;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void Activate()
    {
        transform.localScale = _activeItemScale;
    }

    public void Deactivate()
    {
        transform.localScale = _startScale;
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
