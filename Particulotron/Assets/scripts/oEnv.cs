using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Cryptography;
using UnityEngine.UI;
using System.Collections.Specialized;

public class oEnv : MonoBehaviour
{
    public oParticule Particule;
    public static oTimer Timer;
    public oTuyau Tuyau;
    public oJauge Jauge;
    public oMusic Music;
    public oAimant Aimant;
    public SceneChanger sceneChanger;
    public GenerationObs generationObs;

    public GameObject Main;
    public GameObject Score;
    public GameObject Scoretext;
    public GameObject CaraText;

    public oObstacle test;
    //public List<oObstacle> listObs = new List<oObstacle>();
    public List<oObstacle> listObsSuperpos = new List<oObstacle>();
    public List<oObstacle> listObs = new List<oObstacle>();

    public float score = 0f;
    public int compteur = 0;
    public float maxTime = 70f; //70
    public float startTime = 1f;

    public float chronoTarget = 0f;
    
    public List<float> targetTab = new List<float>();

    //modif vitesse
    public float speedFactor = 1.0f;

    //constante de score (à equilibré)
    public float maxScore = 100f;
    public float scoreMalusObs = 1f;
    public float scoreBonusCircle = 3f;
    public float scoreBonusTime = 1f;

    //poubelle
    List<GameObject> corbeille = new List<GameObject>();
    // Start is called before the first frame update
    public void Start() { }

    public void initialisation()
    {
        //ajout des scripts
        Main = new GameObject();
        Score = new GameObject();
        Scoretext = GameObject.Find("Scoretext");
        CaraText = GameObject.Find("CaraText");
        Particule = Main.AddComponent<oParticule>();
        Timer = Main.AddComponent<oTimer>();
        Tuyau = Main.AddComponent<oTuyau>();
        Jauge = Main.AddComponent<oJauge>();
        Music = Main.AddComponent<oMusic>();
        Aimant = Main.AddComponent<oAimant>();
        sceneChanger = Main.AddComponent<SceneChanger>();
        Aimant.Part = Particule;
        chronoTarget = startTime;

        //reinit
        oTimer.tps = 0;

        //pose des obstacles
        // >> pour linstant manuel, mais à initialiser depuis le createur de niveau (?)
        /*
        generationObs = Main.AddComponent<GenerationObs>();

        listObs = generationObs.listObs; //Obstacles
        Tuyau = generationObs.listCircle(Tuyau); //Circle
        */

        //ZONE DE TEST
        /*
        for (int i = 0; i < 5; i++)
        {
            geometryLine((float)i, 3, 0f, 0.05f*(float)i);
        }*/
        //randomGeneration(0f, 30f, 0.5f);

       // geometryBalayage(1f, 31f, 0.2f, 3, 0, 0.01f);

       circleGeneration(5f);


        //set de la jauge (en fonction du score max)
        Jauge.max = maxScore;
        Jauge.current = 0f;
        Jauge.startTime = startTime;
        Jauge.endTime = maxTime;

        //decor

        //Score
        Scoretext.SetActive(false);
        Score.AddComponent<SpriteRenderer>();
        designWow();
        //aimants();

        //AddOn
        Aimant.speed = speed;
        Particule.degrade = degrade;
        //affichageScore();
        affichageCara();
    }

    // Update is called once per frame
    public void Update()
    {

        //gestion du score + chose sur timer
        if (oTimer.tps < maxTime && oTimer.tps > startTime)
        {
            score += Time.deltaTime*scoreBonusTime;
            //linearSpeedChange();
            proportionalSpeedChange();
            tourni();
            //!!!!!!!!!!!
           // obsTraqueurs(1f);
        }/*
        if (oTimer.tps < maxTime-3  && oTimer.tps > maxTime-10f)
        {
            score += Time.deltaTime * scoreBonusTime*2f;
            obsTraqueurs(0.001f);
        }*/


        demarrageObstacles();
        particleGetHit();
        destroyObstacle();
        activeCircle();
        manageTraqueur();

        //addMagnetOverTime();

        //test destruction
        //if( oTimer.tps > maxTime+startTime) { deleteAll(); }

        //Affichage du score
        if (oTimer.tps > maxTime) {
          Scoretext.SetActive(true);
          affichageScore();
          restartorMenu();
        }
    }

    public void demarrageObstacles()
    {
        //foreach (oObstacle obst in listObs)
        foreach (oObstacle obst in listObs)//.GetRange(0, Math.Min(1000,listObs.Count)))
        {
            if (!obst.started && oTimer.tps > obst.apparitionTime)
            {
                obst.demarrage();
            }
        }
    }

    public void particleGetHit()
    {
        overlaping();
        foreach (oObstacle obst in listObsSuperpos)
        {
            if( obst.normalizeSized() > 0.95f && !obst.hit && !(Particule.invincible > 0f))
            {
                obst.hit = true;
                score -= scoreMalusObs;
                Music.playMe("hit");
                Particule.invincible = 0.2f;
            }
        }
    }

    public void destroyObstacle()
    {
        listObs.RemoveAll(item => item == null);
        /*
        List<int> listIndex = new List<int>();
        for (int i = 0; i < listObs.Count; i++)
        {
            if (listObs[i].started && listObs[i].normalizeSized() > 1.0f)
            {
                listIndex.Add(i);
            }
        }

        listIndex.Sort();
        listIndex.Reverse();

        foreach (int i in listIndex)
        {

            //Destroy(listObs[i].obs);
            Destroy(listObs[i]);

            listObs.RemoveAt(i);
        }
        */
    }

    public void obsTraqueurs( float interval)
    {
        if ( chronoTarget < oTimer.tps )
        {
            chronoTarget += interval;
            oObstacle tmp = Main.AddComponent<oObstacle>();
            tmp.alloc(oTimer.tps + 0.5f, Particule.x, Particule.y, 1f + UnityEngine.Random.Range(-0.2f, 0.2f), "rond");
            listObs.Add(tmp);
        }
    }

    // 0 = start 1 = end 2 = frq
    public void manageTraqueur()
    {
        if(targetTab.Count > 0)
        { 
        if (oTimer.tps > targetTab[0])
        {
            obsTraqueurs(targetTab[2]);
        }

        if (oTimer.tps > targetTab[1])
        {
            targetTab.RemoveAt(2);
            targetTab.RemoveAt(1);
            targetTab.RemoveAt(0);
        }

        }
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
        if (Input.GetKeyDown(KeyCode.Space) && !Tuyau.fail)
        {
            if (Tuyau.activatedOnEdge())
            {
                score += scoreBonusCircle;
                Music.playMe("boost");
                Aimant.chronoGreen = oTimer.tps;

            }
            else if( Tuyau.activated != null && Tuyau.activated.transform.localScale.x/5.0f > 0.05)
            {
                Tuyau.activated.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f, 1f);
                Tuyau.fail = true;
                Music.playMe("null");
                Aimant.chronoPurple = oTimer.tps;
            }

        }
    }

    void restartorMenu() {
      if(Input.GetKey(KeyCode.M)) {
        sceneChanger.Crafting();
      }
    }

    public void affichageScore() {
      //GameObject Score = new GameObject();
      Score.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("écran_clean") as Sprite;
      Score.transform.localScale = new Vector3(0.75f, 0.75f, 0);
      Score.transform.position = new Vector3(0f, -1f, 3f);;
      Score.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.7f);
      Score.GetComponent<SpriteRenderer>().sortingOrder = 2;

      //Scoretext.GetComponentsInChildren<Text>().text = "Score\n" + score.ToString() + "\n\n\nM to Menu";
      foreach( Text txt in Scoretext.GetComponentsInChildren<Text>())
        {
          double scoreint = Math.Round(score*1000);
          txt.text = "Score\n\n" + scoreint.ToString() + "\n\nPress M to Menu";
        }

    }

    public void affichageCara()
    {
        foreach (Text txt in CaraText.GetComponentsInChildren<Text>())
        {
            txt.text = "Q : " + 1.ToString() + "\n\nS : " + 0.ToString() + "\n\nC : " + 0.ToString() + "\n\nB : " + 0.ToString();
        }
    }

    public void designWow()
    {
        /*GameObject ecran = new GameObject();
        ecran.AddComponent<SpriteRenderer>();
        ecran.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("écran_clean") as Sprite;
        ecran.transform.localScale = new Vector3(0.75f, 0.75f, 0);
        ecran.transform.position = new Vector3(0.0f, -1f, 2.0f);*/

        GameObject bar1 = new GameObject();
        bar1.AddComponent<SpriteRenderer>();
        bar1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Jauge_droite_avec") as Sprite;
        bar1.transform.localScale = new Vector3(0.75f, 0.75f, 0);
        bar1.transform.position = new Vector3(0.0f, -1.0f, 0.0f);

        GameObject bar2 = new GameObject();
        bar2.AddComponent<SpriteRenderer>();
        bar2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Jauge_haut") as Sprite;
        bar2.transform.localScale = new Vector3(0.75f, 0.75f, 0);
        bar2.transform.position = new Vector3(0.0f, -0.5f, 0.0f);

        GameObject lignes = new GameObject();
        lignes.AddComponent<SpriteRenderer>();
        lignes.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("lignes") as Sprite;
        lignes.transform.localScale = new Vector3(0.69f, 0.69f, 0);
        lignes.transform.position = new Vector3(0.0f, -0.5f, 1.9f);
        lignes.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        /*GameObject*/ bordure = new GameObject();
        bordure.AddComponent<SpriteRenderer>();
        bordure.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Polygone_c") as Sprite;
        bordure.transform.localScale = new Vector3(7f, 7f, 0);
        bordure.transform.position = new Vector3(0.0f, 0.0f, -1f);

        GameObject bordure2 = new GameObject();
        bordure2.AddComponent<SpriteRenderer>();
        bordure2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Polygone_c") as Sprite;
        bordure2.transform.localScale = new Vector3(1f, 1f, 0);
        bordure2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
        //bordure2.transform.position = new Vector3(0.0f, 0.0f, -1f);

        //mask
        GameObject mask = new GameObject();
        mask.AddComponent<SpriteMask>();
        mask.GetComponent<SpriteMask>().sprite = Resources.Load<Sprite>("Particule_blanche") as Sprite;
        mask.GetComponent<SpriteMask>().alphaCutoff = 0.3f;

        //speed lines
        speed = new GameObject();
        speed.AddComponent<SpriteRenderer>();
        speed.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("speed") as Sprite;
        speed.transform.localScale = new Vector3(1.814f, 1.24f, 0);
        speed.transform.position = new Vector3(-0.031f, -0.031f, 0.0f);
        speed.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        //bordure dégradée
        degrade = new GameObject();
        degrade.AddComponent<SpriteRenderer>();
        degrade.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bordures_grad2") as Sprite;
        degrade.transform.localScale = new Vector3(4.0334f, 3.49f, 0);
        degrade.transform.position = new Vector3(-0.058f, -0.025f, 0.0f);
        degrade.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
    }

    public GameObject speed;
    public GameObject bordure;
    public GameObject degrade;


    public void tourni()
    {
        bordure.transform.rotation = Quaternion.Euler(0, 0, 360f / maxTime * oTimer.tps* oTimer.tps);
    }

    void tmpFunction()
    {
        if( oTimer.tps > 10f && Tuyau.activated == null )
        {
            Tuyau.activation( Color.green);
        }
    }

    public void linearSpeedChange()
    {
        oObstacle.vitesse += speedFactor * 0.5f * Time.deltaTime / maxTime;
        Tuyau.vitesseEvol += speedFactor * 2f * Time.deltaTime / maxTime;
    }

    public void proportionalSpeedChange()
    {
        float factor = (1 + speedFactor * score / maxScore);//(maxTime - startTime));
        oObstacle.vitesse = factor * 0.5f;
        Tuyau.vitesseEvol = factor * 2f;
    }

    bool add1 = true;
    bool add2 = true;
    public void addMagnetOverTime()
    {
        if ( add1 && oTimer.tps > maxTime * 0.5)
        {
            Aimant.listCharge[1] = 1;
            Aimant.listCharge[5] = -1;
            add1 = false;
        }
        if (add2 && oTimer.tps > maxTime * 0.75)
        {
            Aimant.listCharge[2] = 1;
            Aimant.listCharge[6] = -1;
            add2 = false;
        }
    }

    /*public void deleteAll()
    {
        /*foreach( GameObject o in GameObject.FindObjectOfType<GameObject>())
        {
            Destroy(o);
        }

        var tab = GameObject.FindObjectOfType<GameObject>();
        foreach (GameObject go in tab)
        {
            tab.Remove(go); // else there will be pointer to null
            GameObject.Destroy(go);

        }
    }*/


    //alloc(float at, float fx, float fy, float fr, string im)
    public void randomGeneration( float firstTime , float totalTime , float interval )
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

    public void circleGeneration( float tempoCercle )
    {
        //generation cercle (on va dire 5 par seconde pour le moment)
        List<float> tmpList = new List<float>();
        //float tempoCercle = 5.0f;
        for (int i = 1; i < (int)(maxTime / tempoCercle); i++)
        {
            tmpList.Add((float)i * tempoCercle + startTime);
        }
        Tuyau.listRed = tmpList;
    }

    public void geometryGenerator(float time, int polygone, float rayon, float angle)
    {
        float angleFix = 2f * 3.141f / (float)polygone;
        for (int i = 0; i < polygone; i++)
        {
            float tmpX = rayon * (float)(Math.Cos((float)i * angleFix + angle));
            float tmpY = rayon * (float)(Math.Sin((float)i * angleFix + angle));

            oObstacle tmp = Main.AddComponent<oObstacle>();
            tmp.alloc(time, tmpX, tmpY, 0.5f, "rond");
            listObs.Add(tmp);
        }
    }

    public void geometryLine( float time, int polygone , float angle , float coubure )
    {
        for( int i = 0; i < 14;i++)
        {
            geometryGenerator(time, polygone, /*0.15f*/ 0.25f * (float)i, angle + coubure * (float)i);
        }
    }

    public void geometryBalayage( float timeMin ,  float timeMax , float interval , int polygone, float angle, float coubure )
    {
        int nb = (int)( (timeMax-timeMin)/interval );
        float alpha = 2f * 3.141f / nb;
        for( int i = 0; i < nb; i++)
        {
            geometryLine( timeMin+(float)i*interval , polygone, angle+i*alpha , -coubure  );
        }
    }
}
