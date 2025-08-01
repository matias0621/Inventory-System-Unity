using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject draggedItem;
    private bool dragged = false;
    private RectTransform dragArea;
    public Canvas canvas;
    public Item itemDropped;
    public TextMeshProUGUI itemQuantity;
    public GameObject[] SlotList;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (dragged)
        {
            Vector2 localPoint;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out localPoint
            );
            localPoint.x -= 50;
            localPoint.y -= 50;
            dragArea.anchoredPosition = localPoint;
        }
    }

    public void Start()
    {
        draggedItem.SetActive(false);
    }

    public void SetDraggedItem(Item item)
    {
        if (item == null)
        {
            draggedItem.SetActive(false);
            dragged = false;
            itemDropped = null;
            return;
        }
        
        draggedItem.SetActive(true);
        Image image = draggedItem.GetComponent<Image>();
        image.sprite = item.Icon;
        dragged = true;
        dragArea = draggedItem.GetComponent<RectTransform>();
        itemDropped = item;
        SetQuantity(itemDropped.quantity);
    }


    public Item GetItemDropped()
    {
        return itemDropped;
    }

    public void ClearDraggedItem()
    {
        draggedItem.SetActive(false);
        dragged = false;
        itemDropped = null;
    }

    public void SubtractQuantity()
    {
        if (itemDropped != null)
        {
            itemDropped.quantity--;
            SetQuantity(itemDropped.quantity);
            Debug.Log(itemDropped.quantity);
        }
    }

    public void SetQuantity(int quantity)
    {
        itemQuantity.text = quantity.ToString();
    }

    public void FindSlotForSaveItem(Item item)
    {
        foreach (GameObject slot in SlotList)
        {
            SlotController slotController = slot.GetComponent<SlotController>();
            if (slotController.IsEmpty())
            {
                slotController.SetDataSlot(item);
                slotController.SetQuantity(1);
                break;
            }
        }
    }
}
