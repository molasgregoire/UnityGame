using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Linq;

public class oTuyau : MonoBehaviour
{
    //public GameObject neonTest;

    public List<GameObject> circles;
    public GameObject newest;
    public GameObject activated;

    public int nbCircles = 3;
    public float vitesseEvol = 2.0f;

    //tableau de tps dapparition pour cercle activable
    public List<float> listRed = new List<float>();
    public bool fail = false;

    // Start is called before the first frame update
    public void Start()
    {
        circles = new List<GameObject>();
        float dist = 7f*1f/ (nbCircles-1.0f);
        for( int i = 0; i < nbCircles-1 ; i++ )
        {
            GameObject tmp = new GameObject();
            
            tmp.AddComponent<SpriteRenderer>();
            //tmp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
            tmp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Polygone_c") as Sprite;
            //tmp.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);

            tmp.transform.localScale = new Vector3(i*dist,i*dist , 0);
            tmp.transform.position = new Vector3(0, 0, 0.5f);
            //if (i % 2 == 0)            {                tmp.GetComponent<SpriteRenderer>().color = new Color(209, 0, 250, 0.8f);            }
            //else { tmp.GetComponent<SpriteRenderer>().color = new Color(209, 0, 250, 0.8f); }
            circles.Add(tmp);
            
        }
        /*
        neonTest = new GameObject();
        neonTest.AddComponent<SpriteRenderer>();
        neonTest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonLine") as Sprite;
        neonTest.transform.localScale = new Vector3(0.45f, 0.5f, 1);
        neonTest.transform.position = new Vector3(0, 0, 1);*/

        //ontrie la liste pour avoir une list ordonnée
        listRed.Sort();
    }

    // Update is called once per frame
    public void Update()
    {
        // growAndBack(moveCircle);
        for (int i = 0; i < circles.Count; i++)
        {
            transparency(circles[i]);
            growAndBack(circles[i]);
        }

        //cercles rouges
        redOverTime();
    }

   public void growAndBack( GameObject subject )
    {
        if (subject.transform.localScale.x < 7.0f)
        {
            
            subject.transform.localScale += new Vector3(vitesseEvol* Time.deltaTime, vitesseEvol * Time.deltaTime, 0);

        }
        else
        {
            subject.transform.localScale = new Vector3(0.001f, 0.001f, 0);
            newest = subject;
            //refresh color
            newest.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f,1f);
            //reset activated
            if( newest == activated) 
            { 
                activated = null;
                fail = false;
            }
        }
    }

    void transparency(GameObject subject)
    {
        float trans = subject.transform.localScale.x/5f;
        Color tmpCol = subject.GetComponent<SpriteRenderer>().color;

        if( trans < 0.2 )
        {   tmpCol.a = 0f;    }
        else { tmpCol.a = trans; }

        
        subject.GetComponent<SpriteRenderer>().color = tmpCol;
    }

    public void activation( Color neon)
    {
        activated = newest;
        activated.GetComponent<SpriteRenderer>().color =  neon;
    }

    //run dans update pour activé les cerlces rouges
    public void redOverTime()
    {
        if ( listRed.Any() && oTimer.tps > /*1f*/ listRed[0] && activated == null)
        {
            activation(Color.green);
            listRed.RemoveAt(0);
        }
    }

    //dis si le cercle activé est suffisant grand pour pour etre au niveau de la particule
    public bool activatedOnEdge()
    {
        if(activated == null) { return false; }
        if(fail == true) { return false; }
        return (activated.transform.localScale.x / 7f > 0.9f);
    }
}
