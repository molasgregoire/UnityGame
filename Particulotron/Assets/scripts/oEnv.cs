using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class oEnv : MonoBehaviour
{
    public oParticule Particule;
    public static oTimer Timer;
    public oTuyau Tuyau;

    public GameObject Main;

    public oObstacle test;
    public List<oObstacle> listObs = new List<oObstacle>();

    public int compteur = 0;

    // Start is called before the first frame update
    public void Start()
    {

        Main = new GameObject();
        Particule = Main.AddComponent<oParticule>();
        Timer = Main.AddComponent<oTimer>();
        Tuyau = Main.AddComponent<oTuyau>();

        randomGeneration( 3.0f, 60.0f, 1.0f);

        /*test = Main.AddComponent<oObstacle>();
        test.alloc(0.5f,3f,0f,2f,"football");*/
        /*
        oObstacle test1 = Main.AddComponent<oObstacle>();
        test1.alloc(0.5f, 3f, 0f, 2f, "football");
        oObstacle test2 = Main.AddComponent<oObstacle>();
        test2.alloc(1.5f, -3f, 0f, 2f, "football");
        oObstacle test3 = Main.AddComponent<oObstacle>();
        test3.alloc(2.5f, 0f, 3f, 2f, "football");
        oObstacle test4 = Main.AddComponent<oObstacle>();
        test4.alloc(3.5f, 0f, -3f, 2f, "football");

        listObs.Add(test1);
        listObs.Add(test2);
        listObs.Add(test3);
        listObs.Add(test4);*/
        /*
        print(listObs.Count);
        destroyObstacle();
        print(listObs.Count);
        */
        //how to random flaot
        //print(Random.Range(0.0f, 1.0f));

    }

    // Update is called once per frame
    public void Update()
    {
        /*
        if( !test.started  &&  oTimer.tps > test.apparitionTime  )
        {
            //print("vroum");
            test.demarrage();
        }
        particleGetHit();

        destroyObstacle();*/
        
        demarrageObstacles();
        particleGetHit();
        destroyObstacle();
    }

    public void demarrageObstacles()
    {
        foreach (oObstacle obst in listObs)
        {
            if (!obst.started && oTimer.tps > obst.apparitionTime)
            {
                obst.demarrage();
            }
        }
    }

    public void particleGetHit()
    {
        /*
        if (test.started && test.normalizeSized() > 0.95f)
        {
            float calcul = Mathf.Pow(Particule.x - test.obs.transform.position.x, 2) + Mathf.Pow(Particule.y - test.obs.transform.position.y, 2) - Mathf.Pow(test.finalRayon*test.ratio, 2);
            if( calcul < 0 )
            {
                print("BOOM");
            }
        }*/

        foreach( oObstacle obst in listObs )
        {
            if (obst.started && obst.normalizeSized() > 0.95f)
            {
                float calcul = Mathf.Pow(Particule.x - obst.obs.transform.position.x, 2) + Mathf.Pow(Particule.y - obst.obs.transform.position.y, 2) - Mathf.Pow(obst.finalRayon * obst.ratio, 2);
                if (calcul < 0 && !obst.hit)
                {
                    //print("BOOM");
                    obst.hit = true;
                    compteur += 1;
                }
            }
        }
    }

    public void destroyObstacle()
    {
        /*
        if ( test.normalizeSized() > 1.0f)
        {
            Destroy(test.obs);
            test = null;
        }*/
        //foreach (oObstacle obst in listObs)

        List<int> listIndex = new List<int>();
        for (int i = 0; i < listObs.Count; i++)
        {
            if (listObs[i].started && listObs[i].normalizeSized() > 1.0f)
            {
                listIndex.Add(i);
            }
        }

        foreach (int i in listIndex)
        {

            Destroy(listObs[i].obs);
            Destroy(listObs[i]);
            
            listObs.RemoveAt(i);
        }
        
    }

    //alloc(float at, float fx, float fy, float fr, string im)
    void randomGeneration( float startTime , float totalTime , float interval )
    {
        int nb = (int)( (totalTime - startTime) / interval);
        for( int i=0 ;i<nb ; i++)
        {
            oObstacle tmp = Main.AddComponent<oObstacle>();
            tmp.alloc(startTime + i + Random.Range(-0.25f*interval, 0.25f*interval), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 4f + Random.Range(-1.0f, 1.0f), "football");
            listObs.Add(tmp);
        }
    }
}
