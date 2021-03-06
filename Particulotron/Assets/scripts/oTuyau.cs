﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class oTuyau : MonoBehaviour
{
    //public GameObject neonTest;

    public List<GameObject> circles;
    public int nbCircles = 9;
    public float vitesseEvol = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        
        /*
        backCircle = new GameObject();
        backCircle.AddComponent<SpriteRenderer>();
        backCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
        float ratioC = 1 / backCircle.GetComponent<SpriteRenderer>().size.x;
        
        backCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        backCircle.GetComponent<SpriteRenderer>().color = new Color(209, 0, 250, 200);

        frontCircle = new GameObject();
        frontCircle.AddComponent<SpriteRenderer>();
        frontCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
        frontCircle.transform.localScale = new Vector3(1.1f, 1.1f, 0);
        frontCircle.GetComponent<SpriteRenderer>().color = new Color(209, 0, 250, 200);
        /*
        moveCircle = new GameObject();
        moveCircle.AddComponent<SpriteRenderer>();
        moveCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("circle") as Sprite;
        moveCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0);*/

        float dist = 1/ (nbCircles-1.0f);
        for( int i = 1; i < nbCircles ; i++ )
        {
            GameObject tmp = new GameObject();
            
            tmp.AddComponent<SpriteRenderer>();
            tmp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
            tmp.transform.localScale = new Vector3(i*dist,i*dist , 0);
            tmp.transform.position = new Vector3(0, 0, 0.5f);
            if (i % 2 == 0)
            {
                tmp.GetComponent<SpriteRenderer>().color = new Color(209, 0, 250, 0.8f);
            }
            //else { tmp.GetComponent<SpriteRenderer>().color = new Color(209, 0, 250, 0.8f); }
            circles.Add(tmp);
            
        }
        /*
        neonTest = new GameObject();
        neonTest.AddComponent<SpriteRenderer>();
        neonTest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonLine") as Sprite;
        neonTest.transform.localScale = new Vector3(0.45f, 0.5f, 1);
        neonTest.transform.position = new Vector3(0, 0, 1);*/
    }

    // Update is called once per frame
    void Update()
    {
        // growAndBack(moveCircle);
        for (int i = 0; i < nbCircles-1; i++)
        {
            //transparency(circles[i]);
            growAndBack(circles[i]);

            
        }
    }

    void growAndBack( GameObject subject )
    {
        if (subject.transform.localScale.x < 1.1f)
        {
            subject.transform.localScale += new Vector3(vitesseEvol, vitesseEvol, 0);

        }
        else
        {
            subject.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        }
    }

    void transparency(GameObject subject)
    {
        float size = subject.transform.localScale.x;
        float trans = (255* size) / 1.1f;
        subject.GetComponent<SpriteRenderer>().color += new Color(0,0,0, trans);
    }
}
