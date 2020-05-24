using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Cryptography;
using UnityEngine.UI;
using System.Collections.Specialized;

public class GenerationObs : MonoBehaviour
{
    public oParticule Particule;
    public static oTimer Timer;
    public oTuyau Tuyau;
    public oAimant Aimant;

    public GameObject Main;

    public List<oObstacle> listObs = new List<oObstacle>();
    //public List<float> listCircle = new List<float>();

    public float maxTime = 70f; //60
    public float startTime = 1f;

    public float chronoTarget = 0f;

    // Start is called before the first frame update
    public void Start()
    {
        //ajout des scripts
        Main = new GameObject();
        Timer = Main.AddComponent<oTimer>();
        chronoTarget = startTime;

        InitializeObst();

        //test geometry
        /*
        for (int i = 2; i < 10; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                geometryGenerator((float)i, i, (float)j*0.15f, (float)j*0.1f);
            }
        }*/
    }

    void InitializeObst() {

      //pose des obstacles
      // >> pour linstant manuel, mais à initialiser depuis le createur de niveau (?)
      float intervalTest = 0.5f;
      randomGeneration( startTime, maxTime-10f, intervalTest);
      randomGeneration( startTime, maxTime-10f, intervalTest);
      //randomGeneration( startTime, maxTime, intervalTest);
      randomGeneration( maxTime/3f, maxTime, intervalTest);
      //randomGeneration(maxTime / 3f, maxTime, intervalTest);
      //randomGeneration( 2f* maxTime / 3f, maxTime, intervalTest);
      randomGeneration( 2f* maxTime / 3f, maxTime-10f, intervalTest);
      //test
      zoneGeneration(startTime, maxTime-10f, 3f);
      //circleGeneration(5f);
    }

    public oTuyau listCircle(oTuyau Tuyau){
      return circleGeneration(5f, Tuyau);
    }

    //alloc(float at, float fx, float fy, float fr, string im)
    void randomGeneration( float firstTime , float totalTime , float interval )
    {
        //generation obstacles
        int nb = (int)( (totalTime - firstTime -3f) / interval);
        for( int i=0 ;i<nb ; i++)
        {
            oObstacle tmp = Main.AddComponent<oObstacle>();

            float tmpR = UnityEngine.Random.Range(0.7f, 3.0f);
            float tmpA = UnityEngine.Random.Range(0f, 3.141f*2f);

            float tmpX = tmpR * (float)(Math.Cos(tmpA));
            float tmpY = tmpR * (float)(Math.Sin(tmpA));

            //tmp.alloc(startTime + i*interval + Random.Range(-0.25f*interval, 0.25f*interval), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 4f + Random.Range(-1.0f, 1.0f), "football");
            tmp.alloc( firstTime + i*interval + UnityEngine.Random.Range(-0.25f*interval, 0.25f*interval), tmpX, tmpY, 1.2f + UnityEngine.Random.Range(-0.2f, 0.2f), "rond");
            listObs.Add(tmp);
        }

    }

    List<float> zoneAngles =  new List<float>{ 0f, 90f, 180f, 270f };
    List<float> zoneX = new List<float>{ -1.6f, 1.6f , 1.6f ,-1.6f};
    List<float> zoneY = new List<float>{ -1.6f, -1.6f , 1.6f, 1.6f};

    public void zoneGeneration(float firstTime, float totalTime, float interval)
    {
        int nb = (int)((totalTime - firstTime - 3f) / interval);
        for (int i = 0; i < nb; i++)
        {
            //int taille = (1 + UnityEngine.Random.Range(0, 2));
            //int taille = 2;
            int which = UnityEngine.Random.Range(0, 4);

            oZone tmp = Main.AddComponent<oZone>();
            float tmpX = zoneX[which];
            float tmpY = zoneY[which];
            tmp.alloc((float)( firstTime + i * interval), tmpX, tmpY, 0.7f, "Obstacle_2");
            tmp.rotate(zoneAngles[which]);
            listObs.Add(tmp);
        }
    }


    public void obsTraqueurs( float interval)
    {
        if (chronoTarget < oTimer.tps )
        {
            chronoTarget += interval;
            oObstacle tmp = Main.AddComponent<oObstacle>();
            tmp.alloc(oTimer.tps + 0.5f, Particule.x, Particule.y, 1f + UnityEngine.Random.Range(-0.2f, 0.2f), "rond");
            listObs.Add(tmp);
        }
    }

    public void geometryGenerator( float time , int polygone, float rayon, float angle  )
    {
        float angleFix = 2f * 3.141f / (float)polygone;
        for( int i = 0 ; i < polygone ; i++)
        {
            float tmpX = rayon * (float)(Math.Cos( (float)i * angleFix + angle ));
            float tmpY = rayon * (float)(Math.Sin( (float)i * angleFix + angle ));

            oObstacle tmp = Main.AddComponent<oObstacle>();
            tmp.alloc( time, tmpX, tmpY, 0.5f , "rond");
            listObs.Add(tmp);
        }
    }

    public oTuyau circleGeneration( float tempoCercle , oTuyau Tuyau )
    {
        //generation cercle (on va dire 5 par seconde pour le moment)
        List<float> tmpList = new List<float>();
        //float tempoCercle = 5.0f;
        for (int i = 1; i < (int)(maxTime / tempoCercle); i++)
        {
            tmpList.Add((float)i * tempoCercle + startTime);
        }
        Tuyau.listRed = tmpList;
        return Tuyau;
    }
}
