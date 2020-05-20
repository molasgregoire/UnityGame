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
    public float rayonMax = 3.0f;
    public float rayonMin = 0.5f;
    public float vitesseRayon = 3.5f;

    public float angle = 0.0f;
    public float vitesseAngle = 3.5f;

    public GameObject particule;
    public GameObject cercle;

    public float vitesse = 1.0f;

    //mettre ici les composant des particules
    public int magnetisme = 1;

    //test pour ajouter de l'inertie au bordel
    public float inertieAngle = 0f;
    public float inertieRayon = 0f;
    public float mass = 3.0f;

    // Use this for initialization
    public void Start()
    {

        particule = new GameObject();
        particule.AddComponent<SpriteRenderer>();
        particule.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Particule_blanche") as Sprite;
        float ratio;
        ratio = 1 / particule.GetComponent<SpriteRenderer>().size.x;
        //ratio *= 0.5f;
        particule.transform.localScale = new Vector3(ratio, ratio, ratio);

        //test collison
        particule.AddComponent<CircleCollider2D>();
        particule.GetComponent<CircleCollider2D>().radius = 0.43f;

        
        cercle = new GameObject();
        cercle.AddComponent<SpriteRenderer>();
        cercle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("whiteCircle") as Sprite;

        cercle.transform.localScale = new Vector3(rayon*0.42f, rayon*0.42f, 0);
        cercle.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0,0.5f);
    }

    // Update is called once per frame
    public void Update()// { }

    //void OnGUI()
    {        
        deplacementCirculaire();
        posCirc();
        inertie();

        cercle.transform.localScale = new Vector3(rayon * 0.42f, rayon * 0.42f, 0);
        //deplacement();



    }

    void deplacement()
    {
        float rayonCarre = x * x + y * y;

        if (rayonCarre > 36f)
        {
            if (x > 0) { x -= vitesse * Time.deltaTime; } else { x += vitesse * Time.deltaTime; }
            if (y > 0) { y -= vitesse * Time.deltaTime; } else { y += vitesse * Time.deltaTime; }

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
            angle += Time.deltaTime*vitesseAngle / rayon;
            if (inertieAngle < 1) { inertieAngle += Time.deltaTime * 3; }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle -= Time.deltaTime*vitesseAngle / rayon;
            if (inertieAngle > -1) { inertieAngle -= Time.deltaTime * 3; }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (rayon < rayonMax)
            { rayon += Time.deltaTime * vitesseRayon; }
            inertieRayon += Time.deltaTime*3;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (rayon > rayonMin)
            { rayon -= Time.deltaTime * vitesseRayon; }
            inertieRayon -= Time.deltaTime*3;
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

    void inertie()
    {
        angle += mass*Time.deltaTime * inertieAngle / rayon;

        rayon += 2*mass*Time.deltaTime * inertieRayon;

        //reduction de linertie avec le temps
        if( inertieAngle > 0) { inertieAngle -= Time.deltaTime; }
        if (inertieAngle < 0) { inertieAngle += Time.deltaTime; }
        if (inertieRayon > 0) { inertieRayon -= Time.deltaTime; }
        if (inertieRayon < 0) { inertieRayon += Time.deltaTime; }
    }

    public int howManyOverlap()
    {
        //tmpList 
        return particule.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D(), new List<Collider2D>());
    }
}

