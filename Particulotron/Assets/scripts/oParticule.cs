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

    public float rayon = 1.0f;
    public float rayonMax = 2.0f;
    public float rayonMin = 0.5f;
    public float vitesseRayon = 3.0f;

    public float angle = 0.0f;
    public float vitesseAngle = 3.0f;

    public GameObject particule;
    public GameObject cercle;

    public float vitesse = 1f;

    //mettre ici les composant des particules
    public int magnetisme = 1;

    // Use this for initialization
    public void Start()
    {

        particule = new GameObject();
        particule.AddComponent<SpriteRenderer>();
        particule.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rond") as Sprite;
        float ratio;
        ratio = 1 / particule.GetComponent<SpriteRenderer>().size.x;
        ratio *= 0.5f;
        particule.transform.localScale = new Vector3(ratio, ratio, ratio);

        //test collison
        particule.AddComponent<CircleCollider2D>();
        particule.GetComponent<CircleCollider2D>().radius = 0.43f;


        cercle = new GameObject();
        cercle.AddComponent<SpriteRenderer>();
        cercle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("whiteCircle") as Sprite;

        cercle.transform.localScale = new Vector3(rayon*0.42f, rayon*0.42f, 0);
        //cercle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
    }

    // Update is called once per frame
    public void Update()// { }

    //void OnGUI()
    {        
            deplacementCirculaire();
            posCirc();


        cercle.transform.localScale = new Vector3(rayon * 0.42f, rayon * 0.42f, 0);
        //deplacement();



    }

    void deplacement()
    {
        float rayonCarre = x * x + y * y;

        if (rayonCarre > 4)
        {
            if (x > 0) { x -= vitesse; } else { x += vitesse; }
            if (y > 0) { y -= vitesse; } else { y += vitesse; }

        }
        //if ((x + vitesse) * (x + vitesse) + y * y < 16)
        //{
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x += vitesse * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x -= vitesse * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            y += vitesse * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y -= vitesse * Time.deltaTime;
        }
        // }



        particule.transform.position = new Vector3(x, y, -1);
    }

    void deplacementCirculaire()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle -= Time.deltaTime*vitesseAngle / rayon;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle += Time.deltaTime*vitesseAngle / rayon;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if( rayon < rayonMax )
            rayon += Time.deltaTime*vitesseRayon;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if( rayon > rayonMin )
            rayon -= Time.deltaTime*vitesseRayon;
        }

        //ajustements
        if( rayon < rayonMin) { rayon = rayonMin; }
        if (rayon > rayonMax) { rayon = rayonMax; }
    }

    void posCirc()
    {
       /* x = rayon * Convert.ToSingle(Math.Cos(angle));
        y = rayon * Convert.ToSingle(Math.Sin(angle));*/
        x = rayon * (float)(Math.Cos(angle));
        y = rayon * (float)(Math.Sin(angle));
        particule.transform.position = new Vector3(x, y, -1);
    }

    public int howManyOverlap()
    {
        //tmpList 
        return particule.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D(), new List<Collider2D>());
    }
}

