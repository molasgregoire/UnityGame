using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<Item> itemList = new List<Item>();
  public List<Item> itemCraft = new List<Item>();
  public Item particle;
  //public ItemDatabase itemDatabase;
  public GameObject inventoryPanel;
  public GameObject craftingPanel;
  public GameObject particlePanel;
  public static Inventory instance;

  private void Start() {
    instance = this;
    updatePanelSlots();
    updateCaftingSlots();
    updateParticle();
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

  void updateCaftingSlots() {
    int index =0;
    foreach(Transform child in craftingPanel.transform) {
      UIItem slot = child.GetComponent<UIItem>();
      if (index < itemCraft.Count) {
        slot.item = itemCraft[index];
      }
      else {
        slot.item=null;
      }
      slot.UpdateItem();
      index++;
    }
  }

  public void ChangeParticle(Item item) {
    particle = item;
    updateParticle();
  }

  void updateParticle() {
    foreach(Transform child in particlePanel.transform) {
      UIParticle slot = child.GetComponent<UIParticle>();
      slot.item = particle;
      slot.UpdateParticle();
    }
  }

  public List<Item> GetCraftList() {
    return itemCraft;
  }

  public int ItemCount(int itemid)
  {
    int number = 0;

    for (int i = 0; i < itemCraft.Count; i++)
    {
      Item item = itemCraft[i];
      if (item != null && item.id == itemid)
      {
        number++;
      }
    }
    return number;
  }

  public void AddCraft(Item item) {
    if (itemCraft.Count < 3) {
      itemCraft.Add(item);
    }
    updateCaftingSlots();
  }

  public void RemoveCraft(Item item) {
    itemCraft.Remove(item);
    updateCaftingSlots();
  }


  public void AddItem(Item item) {
    if (itemList.Count < 6) {
      itemList.Add(item);
    }
    updatePanelSlots();
  }

  public void RemoveItem(Item item) {
    itemList.Remove(item);
    updatePanelSlots();
  }
}
