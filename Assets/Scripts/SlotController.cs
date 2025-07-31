using System;
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) InteractiveInventory();
        else if (eventData.button == PointerEventData.InputButton.Right) SplitItem();
    }

   public void InteractiveInventory()
    {
        Item mouseItem = InventoryManager.Instance.GetItemDropped();

        if (item != null && mouseItem != null && mouseItem.NameItem != item.NameItem)
        {
            Debug.Log("Primer if: Los ítems son distintos. Se intercambian. SetDraggedItem con el item actual y SetDataSlot con el del mouse.");
            InventoryManager.Instance.SetDraggedItem(item.Clone());
            SetDataSlot(mouseItem.Clone());
            SetQuantity(mouseItem.quantity);
        }
        else if (item != null && mouseItem != null &&
                 mouseItem.NameItem == item.NameItem && mouseItem.quantity > 1 && item.quantity < 64)
        {
            Debug.Log("Segundo if: Mismo tipo de ítem, cantidad del mouse > 1 y slot no lleno. Se suma uno al slot y se resta uno del mouse.");
            item.quantity++;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SubtractQuantity();
        }
        else if (item != null && mouseItem != null &&
                 mouseItem.NameItem == item.NameItem && mouseItem.quantity == 1)
        {
            Debug.Log("Tercer if: Mismo tipo de ítem y el del mouse tiene solo 1. Se suma al slot y se limpia el item del mouse.");
            item.quantity++;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SubtractQuantity();
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && mouseItem != null && item.quantity >= 64)
        {
            Debug.Log("Cuarto if: El slot ya está lleno. Se intercambian los ítems.");
            InventoryManager.Instance.SetDraggedItem(item.Clone());
            SetDataSlot(mouseItem.Clone());
            SetQuantity(mouseItem.quantity);
        }
        else if (item == null && mouseItem != null)
        {
            Debug.Log("Quinto if: El slot está vacío y hay un ítem en el mouse. Se coloca el ítem en el slot y se limpia el del mouse.");
            SetDataSlot(mouseItem.Clone());
            SetQuantity(mouseItem.quantity);
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && mouseItem == null)
        {
            Debug.Log("Sexto if: Hay un ítem en el slot y el mouse está vacío. Se levanta el ítem del slot al mouse y se limpia el slot.");
            InventoryManager.Instance.SetDraggedItem(item.Clone());
            SetDataSlot(null);
            SetQuantity(0);
            item = null;
        }
    }


    public void SplitItem()
    {
        Item mouseItem = InventoryManager.Instance.GetItemDropped();

        if (mouseItem == null && item != null)
        {
            item.quantity = item.quantity/2;
            SetQuantity(item.quantity);
            InventoryManager.Instance.SetDraggedItem(item.Clone());
        }

        if (mouseItem != null && mouseItem.NameItem == item.NameItem)
        {
            item.quantity = item.quantity + mouseItem.quantity;
            if (item.quantity > 64)
            {
                item.quantity = item.quantity - mouseItem.quantity;
                return;
            }
            SetQuantity(item.quantity);
            InventoryManager.Instance.ClearDraggedItem();
        }
    }

    public void SetDataSlot(Item newItem)
    {
        item = newItem;

        if (newItem == null)
        {
            Image image = slot.transform.GetComponent<Image>();
            image.sprite = null;
            slot.SetActive(false);
            quantityObject.text = "";
        }
        else
        {
            slot.SetActive(true);
            Image image = slot.transform.GetComponent<Image>();
            image.sprite = newItem.Icon;
        }
    }

    public void SetQuantity(int quantity)
    {
        quantityObject.text = quantity > 0 ? quantity.ToString() : "";
        Debug.Log(item.quantity);
    }

    public Boolean IsEmpty()
    {
        return item == null ? true : false;
    }

    public Item GetItem()
    {
        return item;
    }
}
