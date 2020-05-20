//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Race() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Crafting() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

}
