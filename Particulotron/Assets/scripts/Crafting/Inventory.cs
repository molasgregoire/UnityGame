using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<Item> itemList = new List<Item>();
  public List<Item> itemCraft = new List<Item>();
  public Item particle;
  public ItemDatabase itemDatabase;
  public GameObject inventoryPanel;
  public GameObject craftingPanel;
  public GameObject particlePanel;
  public static Inventory instance;

  private void Start() {
    instance = this;


    AddItem(1);
    AddItem(2);
    updatePanelSlots();
    updateCaftingSlots();
    updateParticle();

  }

  void updatePanelSlots() {
    int index =0;
    foreach(Transform child in inventoryPanel.transform) {
      UIItem slot = child.GetComponent<UIItem>();
      if (index < itemList.Count) {
        slot.UpdateItem(itemList[index]);
        //slot.item = itemCraft[index];
      }
      else {
        //slot.item=null;
        slot.UpdateItem(null);
      }
      //slot.UpdateItem();
      index++;
    }
  }

  void updateCaftingSlots() {
    int index =0;
    foreach(Transform child in craftingPanel.transform) {
      UIItem slot = child.GetComponent<UIItem>();
      if (index < itemCraft.Count) {
        slot.UpdateItem(itemCraft[index]);
        //slot.item = itemCraft[index];
      }
      else {
        //slot.item=null;
        slot.UpdateItem(null);
      }
      index++;
    }
  }

  public void Craft() {
    int idToCraft = GetIdCraft();
    Debug.Log("CraftId : " + idToCraft.ToString());
    if (idToCraft != 0) {
      this.particle = itemDatabase.GetBaryon(idToCraft);
    }
    else {
      this.particle = null;
    }
    updateParticle();
  }

  void updateParticle() {
    foreach(Transform child in particlePanel.transform) {
      UIParticle slot = child.GetComponent<UIParticle>();
      slot.UpdateParticle(particle);
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

  int GetIdCraft() {
    if (itemCraft.Count == 3) { //Baryons
      int a = itemCraft[0].id;
      int b = itemCraft[1].id;
      int c = itemCraft[2].id;
      if (a >= b & b >= c) { //abc
        return int.Parse(a.ToString() + b.ToString() + c.ToString());
      } else {
        if (a >= c & c >= b) { //acb
          return int.Parse(a.ToString() + c.ToString() + b.ToString());
        }
        else {
          if (b >= a & a >= c) { //bac
            return int.Parse(b.ToString() + a.ToString() + c.ToString());
          }
          else {
            if (b >= c & c >= a) { //bca
              return int.Parse(b.ToString() + c.ToString() + a.ToString());
            }
            else {
              if (c >= a & a >= b) { //cab
                return int.Parse(c.ToString() + a.ToString() + b.ToString());
              }
              else {
                if (c >= b & b >= a) { //cba
                  return int.Parse(c.ToString() + b.ToString() + a.ToString());
                } else {
                  return 0;
                }
              }
            }
          }
        }
      }
    }
    else {
      return 0;
    }
  }

  public void AddCraft(int id) {

    if (itemCraft.Count < 3) {
      //itemList.Add(item);
      Item itemToAdd = itemDatabase.GetQuark(id);
      itemCraft.Add(itemToAdd);
    }
    updateCaftingSlots();
  }

  public Item CheckForCraft(int id) {
    return itemCraft.Find(item => item.id == id);
  }

  public void RemoveCraft(int id) {
    Item item = CheckForCraft(id);
    if (item != null) {
        itemCraft.Remove(item);
    }
    updateCaftingSlots();
  }


  public void AddItem(int id) {

    if (itemList.Count < 6) {
      //itemList.Add(item);
      Item itemToAdd = itemDatabase.GetQuark(id);
      itemList.Add(itemToAdd);
    }
    updatePanelSlots();
  }

  public Item CheckForItem(int id) {
    return itemList.Find(item => item.id == id);
  }

  public void RemoveItem(int id) {
    Item item = CheckForItem(id);
    if (item != null) {
        itemList.Remove(item);
    }
    updatePanelSlots();
  }
}
