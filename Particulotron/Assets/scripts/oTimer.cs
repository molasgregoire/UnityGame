using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oTimer : MonoBehaviour
{
    public float tps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //petit timer des familles
    void OnGUI()
    {
        tps += Time.deltaTime * 0.5f;

        float minutes = Mathf.Floor(tps / 60);
        float seconds = tps % 60;

        GUI.Label(new Rect(15, 15, 2500, 1000), minutes + ":" + Mathf.RoundToInt(seconds));
    }
}
