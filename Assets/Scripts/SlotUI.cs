using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{

    public TMP_Text stackableText;
    public Image icon;

    [HideInInspector] public ItemSO item;

    public void SetItem(ItemSO newItem)
    {
        item = newItem;
        UpdateUI();
    }
    private void UpdateUI()
    {
        icon.sprite = item.icon;
        if (icon.sprite == null)
        {
            icon.enabled = false;
        }
        else {
            icon.enabled = true;
        }
    }
}
