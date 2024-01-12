using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Scriptable Object/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public int quantity = 0;

    public void Collect()
    {
        quantity++;

        //Update the inventory
        Inventory.Instance.AddToSlot(this);
    }
}
