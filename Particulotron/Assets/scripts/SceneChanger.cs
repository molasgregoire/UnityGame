//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Race() {

        if(Inventory.instance.particle != null){
          if(GameData.CheckforHistory() && GameData.current.first) {
              GameData.HistoryRun();
          }
          else {
              //GameObject.Find("Canvas");
              Baryon tmparticle = GameObject.Find("Canvas1").GetComponent<Inventory>().particle;

              //test static
              oEnv.Q = tmparticle.Q;
              oEnv.S = tmparticle.S;
              oEnv.C = tmparticle.C;
              oEnv.B = tmparticle.B;

              //History tmphistory = GameObject.Find("Canvas1").GetComponent<Inventory>().history;
              //GameData.HistoryRun();

              SaveLoad.Save();

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
      } else {
        //Si pas de particule
        foreach(Transform child in Inventory.instance.particlePanel.transform) {
          UIParticle panel = child.GetComponent<UIParticle>();
          //slot.UpdateParticle(particle);
          panel.UpdateText("Use the post-it to create a particle.");
        }
        //UIParticle panel = Inventory.instance.particlePanel.GetComponent<UIParticle>();
        //slot.UpdateParticle(particle);

      }
    }

    public void Crafting() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        SaveLoad.Save();
    }

}
