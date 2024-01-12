using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public SlotUI[] slots;

    public ItemSO item;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(this); }
    }
    public void AddToSlot(ItemSO item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].SetItem(item);
                return;
            }
        }
    }
}
