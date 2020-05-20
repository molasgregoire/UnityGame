using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oZone : oObstacle
{
    public float rotation = 0f;
    //nord
    //nord est
    //est
    //sud est
    //sud
    //sud ouest
    //ouest
    //nord ouest
    public override void alloc(float at, float fx, float fy, float fr, string im)
    {
        apparitionTime = at;
        finalX = fx;
        finalY = fy;
        finalRayon = fr;
        image = im;
        dist = (new Vector2(fx, fy)).magnitude;
    }

    public override void demarrage()
    {
        started = true;
        //print("DEMARRAGE");

        obs = new GameObject();
        obs.AddComponent<SpriteRenderer>();
        obs.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(image) as Sprite;
        obs.transform.localScale = new Vector3(0.0f, 0.0f, 0);
        obs.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);

        //obs.AddComponent<CircleCollider2D>();
        //obs.GetComponent<CircleCollider2D>().radius = 0.43f;
        obs.AddComponent<PolygonCollider2D>();

        ratio = 1 / obs.GetComponent<SpriteRenderer>().size.x;
        ratio *= 0.5f;

        obs.transform.rotation = Quaternion.Euler(0, 0, rotation);

    }

    public void rotate( float angle )
    {
        rotation = angle;
    }
}
