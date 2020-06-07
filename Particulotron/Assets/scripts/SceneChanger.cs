//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Race() {

        //GameObject.Find("Canvas");
        Baryon tmparticle = GameObject.Find("Canvas1").GetComponent<Inventory>().particle;

        //test static
        oEnv.Q = tmparticle.Q;
        oEnv.S = tmparticle.S;
        oEnv.C = tmparticle.C;
        oEnv.B = tmparticle.B;

        History tmphistory = GameObject.Find("Canvas1").GetComponent<Inventory>().history;
        tmphistory.HistoryRun();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }

    public void Crafting() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

}
