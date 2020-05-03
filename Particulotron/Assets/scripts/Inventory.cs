using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<Item> itemList = new List<Item>();
  //public ItemDatabase itemDatabase;
  public GameObject inventoryPanel;
  //public UIInventory inventoryUI;
  public static Inventory instance;

  private void Start() {
    instance = this;
    //AddItem(1);
    //AddItem("Proton");
    //Debug.Log("Start ?");
    //RemoveItem(1);
    updatePanelSlots();
  }

  void updatePanelSlots() {
    int index =0;
    foreach(Transform child in inventoryPanel.transform) {
      UIItem slot = child.GetComponent<UIItem>();
      if (index < itemList.Count) {
        slot.item = itemList[index];
      }
      else {
        slot.item=null;
      }
      slot.UpdateItem();
      index++;
    }
  }


  public void AddItem(Item item) {
    if (itemList.Count < 6) {
      itemList.Add(item);
    }
    updatePanelSlots();
    /*
    Item itemToAdd = itemDatabase.GetItem(id);
    itemList.Add(itemToAdd);
    inventoryUI.AddNewItem(itemToAdd);
    Debug.Log("Added item : " + itemToAdd.title);
    */
  }

  public void RemoveItem(Item item) {
    itemList.Remove(item);
    updatePanelSlots();
  }
/*
  public void AddItem(string title) {
    Item itemToAdd = itemDatabase.GetItem(title);
    itemList.Add(itemToAdd);
    Debug.Log("Added item title : " + itemToAdd.title);
  }

  public Item HaveItem(int id) {
    return itemList.Find(item => item.id == id);
  }

  public void RemoveItem(int id) {
    Item itemToRemove = HaveItem(id);
    if (itemToRemove != null) {
      itemList.Remove(itemToRemove);
      inventoryUI.RemoveItem(itemToRemove);
      Debug.Log("Item removed : " +itemToRemove.title);
    }
  }*/
}
