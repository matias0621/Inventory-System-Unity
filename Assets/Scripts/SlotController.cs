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
    [SerializeField] private int quantityItem;
    
    

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
        int quantityItemMouse = InventoryManager.Instance.quantityItem;

        if (item != null && mouseItem != null && mouseItem.NameItem != item.NameItem)
        {
            //Debug.Log("Primer if: Los ítems son distintos. Se intercambian. SetDraggedItem con el item actual y SetDataSlot con el del mouse.");
            InventoryManager.Instance.SetDraggedItem(item.Clone(), quantityItem);
            SetDataSlot(mouseItem.Clone());
            SetQuantity(quantityItemMouse);
        }
        else if (item != null && mouseItem != null &&
                 mouseItem.NameItem == item.NameItem && quantityItemMouse > 1 && quantityItem < 64)
        {
            //Debug.Log("Segundo if: Mismo tipo de ítem, cantidad del mouse > 1 y slot no lleno. Se suma uno al slot y se resta uno del mouse.");
            IncreaseQuantity();
            SetQuantity(quantityItem);
            InventoryManager.Instance.SubtractQuantity();
        }
        else if (item != null && mouseItem != null &&
                 mouseItem.NameItem == item.NameItem && quantityItemMouse == 1)
        {
            //Debug.Log("Tercer if: Mismo tipo de ítem y el del mouse tiene solo 1. Se suma al slot y se limpia el item del mouse.");
            IncreaseQuantity();
            InventoryManager.Instance.SubtractQuantity();
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && mouseItem != null && quantityItem >= 64)
        {
            //Debug.Log("Cuarto if: El slot ya está lleno. Se intercambian los ítems.");
            InventoryManager.Instance.SetDraggedItem(item.Clone(), quantityItem);
            SetDataSlot(mouseItem.Clone());
            SetQuantity(quantityItemMouse);
        }
        else if (item == null && mouseItem != null)
        {
            //Debug.Log("Quinto if: El slot está vacío y hay un ítem en el mouse. Se coloca el ítem en el slot y se limpia el del mouse.");
            SetDataSlot(mouseItem.Clone());
            SetQuantity(quantityItemMouse);
            InventoryManager.Instance.ClearDraggedItem();
        }
        else if (item != null && mouseItem == null)
        {
            //Debug.Log("Sexto if: Hay un ítem en el slot y el mouse está vacío. Se levanta el ítem del slot al mouse y se limpia el slot.");
            InventoryManager.Instance.SetDraggedItem(item.Clone(), quantityItem);
            SetDataSlot(null);
            SetQuantity(0);
            item = null;
        }
    }


    public void SplitItem()
    {
        Item mouseItem = InventoryManager.Instance.GetItemDropped();
        int quantityItemMouse = InventoryManager.Instance.quantityItem;

        if (mouseItem == null && item != null && quantityItem > 1)
        {
            Debug.Log("---------------------------Antes de la divicion----------------------");
            Debug.Log(quantityItem);
            Debug.Log("---------------------------Antes de la divicion----------------------");
            quantityItem /= 2;
            Debug.Log("---------------------------Despues de la divicion----------------------");
            Debug.Log(quantityItem);
            Debug.Log("---------------------------Despues de la divicion----------------------");
            SetQuantity(quantityItem);
            InventoryManager.Instance.SetDraggedItem(item.Clone(), quantityItem);
        }

        if (mouseItem != null && mouseItem.NameItem == item.NameItem)
        {
            quantityItem += quantityItemMouse;
            if (quantityItem > 64)
            {
                quantityItem -= quantityItemMouse;
                return;
            }
            SetQuantity(quantityItem);
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
    }

    public void IncreaseQuantity()
    {
        quantityItem++;
        quantityObject.text = quantityItem.ToString(); 
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
