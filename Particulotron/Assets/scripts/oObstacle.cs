using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oObstacle : MonoBehaviour
{
    public float apparitionTime;

    public float finalX;
    public float finalY;

    public float finalRayon;

    public string image;
    public GameObject obs;

    public bool started = false;
    public bool hit = false;
    //public float* tps; 

    public float ratio=0f;

    public static float vitesse = 0.5f;
    public float dist = 1f;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update() //{ }

    //void OnGUI()
    {
        /*
        if (started && obs.transform.localScale.x < 0.25f)
        {
            obs.transform.localScale += new Vector3(0.005f, 0.005f, 0);
        }*/
        
        

        if (started && obs.transform.localScale.x < finalRayon)//*ratio)
        {
            float tmp = linScale(oTimer.tps);
            /*float ratio;
            ratio = 1 / obs.GetComponent<SpriteRenderer>().size.x;
            ratio *= 0.5f;*/
            //obs.transform.localScale += new Vector3(tmp*ratio, tmp*ratio, 0);
            obs.transform.localScale += new Vector3(tmp, tmp, 0);
            linPos();

            //color transparency
            transparency();
            
        }
        colorMeRed();
        
    }

    public void transparency()
    {
        Color tmpColor = obs.GetComponent<SpriteRenderer>().color;
        float calcul = obs.transform.position.x * obs.transform.position.x + obs.transform.position.y * obs.transform.position.y;
        /*if (calcul < 0.125f) { tmpColor.a = 0f; }
        else {*/
        tmpColor.a = normalizeSized();// * normalizeSized(); 
        //}
        obs.GetComponent<SpriteRenderer>().color = tmpColor;
    }

    public float linScale( float time )
    {
        // div maxTime
        float maxTime = dist / vitesse;
        return Time.deltaTime * finalRayon * vitesse;//  maxTime ;
    }

    public void linPos()
    {
        /*float newX = (oTimer.tps - apparitionTime) * finalX * vitesse; // / 3f;
        float newY = (oTimer.tps - apparitionTime) * finalY * vitesse; // / 3f;*/
        float maxTime = dist / vitesse;
        float newX = Time.deltaTime * finalX * vitesse; // maxTime; // / 3f;
        float newY = Time.deltaTime * finalY * vitesse; // maxTime; // / 3f;
        obs.transform.position += new Vector3(newX, newY, 0);

    }

    public virtual void alloc( float at, float fx , float fy , float fr , string im  )
    {
        apparitionTime = at;
        finalX = fx;
        finalY = fy;
        finalRayon = fr;
        image=im;
        dist = ( new Vector2(fx,fy) ).magnitude ;
    }

    public virtual void demarrage()
    {
        started = true;
        //print("DEMARRAGE");

        obs = new GameObject();
        obs.AddComponent<SpriteRenderer>();
        obs.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(image) as Sprite;
        obs.transform.localScale = new Vector3( 0.0f,  0.0f, 0);
        obs.GetComponent<SpriteRenderer>().color = new Color(1f,0f,0.73f,0f);

        obs.AddComponent<CircleCollider2D>();
        obs.GetComponent<CircleCollider2D>().radius=0.43f;

        ratio = 1 / obs.GetComponent<SpriteRenderer>().size.x;
        ratio *= 0.5f;
        
    }

    public float normalizeSized()
    {
        return obs.transform.localScale.x / finalRayon;//(ratio * finalRayon);
    }

    public void colorMeRed()
    {
        if( started && normalizeSized() > 0.95f)
        {
            obs.GetComponent<SpriteRenderer>().color = new Color(200, 0, 0);
        }
    }
}
