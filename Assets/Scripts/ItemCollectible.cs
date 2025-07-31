using System;
using UnityEngine;

public class ItemCollectible : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Item item;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.FindSlotForSaveItem(item);
            Destroy(gameObject);
        }
    }
}
