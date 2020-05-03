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

    public float vitesse = 0.5f;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update() { }

    void OnGUI()
    {
        /*
        if (started && obs.transform.localScale.x < 0.25f)
        {
            obs.transform.localScale += new Vector3(0.005f, 0.005f, 0);
        }*/
        
        

        if (started && obs.transform.localScale.x < finalRayon*ratio)
        {
            float tmp = linScale(oTimer.tps);
            /*float ratio;
            ratio = 1 / obs.GetComponent<SpriteRenderer>().size.x;
            ratio *= 0.5f;*/
            obs.transform.localScale = new Vector3(tmp*ratio, tmp*ratio, 0);
            linPos();
        }
        colorMeRed();
    }

    public float linScale( float time )
    {
        // div maxTime
        return (time - apparitionTime) * finalRayon*vitesse;
    }

    public void linPos()
    {
        float newX = (oTimer.tps - apparitionTime) * finalX * vitesse; // / 3f;
        float newY = (oTimer.tps - apparitionTime) * finalY * vitesse; // / 3f;
        obs.transform.position = new Vector3(newX, newY, 0);

    }

    public void alloc( float at, float fx , float fy , float fr , string im  )
    {
        apparitionTime = at;
        finalX = fx;
        finalY = fy;
        finalRayon = fr;
        image=im;
        
    }

    public void demarrage()
    {
        started = true;
        //print("DEMARRAGE");

        obs = new GameObject();
        obs.AddComponent<SpriteRenderer>();
        obs.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(image) as Sprite;
        obs.transform.localScale = new Vector3( 0.0f,  0.0f, 0);
        obs.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);

        obs.AddComponent<CircleCollider2D>();
        obs.GetComponent<CircleCollider2D>().radius=0.5f;

        ratio = 1 / obs.GetComponent<SpriteRenderer>().size.x;
        ratio *= 0.5f;
        
    }

    public float normalizeSized()
    {
        return obs.transform.localScale.x / (ratio * finalRayon);
    }

    public void colorMeRed()
    {
        if( started && normalizeSized() > 0.95f)
        {
            obs.GetComponent<SpriteRenderer>().color = new Color(200, 0, 0);
        }
    }
}
