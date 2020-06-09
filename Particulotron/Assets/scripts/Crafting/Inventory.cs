using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  public List<Item> itemList = new List<Item>();
  public List<ElmParticule> itemCraft = new List<ElmParticule>();
  public Baryon particle;
  public ItemDatabase itemDatabase;
  public GameObject inventoryPanel;
  public GameObject craftingPanel;
  public GameObject particlePanel;

  public List<int> baryons = new List<int>();
  public Dictionary<int, List<ElmParticule>> previouslyCrafted = new Dictionary<int, List<ElmParticule>>();
  public static Inventory instance;

  public History history; //Progression state

  //public GameData data;

  private void Start() {
    instance = this;

    InitializeGame();

    initializeSlots();
    initializeCraftingSlots();
    //updatePanelSlots();
    //updateCaftingSlots();
    updateParticle();

    history.HistoryStart();
  }

  void InitializeGame() {

    //Initiate Elementary particles
    AddItem(1);
    AddItem(2);
    //AddItem(3);
    //AddItem(4);
    //AddItem(5);
    //AddItem(6);
    //Debug.Log("before load ? "+itemDatabase.GetQuark(1).title);

    //load ici si ça existe /!\
    //data = new GameData();
    SaveLoad.Load();
    //Debug.Log("Loaded");

    //Load the Load inventory
    if(previouslyCrafted.Count != 0) {
      foreach(KeyValuePair<int, List<ElmParticule>> element in previouslyCrafted) {
        //Debug.Log(element.Key);
        itemCraft = element.Value;
        //Debug.Log("itemcraft "+element.Value[0].title);
        addToLoadMenu(itemDatabase.GetBaryon(element.Key));
      }
      itemCraft.Clear();
    }

    //Debug.Log("Loading complete");

    //initialise l'histoire
    //history = tmp.AddComponent<History>();
    history = new History();
    history.state = GameData.current.state;
    //history.inventory = this;
    history.first = GameData.current.first;;

    SaveLoad.Save();
  }


  void initializeSlots() {
    for(int index=0; index < 6; index++) {
      if (index < itemList.Count) {
        GameObject slot = GameObject.Find("Slot"+index.ToString());
        Image Icon = slot.GetComponent<Image>();
        Icon.color = Color.white;
        Icon.sprite = itemList[index].icon;

        UIItem oslot = slot.GetComponent<UIItem>();
        oslot.item = itemList[index];
      }
      else {
        GameObject slot = GameObject.Find("Slot"+index.ToString());
        Image Icon = slot.GetComponent<Image>();
        Icon.color = Color.clear;

        UIItem oslot = slot.GetComponent<UIItem>();
        oslot.item = null;
      }
    }
  }

  void initializeCraftingSlots() {
    for(int index=0; index < 3; index++) {
      GameObject slot = GameObject.Find("CraftingSlot"+index.ToString());
      Image Icon = slot.GetComponent<Image>();
      if (Icon != null && index < itemCraft.Count) {
        Icon.color = Color.white;
        Icon.sprite = itemCraft[index].icon2;

        UIItem oslot = slot.GetComponent<UIItem>();
        oslot.item = itemCraft[index];
      }
      else {
        Icon.color = Color.clear;
        Icon.sprite = null;

        UIItem oslot = slot.GetComponent<UIItem>();
        oslot.item = null;
      }
    }
  }
    public void addToLoadMenu(Baryon baryon)
    {
        GameObject list = GameObject.Find("List");
        LoadList loadList = list.GetComponent<LoadList>();
        loadList.GenButton(baryon,itemCraft);
    }

  public void Craft() {
    int idToCraft = GetIdCraft();
    if (idToCraft != 0) {
      Baryon baryon = itemDatabase.GetBaryon(idToCraft);
      if (baryon != null) {
        this.particle = baryon;
                List<ElmParticule> value;
        if (previouslyCrafted.TryGetValue(baryon.id,out value))
        {

          }else
                    {
                        previouslyCrafted.Add(baryon.id, itemCraft);
                        addToLoadMenu(baryon);
                    }
      }
      else {
        this.particle = itemDatabase.GetBaryon(8);
      }
    }
    else {
      this.particle = null;
    }
    updateParticle();
  }

    public void CraftPreviouslyCrafted(Baryon baryon)
    {
        this.itemCraft = previouslyCrafted[baryon.id];
        this.particle = baryon;
        updateParticle();
    }

  void updateParticle() {
    foreach(Transform child in particlePanel.transform) {
      UIParticle slot = child.GetComponent<UIParticle>();
      slot.UpdateParticle(particle);
    }
  }

  public List<ElmParticule> GetCraftList() {
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
      ElmParticule itemToAdd = itemDatabase.GetQuark(id);
      itemCraft.Add(itemToAdd);
    }
    initializeCraftingSlots();
    //updateCaftingSlots();
  }

  public ElmParticule CheckForCraft(int id) {
    return itemCraft.Find(item => item.id == id);
  }

  public void RemoveCraft(int id) {
    ElmParticule item = CheckForCraft(id);
    if (item != null) {
        itemCraft.Remove(item);
    }
    initializeCraftingSlots();
    //updateCaftingSlots();
  }


  public void AddItem(int id) {

    if (itemList.Count < 6) {
      //itemList.Add(item);
      Item itemToAdd = itemDatabase.GetQuark(id);
      itemList.Add(itemToAdd);
    }
    initializeSlots();
    //updatePanelSlots();
  }

  public Item CheckForItem(int id) {
    return itemList.Find(item => item.id == id);
  }

  public void RemoveItem(int id) {
    Item item = CheckForItem(id);
    if (item != null) {
        itemList.Remove(item);
    }
    initializeSlots();
    //updatePanelSlots();
  }
}


/*
  void updatePanelSlots() {
    int index =0;
    foreach(Transform child in inventoryPanel.transform) {
      UIItem slot = child.GetComponent<UIItem>();
      if (index < itemList.Count) {
        slot.UpdateItem(itemList[index]);
        //Debug.Log("slot index "+index.ToString());
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
        slot.UpdateItemCraft(itemCraft[index]);
        //slot.item = itemCraft[index];
      }
      else {
        //slot.item=null;
        slot.UpdateItemCraft(null);
      }
      index++;
    }
  }

*/
