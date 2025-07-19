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
        }
    }

    public void SetDataSlot(Item item)
    {
        if (item == null) return;
        
        Image image = slot.transform.GetComponent<Image>();
        image.sprite = item.Icon;
    }
}