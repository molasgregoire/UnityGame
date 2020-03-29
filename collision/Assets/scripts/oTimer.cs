using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oTimer : MonoBehaviour {

    public float tps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //petit timer des familles
    void OnGUI()
    {
        tps += Time.deltaTime * 0.5f;

        float minutes = Mathf.Floor(tps / 60);
        float seconds = tps % 60;

        GUI.Label(new Rect(10, 10, 250, 100), minutes + ":" + Mathf.RoundToInt(seconds));
    }
}
