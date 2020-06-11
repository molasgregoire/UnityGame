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
  public bool inprogress;

  //Affichage
  //public bool faxShow = false;

  public GameData() {
      current = this;
      state = 0;
      first = true;
      inprogress = false;
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

  public static void Advance() {
    GameData.current.state = GameData.current.state + 1;
    GameData.current.first = true;
    SaveLoad.Save();
  }

  public static void HistoryStart() {
    if(GameData.current.first){
      Debug.Log("Scene start");
      Inventory.instance.AffichageText(GameData.current.state+1);
    }
  }

  public static void HistoryRun() {
      GameData.current.inprogress = CheckforHistory();
      Debug.Log("Scene Run");
      GameData.current.first = false;
      Inventory.instance.AffichageText(GameData.current.state+2);
      SaveLoad.Save();

      //pause ?
      //Time.timeScale = 0f;
      //if(Input.GetKey(KeyCode.Space)) {Time.timeScale = 1f;}

      /*bool press = true;
      while(press){
        if(Input.anyKey) {press=false;}
      };*/

  }

  public static void HistoryEnd() {

    //Debug.Log("End");
    if(GameData.current.inprogress) {
      Debug.Log("Scene end");
      GameData.current.inprogress = false;
      GameData.Advance();
      //GameData.UpdateQuark();
    }
    //SaveLoad.Save();
  }

  public static void UpdateQuark() {
    for (int i = 1; i < (GameData.current.state + 3); i++) {
      Inventory.instance.AddItem(i);
    }
    GameData.current.first = true;
    SaveLoad.Save();
  }

  public static bool CheckforHistory() {
    switch(GameData.current.state)
    {
        case 0: //proton
            if(Inventory.instance.particle.id == 211) {
              return true;
            }
            break;
        case 1: //Sigma
            if(Inventory.instance.particle.id == 311) {
              return true;
            }
            break;
        case 2: //charmed xi
            if(Inventory.instance.particle.id == 431) {
              return true;
            }
            break;
        case 3: //bottom xi
            if(Inventory.instance.particle.id == 632) {
              return true;
            }
            break;

        default:
            return false;
    }
    return false;
  }
}
