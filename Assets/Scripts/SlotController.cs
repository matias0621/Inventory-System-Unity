using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Item item;
    private GameObject slot;
    
    
    void Start()
    {
        slot = gameObject.transform.GetChild(0).gameObject;
        SetDataSlot(item);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && InventoryManager.Instance.GetItemDropped() != null)
        {
            Item newItem = InventoryManager.Instance.GetItemDropped();
            InventoryManager.Instance.SetDraggedItem(item);
            SetDataSlot(newItem);
        }
        else if (item == null && InventoryManager.Instance.GetItemDropped() != null)
        {
            item = InventoryManager.Instance.GetItemDropped();
            SetDataSlot(item);
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && InventoryManager.Instance.GetItemDropped() == null)
        {
            InventoryManager.Instance.SetDraggedItem(item);
            SetDataSlot(null);
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
}