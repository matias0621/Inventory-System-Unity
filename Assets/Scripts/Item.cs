using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public enum Quality
    {
        Normal,
        Rare,
        Epic,
        Legendary,
    }
    [Header("Item")] 
    public string NameItem;
    public Sprite Icon;
    public string description;
    public Quality quality;
    public int weight;
    
    public Item Clone()
    {
        Item newItem = Instantiate(this);
        return newItem;
    }
}
