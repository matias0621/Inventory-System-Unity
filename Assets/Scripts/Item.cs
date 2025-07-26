using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("Item")] 
    public string NameItem;
    public Sprite Icon;
    public int quantity;
    
    public Item Clone()
    {
        Item newItem = Instantiate(this);
        newItem.quantity = this.quantity;
        return newItem;
    }
}
