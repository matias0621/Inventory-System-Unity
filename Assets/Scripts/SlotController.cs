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
        if (item != null)
        {
            InventoryManager.Instance.SetDraggedItem(item);
            SetDataSlot(null);
            item = null;
        }
        else if (item == null && InventoryManager.Instance.GetItemDropped() != null)
        {
            item = InventoryManager.Instance.GetItemDropped();
            SetDataSlot(item);
            InventoryManager.Instance.ClearDraggedItem();
        }
    }

    public void SetDataSlot(Item item)
    {
        Image image = slot.transform.GetComponent<Image>();
        if (item == null)
        {
            image.color = new Color(0, 0, 0, 0);
            return;
        }
        image.color = Color.white;
        image.sprite = item.Icon;
    }
}