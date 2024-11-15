using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{

    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public ItemType itemType;    


    public enum ItemType {
         Pipe,
         MedKit,
         Key,
         Journal1,
         Journal2,
         Journal3,
         Journal4
    }



}
