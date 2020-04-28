using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class oParticule : MonoBehaviour
{
    public bool circularMvt = false;
    //coordonnées
    public float x = 0;
    public float y = 0;

    public float rayon = 3.0f;
    public float rayonMax = 4.0f;
    public float rayonMin = 1.0f;
    public float vitesseRayon = 0.1f;

    public float angle = 0.0f;
    public float vitesseAngle = 0.1f;

    public GameObject particule;
    public GameObject cercle;

    public float vitesse = 0.1f;

    

    // Use this for initialization
    public void Start()
    {

        particule = new GameObject();
        particule.AddComponent<SpriteRenderer>();
        particule.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("red-point") as Sprite;
        float ratio;
        ratio = 1 / particule.GetComponent<SpriteRenderer>().size.x;
        ratio *= 0.5f;
        particule.transform.localScale = new Vector3(ratio, ratio, ratio);
        
        cercle = new GameObject();
        cercle.AddComponent<SpriteRenderer>();
        cercle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("circleThin") as Sprite;

        cercle.transform.localScale = new Vector3(rayon*0.42f, rayon*0.42f, 0);

    }

    // Update is called once per frame
    public void Update()
    {        
            deplacementCirculaire();
            posCirc();


        cercle.transform.localScale = new Vector3(rayon * 0.42f, rayon * 0.42f, 0);
        //deplacement();



    }

    void deplacement()
    {
        float rayonCarre = x * x + y * y;

        if (rayonCarre > 16)
        {
            if (x > 0) { x -= vitesse; } else { x += vitesse; }
            if (y > 0) { y -= vitesse; } else { y += vitesse; }

        }
        //if ((x + vitesse) * (x + vitesse) + y * y < 16)
        //{
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x += vitesse;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x -= vitesse;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            y += vitesse;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y -= vitesse;
        }
        // }



        particule.transform.position = new Vector3(x, y, -1);
    }

    void deplacementCirculaire()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle -= vitesseAngle/rayon;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle += vitesseAngle/rayon;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if( rayon < rayonMax )
            rayon += vitesseRayon;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if( rayon > rayonMin )
            rayon -= vitesseRayon;
        }

        //ajustements
        if( rayon < rayonMin) { rayon = rayonMin; }
        if (rayon > rayonMax) { rayon = rayonMax; }
    }

    void posCirc()
    {
        x = rayon * Convert.ToSingle(Math.Cos(angle));
        y = rayon * Convert.ToSingle(Math.Sin(angle));
        particule.transform.position = new Vector3(x, y, -1);
    }
}

