using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class oEnv : MonoBehaviour
{
    public oParticule Particule;
    public static oTimer Timer;
    public oTuyau Tuyau;
    public oJauge Jauge;
    public oMusic Music;

    public GameObject Main;

    public oObstacle test;
    public List<oObstacle> listObs = new List<oObstacle>();
    public List<oObstacle> listObsSuperpos = new List<oObstacle>();

    public float score = 0f;
    public int compteur = 0;
    public float maxTime = 60f;
    public float startTime = 3f;

    public List<GameObject> listAimants = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        //ajout des scripts
        Main = new GameObject();
        Particule = Main.AddComponent<oParticule>();
        Timer = Main.AddComponent<oTimer>();
        Tuyau = Main.AddComponent<oTuyau>();
        Jauge = Main.AddComponent<oJauge>();
        Music = Main.AddComponent<oMusic>();

        //pose des obstacles
        randomGeneration( startTime, maxTime, 0.4f);
        randomGeneration( startTime, maxTime, 0.4f);
        
        //set de la jauge (en fonction du score max)
        Jauge.max = maxTime;
        Jauge.current = 0f;

        designWow();
        aimants();
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
        //gestion du score
        if (oTimer.tps < maxTime && oTimer.tps > startTime) { score += Time.deltaTime; }


        demarrageObstacles();
        particleGetHit();
        destroyObstacle();
        activeCircle();

        //tmpFunction();
        tmpAltAimants();
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
        /* foreach( oObstacle obst in listObs )
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
         }*/
        overlaping();
        foreach (oObstacle obst in listObsSuperpos)
        {
            if( obst.normalizeSized() > 0.95f && !obst.hit)
            {
                obst.hit = true;
                //compteur += 1;
                score -= 1;
                Music.playMe("hit");
            }
        }
    }

    public void destroyObstacle()
    {

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
        //generation obstacles
        int nb = (int)( (totalTime - startTime) / interval);
        for( int i=0 ;i<nb ; i++)
        {
            oObstacle tmp = Main.AddComponent<oObstacle>();

            float tmpR = UnityEngine.Random.Range(0.5f, 2f);
            float tmpA = UnityEngine.Random.Range(0f, 3.141f*2f);

            float tmpX = tmpR * (float)(Math.Cos(tmpA));
            float tmpY = tmpR * (float)(Math.Sin(tmpA));

            //tmp.alloc(startTime + i*interval + Random.Range(-0.25f*interval, 0.25f*interval), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 4f + Random.Range(-1.0f, 1.0f), "football");
            tmp.alloc(startTime + i*interval + UnityEngine.Random.Range(-0.25f*interval, 0.25f*interval), tmpX, tmpY, 1f + UnityEngine.Random.Range(-0.2f, 0.2f), "rond");
            listObs.Add(tmp);
        }
        //generation cercle (on va dire 1 par seconde pour le moment)
        List<float> tmpList = new List<float>();
        for( int i = (int)startTime +1 ; i < (int)totalTime ; i++)
        {
            tmpList.Add((float)i);
        }
        Tuyau.listRed = tmpList;
    }

    void OnGUI()
    {
        if ( oTimer.tps < maxTime && oTimer.tps > startTime)
        {
            //float score = oTimer.tps - (float)compteur - startTime;
            GUI.Label(new Rect(15, 300, 2500, 1000), "score = " + score);
            Jauge.current = score;
        }
        /*overlaping();
        GUI.Label(new Rect(15, 300, 2500, 1000), "Over Lap : " + listObsSuperpos.Count);*/
    }

    public void overlaping()
    {
        listObsSuperpos = new List<oObstacle>();
        List<Collider2D> tmp = new List<Collider2D>();
        Particule.particule.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D(), tmp);

        foreach( Collider2D collider in tmp )
        {
            foreach (oObstacle obstacle in listObs)
            {
                if (collider.gameObject == obstacle.obs)
                {
                    listObsSuperpos.Add(obstacle);
                }
            }
        }
    }

    public void activeCircle()
    {
        if( Tuyau.activatedOnEdge()  && Input.GetKeyDown(KeyCode.Space) )
        {
            score += 3;
            print("nice");
        }
    }

    public void designWow()
    {
        GameObject ecran = new GameObject();
        ecran.AddComponent<SpriteRenderer>();
        ecran.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("écran") as Sprite;
        ecran.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        ecran.transform.position = new Vector3(0.0f, -0.4f, 2.0f);

        GameObject bar1 = new GameObject();
        bar1.AddComponent<SpriteRenderer>();
        bar1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Jauge_droite_avec") as Sprite;
        bar1.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        bar1.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        GameObject bar2 = new GameObject();
        bar2.AddComponent<SpriteRenderer>();
        bar2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Jauge_haut") as Sprite;
        bar2.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        bar2.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        GameObject lignes = new GameObject();
        lignes.AddComponent<SpriteRenderer>();
        lignes.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("lignes") as Sprite;
        lignes.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        lignes.transform.position = new Vector3(0.0f, -0.364f, 1.9f);

        GameObject bordure = new GameObject();
        bordure.AddComponent<SpriteRenderer>();
        bordure.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Polygone_c") as Sprite;
        bordure.transform.localScale = new Vector3(5f, 5f, 0);
        bordure.transform.position = new Vector3(0.0f, 0.0f, -1f);
    }

    public void aimants()
    {
        ///listAimants
        //nord
        GameObject nord = new GameObject();
        nord.AddComponent<SpriteRenderer>();
        nord.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        nord.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        nord.transform.position = new Vector3(0.02f, 2.473f, 0.0f);
        nord.transform.rotation = Quaternion.Euler(0, 0, 22.5f);
        listAimants.Add(nord);
        //nord est
        GameObject nordEst = new GameObject();
        nordEst.AddComponent<SpriteRenderer>();
        nordEst.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        nordEst.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        nordEst.transform.position = new Vector3(1.768f, 1.768f, 0.0f);
        nordEst.transform.rotation = Quaternion.Euler(0, 0, 337.5f);
        listAimants.Add(nordEst);
        //est
        GameObject est = new GameObject();
        est.AddComponent<SpriteRenderer>();
        est.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        est.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        est.transform.position = new Vector3(2.47f, 0.02f, 0.0f);
        est.transform.rotation = Quaternion.Euler(0, 0, 292.5f);
        listAimants.Add(est);
        //sud est
        GameObject sudEst = new GameObject();
        sudEst.AddComponent<SpriteRenderer>();
        sudEst.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        sudEst.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        sudEst.transform.position = new Vector3(1.71f, -1.739f, 0.0f);
        sudEst.transform.rotation = Quaternion.Euler(0, 0, 247.5f);
        listAimants.Add(sudEst);
        //sud
        GameObject sud = new GameObject();
        sud.AddComponent<SpriteRenderer>();
        sud.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        sud.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        sud.transform.position = new Vector3(-0.029f, -2.437f, 0.0f);
        sud.transform.rotation = Quaternion.Euler(0, 0, 202.5f);
        listAimants.Add(sud);
        //sud ouest
        GameObject sudOuest = new GameObject();
        sudOuest.AddComponent<SpriteRenderer>();
        sudOuest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        sudOuest.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        sudOuest.transform.position = new Vector3(-1.769f, -1.7f, 0.0f);
        sudOuest.transform.rotation = Quaternion.Euler(0, 0, 157.5f);
        listAimants.Add(sudOuest);
        //ouest
        GameObject ouest = new GameObject();
        ouest.AddComponent<SpriteRenderer>();
        ouest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        ouest.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        ouest.transform.position = new Vector3(-2.467f, 0.046f, 0.0f);
        ouest.transform.rotation = Quaternion.Euler(0, 0, 112.5f);
        listAimants.Add(ouest);
        //nord ouest
        GameObject nordouest = new GameObject();
        nordouest.AddComponent<SpriteRenderer>();
        nordouest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        nordouest.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        nordouest.transform.position = new Vector3(-1.735f, 1.762f, 0.0f);
        nordouest.transform.rotation = Quaternion.Euler(0, 0, 67.5f);
        listAimants.Add(nordouest);
    }

    public void tmpAltAimants()
    {
        for( int i = 0; i < 4; i++)
        {
            if( (int)oTimer.tps % 2 == 0 )
            {
                listAimants[2*i].GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
                listAimants[2*i+1].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1f);
            }
            else
            {
                listAimants[2 * i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1f);
                listAimants[2 * i+1].GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
            }
        }
    }

    void tmpFunction()
    {
        if( oTimer.tps > 10f && Tuyau.activated == null )
        {
            Tuyau.activation( Color.green);
        }
    }

}
