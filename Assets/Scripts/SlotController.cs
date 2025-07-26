using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Item item;
    public GameObject slot;
    public TextMeshProUGUI quantityObject;

    void Start()
    {
        SetDataSlot(item?.Clone());
        SetQuantity(item?.quantity ?? 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Item mouseItem = InventoryManager.Instance.GetItemDropped();

        if (item != null && mouseItem != null && mouseItem.NameItem != item.NameItem)
        {
            Debug.Log("1");
            InventoryManager.Instance.SetDraggedItem(item.Clone());
            SetDataSlot(mouseItem.Clone());
            SetQuantity(mouseItem.quantity);
        }
        else if (item != null && mouseItem != null && mouseItem.NameItem == item.NameItem && mouseItem.quantity > 1)
        {
            Debug.Log("2");
            item.quantity++;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SubtractQuantity();
        }
        else if (item != null && mouseItem != null && mouseItem.NameItem == item.NameItem && mouseItem.quantity == 1)
        {
            Debug.Log("3");
            item.quantity++;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SubtractQuantity();
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item == null && mouseItem != null)
        {
            Debug.Log("4");
            SetDataSlot(mouseItem.Clone());
            SetQuantity(mouseItem.quantity);
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && mouseItem == null)
        {
            Debug.Log("5");
            InventoryManager.Instance.SetDraggedItem(item.Clone());
            SetDataSlot(null);
            SetQuantity(0);
            item = null;
        }
    }

    public void SetDataSlot(Item newItem)
    {
        Image image = slot.transform.GetComponent<Image>();
        item = newItem;

        if (newItem == null)
        {
            image.color = new Color(0, 0, 0, 0);
            return;
        }

        image.color = Color.white;
        image.sprite = newItem.Icon;
    }

    public void SetQuantity(int quantity)
    {
        quantityObject.text = quantity > 0 ? quantity.ToString() : "";
    }
}
