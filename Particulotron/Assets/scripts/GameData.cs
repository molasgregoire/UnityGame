using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

  public static GameData current;

  //Liste des crafts
  public List<int> previouslyCraftedId;
  //public Dictionary<int, List<ElmParticule>> previouslyCrafted;

  //History save
  public int state;
  public bool first;

  public GameData() {
      current = this;
      state = 0;
      first = true;
      //previouslyCrafted = new Dictionary<int, List<ElmParticule>>();
      previouslyCraftedId = new List<int>();
    }


  public void ListToDict() {
    Dictionary<int, List<ElmParticule>> tmp = new Dictionary<int, List<ElmParticule>>();
    foreach(int idtmp in previouslyCraftedId) {
      tmp.Add(idtmp, CreateElmList(idtmp));
    }
        Inventory.instance.previouslyCrafted = tmp;
  }

  public void DictToList() {
      previouslyCraftedId = new List<int>(Inventory.instance.previouslyCrafted.Keys);
  }

  List<ElmParticule> CreateElmList(int id) {
    List<ElmParticule> tmplist = new List<ElmParticule>();
    string ids = id.ToString();
    for(int i=0; i < ids.Length; i++) {
      int tmpid = int.Parse(ids[i].ToString());
      tmplist.Add(Inventory.instance.itemDatabase.GetQuark(tmpid));
    }
    return tmplist;
  }
}
