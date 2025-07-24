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
        SetDataSlot(item);
        Debug.Log(item.quantity);
        SetQuantity(item.quantity);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && InventoryManager.Instance.GetItemDropped() != null 
                         && InventoryManager.Instance.GetItemDropped().NameItem != item.name)
        {
            Item newItem = InventoryManager.Instance.GetItemDropped();
            InventoryManager.Instance.SetDraggedItem(item);
            SetDataSlot(newItem);
            SetQuantity(newItem.quantity);
        }
        else if (item != null && InventoryManager.Instance.GetItemDropped() != null && 
                 InventoryManager.Instance.GetItemDropped().NameItem == item.name && 
                 InventoryManager.Instance.GetItemDropped().quantity != 1)
        {
            item.quantity++;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SubtractQuantity();
        }
        else if (item != null && InventoryManager.Instance.GetItemDropped() != null && 
                 InventoryManager.Instance.GetItemDropped().NameItem == item.name && 
                 InventoryManager.Instance.GetItemDropped().quantity == 1)
        {
            item.quantity++;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SubtractQuantity();
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item == null && InventoryManager.Instance.GetItemDropped() != null)
        {
            item = InventoryManager.Instance.GetItemDropped();
            SetDataSlot(item);
            SetQuantity(item.quantity);
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && InventoryManager.Instance.GetItemDropped() == null)
        {
            InventoryManager.Instance.SetDraggedItem(item);
            SetDataSlot(null);
            SetQuantity(0);
            item = null;
        }
    }

    public void SetDataSlot(Item item)
    {
        Image image = slot.transform.GetComponent<Image>();
        if (item == null)
        {
            this.item = item;
            image.color = new Color(0, 0, 0, 0);
            return;
        }
        this.item = item;
        image.color = Color.white;
        image.sprite = item.Icon;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity == 0)
        {
            quantityObject.text = "";
        }
        quantityObject.text = quantity.ToString();
    }
}