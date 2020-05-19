using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading;
using System;

public class oJauge : MonoBehaviour
{
    public GameObject jauge;
    public GameObject mask;

    public float height = 1.286f;
    public float min = 0f;
    public float max = 1f;
    public float current = 0.5f;
    public float vitesse = 1f;

    //jauge de temps
    public GameObject jaugeTps;
    float largeur = 1.06f;
    public float startTime = 0f;
    public float endTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        jauge = new GameObject();
        jauge.AddComponent<SpriteRenderer>();
        jauge.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rainbow") as Sprite;
        jauge.transform.localScale = new Vector3(height, 0.448f, 0);
        jauge.transform.position = new Vector3(3.3f, 3.08f, 1);
        jauge.transform.rotation = Quaternion.Euler(0,0,90f);
        jauge.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        /* float ratio;
         ratio = 1 / jauge.GetComponent<SpriteRenderer>().size.x;
         ratio *= 0.5f;
         particule.transform.localScale = new Vector3(ratio, ratio, ratio);*/

        mask = new GameObject();
        mask.AddComponent<SpriteMask>();
        mask.GetComponent<SpriteMask>().sprite = Resources.Load<Sprite>("rainbow") as Sprite;
        mask.transform.localScale = new Vector3(height, 0.448f, 0);
        mask.transform.position = new Vector3(3.3f, 3.08f, 1);
        mask.transform.rotation = Quaternion.Euler(0, 0, 90f);


        jaugeTps = new GameObject();
        jaugeTps.AddComponent<SpriteRenderer>();
        jaugeTps.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rectWhite") as Sprite;
        jaugeTps.transform.localScale = new Vector3(0, 0.15f, 0);
        jaugeTps.transform.position = new Vector3(-1.97f, 3.177f, 1f);
        jaugeTps.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        evolOverTime();
        gestionTemps();
        
    }

    void evolOverTime()
    {
        float quotient = height * (1- current / (max - min));
        quotient -= mask.transform.localScale.x;
        mask.transform.localScale += new Vector3(quotient*vitesse* Time.deltaTime, 0, 0);

    }

    void gestionTemps()
    {
        //jauge temps
        if (oTimer.tps - startTime > 0 && oTimer.tps - endTime < 0)
        {
            float scaling = largeur * (oTimer.tps - startTime) / (endTime - startTime);
            jaugeTps.transform.localScale = new Vector3(scaling, 0.15f, 0);
        }
        /*
        if (oTimer.tps - startTime > endTime * 2f/3f)
        { jaugeTps.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f); }
        else if (oTimer.tps - startTime > endTime / 3f)
        { jaugeTps.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f); }
        else if (oTimer.tps - startTime > 0f)
        { jaugeTps.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f); }*/
        if (oTimer.tps > endTime * 2f / 3f)
        { 
            jaugeTps.GetComponent<SpriteRenderer>().color += new Color(0f, -Time.deltaTime * 3f / endTime,  0f);
        }
        else if (oTimer.tps  >  endTime / 3f)
        { 
            jaugeTps.GetComponent<SpriteRenderer>().color += new Color( Time.deltaTime* 3f / endTime, 0f, 0f); 
        }
        else if (oTimer.tps > 0f)
        {
            jaugeTps.GetComponent<SpriteRenderer>().color += new Color(0f, Time.deltaTime * 3f / endTime, 0f);
        }


    }
}
