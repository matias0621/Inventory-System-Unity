using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject draggedItem;
    
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

    public void Start()
    {
        draggedItem.SetActive(false);
    }

    public void SetDraggedItem(Item item)
    {
        draggedItem.SetActive(true);
        Image image = draggedItem.GetComponent<Image>();
        image.sprite = item.Icon;
    }
}
