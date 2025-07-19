using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject draggedItem;
    private bool dragged = false;
    private RectTransform dragArea;
    public Canvas canvas;
    
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
            dragArea.anchoredPosition = localPoint;
        }
    }

    public void Start()
    {
        draggedItem.SetActive(false);
    }

    public void SetDraggedItem(Item item)
    {
        draggedItem.SetActive(true);
        Image image = draggedItem.GetComponent<Image>();
        image.sprite = item.Icon;
        dragged = true;
        dragArea = draggedItem.GetComponent<RectTransform>();
    }
}
