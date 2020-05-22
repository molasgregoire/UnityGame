using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class levelCreator : MonoBehaviour
{
    public static int level;
    public oEnv env;

    // Start is called before the first frame update
    void Start() { Go(); }

    public void Go()
    {
        GameObject tmp = new GameObject();
        env = tmp.AddComponent<oEnv>();
        
        
        switch(level)
        {
            case 0: levelTest();
                //print(0);
                break;
            case 1:
                //print(1);
                break;
            default: print("Default");
                levelTest();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void levelTest()
    {
        float start = 3f;
        env.maxTime = 60f;
        env.startTime = start;
        env.speedFactor = 1.0f;
        env.maxScore = 80f;

        //etc..

        //empeche les probleme d'instanciation
        env.initialisation();

        env.circleGeneration(5f);
        // 0 > 10
        env.randomGeneration(0f+start, 10f + start, 0.2f);
        env.randomGeneration(0f+start, 10f + start, 0.2f);
        //10 > 20
        env.geometryBalayage(10f + start, 20f + start, 0.3f, 2, 0, 0.05f);
        //20 > 30
        env.randomGeneration(20f + start, 30f + start, 2f);
        env.randomGeneration(20f + start, 30f + start, 0.5f);
        env.randomGeneration(20f + start, 30f + start, 1.0f);
        //30 > 40
        env.geometryBalayage(30f + start, 40f + start, 0.3f, 6, 0, 0.05f);
        //40 > 50
        //50 > 60
        env.randomGeneration(40f + start, 60f + start, 0.5f);
        env.randomGeneration(40f + start, 60f + start, 0.5f);
        env.zoneGeneration(40f + start, 60f + start, 1f);

        //print(env.listObs.Count);
    }
}
