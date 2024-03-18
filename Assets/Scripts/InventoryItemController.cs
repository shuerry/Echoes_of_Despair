using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveButton;    

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);      
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()  
    {
        switch (item.itemType)
        {
            case Item.ItemType.Pipe:
                break;
            case Item.ItemType.MedKit:
                PlayerHealth.Instance.TakeHealth(item.value);
                break;
            case Item.ItemType.Key:
                break;
            default:
                break;   
        }


        RemoveItem();    
    }



}

