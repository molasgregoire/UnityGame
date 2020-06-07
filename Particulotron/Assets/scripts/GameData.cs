using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

  public static GameData current;

  //Liste des crafts
  public Dictionary<int, List<ElmParticule>> previouslyCrafted;

  //History save
  public int state;
  public bool first;

  public GameData() {
      state = 0;
      first = true;
      previouslyCrafted = new Dictionary<int, List<ElmParticule>>();
    }
}
