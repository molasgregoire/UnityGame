using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class levelCreator : MonoBehaviour
{
    public static int level = 0;
    public oEnv env;

    public static List<int> Blocks = new List<int>();

    // Start is called before the first frame update
    void Start() { /*Go();*/ }

    public void Go()
    {
        GameObject tmp = new GameObject();
        env = tmp.AddComponent<oEnv>();


        switch(level)
        {
            case 0:
                lvl4();
                ///lvl1Proton();
                //levelDemo();
                //print(0);
                break;
            case 1:
                levelTest();
                //print(1);
                break;
            case 2:
                levelBlock();
                break;
            case 3:
                levelEndtest();
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

    public void blocinit(int blocky, float first, float total) {
      switch(blocky)
      {
        case 0: //ZONE
            env.zoneGeneration(first, total,0.5f);
            break;
        case 1: //ETC...

            break;
        default: //RANDOM
            env.randomGeneration(first, total,0.5f);
            break;
      }
    }

    public void initlevel() {

      foreach(int bloc in Blocks) {
        float first = 0f; //a changer...
        float total = 10f;
        blocinit(bloc, first, total);
      }

    }

    void levelBlock() {
      float start = 3f;
      env.maxTime = 60f; //70
      env.startTime = start;
      env.speedFactor = 1.5f;
      env.maxScore = 100f;
      env.scoreBonusTime = 1f;
      oTimer.tps = 0;

      env.initialisation();

      //initialisation nb blocks
      //random list de int de la taille du nb
      //initialisation
    }

    void levelDemo()
    {
      float start = 3f;
      env.maxTime = 60f; //70
      env.startTime = start;
      env.speedFactor = 1.5f;
      env.maxScore = 100f;
      env.scoreBonusTime = 1f;
      oTimer.tps = 0;

      env.initialisation();

        List<List<float>> magnetTab = new List<List<float>>() {
            new List<float> {10f+start , 1f, 1f, 0f, 0f, -1f, -1f, 0f, 0f }, };
        env.Aimant.magnetTab = magnetTab;

        env.targetTab = new List<float>() { start , 50f , 1f , 50f ,60f,0.001f };

        float intervalTest = 0.5f;
      env.randomGeneration( 0, env.maxTime-10f, intervalTest);
      env.randomGeneration( 0, env.maxTime-10f, intervalTest);
      //randomGeneration( startTime, maxTime, intervalTest);
      env.randomGeneration( (env.maxTime-10f)/3f, env.maxTime-10f, intervalTest);
      //randomGeneration(maxTime / 3f, maxTime, intervalTest);
      //randomGeneration( 2f* maxTime / 3f, maxTime, intervalTest);
      env.randomGeneration( 2f* (env.maxTime-10f) / 3f, env.maxTime-10f, intervalTest);
      //test
      env.zoneGeneration(env.startTime, env.maxTime-10f, 3f);

      foreach( oObstacle obs in env.listObs) { obs.apparitionTime += start; }
    }

    void levelTest()
    {
        float start = 3f;
        env.maxTime = 95f;
        env.startTime = start;
        env.speedFactor = 1.0f;
        env.maxScore = 150f;
        env.scoreBonusTime = 1.5f;
        //reinit
        oTimer.tps = 0;
        //etc..

        //empeche les probleme d'instanciation
        env.initialisation();

        //aimants
        // doit contenir des liste de 9 float
        // 1 pour le temps, et les 8 autres -1 0 ou +1 pour les aimants
        List<List<float>> magnetTab = new List<List<float>>() {
            new List<float> {10f+start , 1f,1f,1f,1f,-1f,-1f,-1f,-1f },
            new List<float> {20f+start , 1f,1f,0f,0f,-1f,-1f,0f,0f },
            new List<float> {30f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
            new List<float> {40f+start , -1f,-1f,-1f,-1f,-1f,-1f,-1f,-1f },
            new List<float> {50f+start , 1f,1f,1f,1f,-1f,-1f,-1f,-1f },
            new List<float> {70f+start , -1f,1f,-1f,1f,-1f,1f,-1f,1f },
            new List<float> {80f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
        };

        env.Aimant.magnetTab = magnetTab;

        env.circleGeneration(5f);

        // 0 > 10
        env.randomGeneration(0f, 10f , 0.3f);
        env.randomGeneration(0f, 10f , 0.7f);
        //10 > 20
        env.geometryBalayage(10f , 13f, 0.3f, 2, 0, 0.15f);
        env.geometryBalayage(13f , 16f, 0.3f, 2, 0, 0.15f);
        env.geometryBalayage(16f , 19f, 0.3f, 2, 0, 0.15f);
        //env.geometryBalayage(17.5f + start, 20f + start, 0.3f, 2, 0, 0.15f);
        //20 > 30
        env.randomGeneration(20f , 30f , 2f);
        env.randomGeneration(20f , 30f , 0.5f);
        env.randomGeneration(20f , 30f , 1.0f);
        //30 > 40
        for (int i = 0; i < 10; i++)
        { env.geometryLine(30f+(float)i, i+1, (float)i, 0.1f); }
        //40 > 50
        env.zoneGeneration(40f , 50f , 0.25f);
        env.targetTab = new List<float>() { 40f+start,50f+start,1f };
        //50 > 70
        env.randomGeneration(50f , 70f , 0.3f);
        env.randomGeneration(50f , 70f, 0.5f);
        env.randomGeneration(50f , 70f, 0.5f);
        env.zoneGeneration(50f , 70f , 1f);
        //70 > 80
        env.geometryBalayage(70f , 80f , 0.3f, 2, 0, 0f);
        env.geometryBalayage(70f, 80f, 0.3f, 2, 3.141f/2f, 0f);
        //80 > 90
        for( int i = 0; i < 20; i++)
        {
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
            env.geometryGenerator(80f+(float)i*0.5f, UnityEngine.Random.Range(2,8), UnityEngine.Random.Range(0.5f,3.5f), UnityEngine.Random.Range(0f,3.141f));
        }


        foreach( oObstacle obs in env.listObs) { obs.apparitionTime += start; }
        ///peut etre juste faire une iteration sur les obs pour ajouté start à la fin ?
        //print(env.listObs.Count);
    }

    void lvl1Proton()
    {
        float start = 3f;
        env.maxTime = 65f;
        env.startTime = start;
        env.speedFactor = 1.0f;
        env.maxScore = 100f;
        env.scoreBonusTime = 1.0f;
        //reinit
        oTimer.tps = 0;
        //etc..

        //empeche les probleme d'instanciation
        env.initialisation();

        List<List<float>> magnetTab = new List<List<float>>() {
            //new List<float> {10f+start , 1f,1f,1f,1f,-1f,-1f,-1f,-1f },
            new List<float> {10f+start , -1f,-1f,-1f,-1f,-1f,-1f,-1f,-1f },
            new List<float> {20f+start , -1f,1f,-1f,1f,-1f,1f,-1f,1f },
            new List<float> {30f+start , 1f,0f,0f,0f,-1f,0f,0f,0f },
            new List<float> {40f+start , 0f,1f,0f,0f,0f,-1f,0f,0f },
            new List<float> {42f+start , 0f,0f,1f,0f,0f,0f,-1f,0f },
            new List<float> {44f+start , 0f,0f,0f,1f,0f,0f,0f,-1f },
            new List<float> {46f+start , -1f,0f,0f,0f,1f,0f,0f,0f },
            new List<float> {48f+start , 0f,-1f,0f,0f,0f,1f,0f,0f },
            new List<float> {50f+start , 0f,0f,-1f,0f,0f,0f,1f,0f },
            new List<float> {52f+start , 0f,0f,0f,-1f,0f,0f,0f,1f },
            new List<float> {54f+start , 1f,0f,0f,0f,-1f,0f,0f,0f },
            new List<float> {56f+start , 0f,1f,0f,0f,0f,-1f,0f,0f },
            new List<float> {58f+start , 0f,0f,1f,0f,0f,0f,-1f,0f },
            new List<float> {60f+start , 0f,0f,0f,1f,0f,0f,0f,-1f },
            //new List<float> {50f+start , 1f,1f,1f,1f,-1f,-1f,-1f,-1f },
           // new List<float> {70f+start , -1f,1f,-1f,1f,-1f,1f,-1f,1f },
           // new List<float> {80f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
        };

        env.Aimant.magnetTab = magnetTab;

        // 0 > 10 aleatoire leger
        env.randomGeneration(0f, 10f, 1.2f);
        env.randomGeneration(0f, 10f, 0.4f);
        env.randomGeneration(0f, 10f, 0.8f);
        //10 > 20 aimant bleu et obs en bordure
        env.zoneGeneration(10f, 20f, 0.25f);
        env.zoneGeneration(10f, 20f, 0.25f);
        env.zoneGeneration(10f, 20f, 0.25f);
        env.zoneGeneration(10f, 20f, 0.25f);
        //20 > 30 traqueur + aimants alt + aleatoire leger
        env.randomGeneration(20f, 30f, 0.3f);
        env.randomGeneration(20f, 30f, 0.4f);
        env.randomGeneration(20f, 30f, 0.8f);
        env.targetTab = new List<float>() { 20f + start, 30f + start, 1.0f };
        //30 > 40 balayage lent
        env.geometryBalayage(30f, 40f, 0.5f, 3, 0, 0.1f);
        env.geometryBalayage(30f, 40f, 0.5f, 3, 0, -0.1f);
        //40 > 60 accumunlation aleatoire + aimants rota
        env.randomGeneration(40f, 60f, 0.5f);
        env.randomGeneration(43f, 60f, 0.5f);
        env.randomGeneration(45f, 60f, 0.5f);
        env.randomGeneration(47.5f, 60f, 0.5f);
        env.randomGeneration(50f, 60f, 0.5f);
        env.randomGeneration(55f, 60f, 0.5f);
        env.randomGeneration(55f, 60f, 0.5f);


        foreach (oObstacle obs in env.listObs) { obs.apparitionTime += start; }
    }

    void lvl2()
    {
        float start = 3f;
        env.maxTime = 65f;
        env.startTime = start;
        env.speedFactor = 1.0f;
        env.maxScore = 100f;
        env.scoreBonusTime = 1.0f;
        //reinit
        oTimer.tps = 0;
        //etc..

        //empeche les probleme d'instanciation
        env.initialisation();
        env.circleGeneration(4f);

        List<List<float>> magnetTab = new List<List<float>>() {
            new List<float> {0f+start , -1f,1f,-1f,1f,-1f,1f,-1f,1f },
            new List<float> {20f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
            new List<float> {30f+start , 1f,1f,0f,0f,-1f,-1f,0f,0f },
            new List<float> {40f+start , 1f,1f,-1f,-1f,1f,1f,-1f,-1f },
            new List<float> {50f+start , 0f,0f,0f,0f,0f,0f,0f,0f },

        };

        // 0 > 10 spiral zone
        env.zoneSpiral(0.3f, 0f, 10f);
        //0 > 20 accumulation aleatoire !!
        env.randomGeneration(5f, 20f, 0.5f);
        env.randomGeneration(7f, 20f, 0.5f);
        env.randomGeneration(10f, 20f, 0.5f);
        env.randomGeneration(10f, 20f, 0.5f);
        env.randomGeneration(13f, 20f, 0.5f);
        env.randomGeneration(15f, 20f, 0.5f);
        env.randomGeneration(15f, 20f, 0.5f);
        //20 > 30 balayage multiples
        //env.geometryBalayage(20f, 30f, 0.5f, 4, 0, 0.15f);
        env.geometryBalayage(20f, 30f, 1f, 5, 0, 0.1f);
        env.geometryBalayage(20.5f, 29.5f, 1f, 5, 0, -0.1f);
        //30 > 40 traqueur + aleatoire + zones
        env.targetTab = new List<float>() { 30f + start, 40f + start, 1.0f };
        env.zoneSpiral(1.3f, 30f, 40f);
        env.randomGeneration(30f, 40f, 0.2f);
        env.randomGeneration(30f, 40f, 0.5f);
        env.randomGeneration(30f, 40f, 0.9f);
        //40 > 50 balayage double
        env.geometryBalayage(40f, 50f, 0.75f, 3, 0, 0.07f);
        env.geometryBalayage(40f, 50f, 0.75f, 3, 0, -0.07f);
        //50 > 60 pousiere d'étoile
        for (int i = 0; i < 20; i++)
        {
            env.geometryGenerator(50f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(50f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(50f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            //env.geometryGenerator(60f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            //env.geometryGenerator(60f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
        }


        env.Aimant.magnetTab = magnetTab;

        foreach (oObstacle obs in env.listObs) { obs.apparitionTime += start; }
    }

    void lvl3()
    {
        float start = 3f;
        env.maxTime = 65f;
        env.startTime = start;
        env.speedFactor = 1.0f;
        env.maxScore = 100f;
        env.scoreBonusTime = 1.0f;
        //reinit
        oTimer.tps = 0;
        //etc..

        //empeche les probleme d'instanciation
        env.initialisation();
        env.circleGeneration(6f);

        List<List<float>> magnetTab = new List<List<float>>() {
            new List<float> {0f+start , 1f,1f,1f,1f,1f,1f,1f,1f },
            new List<float> {5f+start , -1f,-1f,-1f,-1f,-1f,-1f,-1f,-1f },
            new List<float> {10f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
            new List<float> {20f+start , 0f,0f,1f,1f,-1f,-1f,0f,0f },
            new List<float> {30f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
            new List<float> {40f+start , 1f,1f,-1f,-1f,1f,1f,-1f,-1f },
            new List<float> {50f+start , -1f,-1f,1f,1f,-1f,-1f,1f,1f },
        };
        env.Aimant.magnetTab = magnetTab;

        //0 > 10 grandir et revenir + zone en spiral
        for ( int i = 1; i < 6; i++)
        { env.geometryLine((float)(i - 1), i, 0, 0.17f);}
        for (int i = 4; i > 0; i--)
        { env.geometryLine((float)(9-i), i, 0, 0.17f); }
        env.zoneSpiral(0.5f, 0f, 10f);
        //10 > 20 poussire + traqueur
        env.targetTab = new List<float>() { 10f , 20f , 1f , 40f, 50f, 0.01f };
        //env.targetTab = new List<float>() { 10f + start, 20f + start, 1f , 40f+start, 50f+start, 0.01f };
        for (int i = 0; i < 20; i++)
        {
            env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            //env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            //env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            //env.geometryGenerator(10f + (float)i * 0.5f, UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
        }
        //20 > 30 double grow and back
        for (int i = 1; i < 6; i++)
        { 
            env.geometryLine((float)(20+i - 1), i, 0, 0.17f); 
            env.geometryLine((float)(20+i - 1), i, 0, -0.17f); 
        }
        for (int i = 4; i > 0; i--)
        { 
            env.geometryLine((float)(20+9 - i), i, 0, 0.17f); 
            env.geometryLine((float)(20+9 - i), i, 0, -0.17f); 
        }
        //30 > 40 aleatoire à lancienne
        env.randomGeneration(30f, 40f, 0.2f);
        env.randomGeneration(30f, 40f, 0.4f);
        env.randomGeneration(30f, 40f, 0.8f);
        env.randomGeneration(30f, 40f, 1f);
        env.randomGeneration(20f, 40f, 1.5f);
        //40>50 ultra traqueur
        //50 > 60
        env.geometryBalayage(50f, 53f, 0.5f, 2, 0, 0.15f);
        env.geometryBalayage(53f, 56f, 0.5f, 3, 0, 0.15f);
        env.geometryBalayage(56f, 59f, 0.5f, 4, 0, 0.15f);

        

        foreach (oObstacle obs in env.listObs) { obs.apparitionTime += start; }
    }

    float haha() { return 1f - 2f * (float)UnityEngine.Random.Range(0, 2); }

    void lvl4()
    {
        float start = 3f;
        env.maxTime = 65f;
        env.startTime = start;
        env.speedFactor = 1.0f;
        env.maxScore = 100f;
        env.scoreBonusTime = 1.0f;
        //reinit
        oTimer.tps = 0;
        //etc..

        //empeche les probleme d'instanciation
        env.initialisation();
        env.circleGeneration(10f);

        

        List<List<float>> magnetTab = new List<List<float>>() {
            new List<float> {0f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
            new List<float> {10f+start , 1f,0f,1f,0f,1f,0f,1f,0f },
            new List<float> {15f+start , 0f,-1f,0f,-1f,0f,-1f,0f,-1f },
            new List<float> {20f+start , haha(),haha(),haha(),haha(),haha(),haha(),haha(),haha() },
            new List<float> {30f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
            new List<float> {40f+start , -1f,1f,-1f,1f,-1f,1f,-1f,1f },
            new List<float> {50f+start , 0f,0f,0f,0f,0f,0f,0f,0f },
        };
        env.Aimant.magnetTab = magnetTab;

        // 0 > 10 aleatoire 
        env.randomGeneration(0f, 10f, 0.2f);
        env.randomGeneration(0f, 10f, 0.4f);
        env.randomGeneration(0f, 10f, 0.8f);
        env.randomGeneration(0f, 10f, 0.2f);
        env.randomGeneration(0f, 10f, 0.4f);
        env.randomGeneration(0f, 10f, 0.8f);
        //10 > 20
        env.geometryBalayage(10f, 13f, 0.5f, 3, 0, 0.15f);
        env.geometryBalayage(13f, 16f, 0.5f, 3, 0, 0.15f);
        env.geometryBalayage(16f, 19f, 0.5f, 3, 0, 0.15f);
        //20 > 30
        for (int i = 0; i < 10; i++)
        {
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
            env.geometryGenerator(20f + (float)i , UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(0.5f, 3.5f), UnityEngine.Random.Range(0f, 3.141f));
        }
        //30 > 40
        for (int i = 0; i < 10; i++)
        { env.geometryLine(30f + (float)i, i + 1, (float)i, 0.1f); }
        env.targetTab = new List<float>() { 30f + start, 40f + start, 1f , 50f + start, 60f + start, 0.5f };
        // 40 > 50 aleatoire 
        env.randomGeneration(40f, 50f, 0.3f);
        env.randomGeneration(40f, 50f, 0.5f);
        env.randomGeneration(40f, 50f, 0.7f);
        env.randomGeneration(40f, 50f, 0.9f);
        env.randomGeneration(40f, 50f, 0.3f);
        env.randomGeneration(40f, 50f, 0.5f);
        env.randomGeneration(40f, 50f, 0.7f);
        env.randomGeneration(40f, 50f, 0.9f);
        //50 > 60
        env.zoneSpiral(0.25f, 50f, 60f);
        env.randomGeneration(50f, 60f, 0.1f);


        foreach (oObstacle obs in env.listObs) { obs.apparitionTime += start; }
    }

    void levelEndtest() {
      float start = 3f;
      env.maxTime = 5f;
      env.startTime = start;
      env.speedFactor = 1.0f;
      env.maxScore = 100f;
      env.scoreBonusTime = 1.0f;
      //reinit
      oTimer.tps = 0;
      //etc..

      //empeche les probleme d'instanciation
      env.initialisation();
    }
}
