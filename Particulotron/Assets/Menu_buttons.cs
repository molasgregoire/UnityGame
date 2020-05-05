using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Menu_buttons : MonoBehaviour
{

    public void PlayGame()
    {
       UnityEngine.Debug.Log("Play_button pressed");
       SceneManager.LoadScene(1);
    }

    public void OpenCrafting()
    {
        SceneManager.LoadScene(0);
    }
}
