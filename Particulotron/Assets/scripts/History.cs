using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History
{
  public int state; //0-u,d; 1-u,d,s; 2-u,d,s,c; 3-u,d,u,c,b; 4-u,d,u,c,b,t;
  public bool first; //si c'est la première fois ou non

  public Inventory inventory;

  public void Advance() {
    state = state + 1;
    first = true;
  }

  public void HistoryStart() {
    if(first){
      Debug.Log("Scene start");
      //first = false;
    }
  }

  public void HistoryRun() {
    if(CheckforHistory() && first) {
      Debug.Log("Scene Run");
      first = false;
    }
  }

  public void HistoryEnd() {
    Debug.Log("Scene end");
  }

  public void UpdateQuark() {
    inventory.AddItem(state+2);
  }

  bool CheckforHistory() {
    switch(state)
    {
        case 0: //proton
            if(inventory.particle.id == 211) {
              return true;
            }
            break;
        case 1: //Sigma
            if(inventory.particle.id == 311) {
              return true;
            }
            break;
        case 2: //charmed xi
            if(inventory.particle.id == 431) {
              return true;
            }
            break;
        case 3: //bottom xi
            if(inventory.particle.id == 632) {
              return true;
            }
            break;

        default:
            return false;
    }
    return false;
  }
}
