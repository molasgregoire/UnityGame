using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class oJauge : MonoBehaviour
{
    public GameObject jauge;
    public GameObject mask;

    public float height = 1.5f;
    public float min = 0f;
    public float max = 1f;
    public float current = 0.5f;
    public float vitesse = 0.1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        jauge = new GameObject();
        jauge.AddComponent<SpriteRenderer>();
        jauge.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rainbow") as Sprite;
        jauge.transform.localScale = new Vector3(height, 1f, 0);
        jauge.transform.position = new Vector3(10, 3, 0);
        jauge.transform.rotation = Quaternion.Euler(0,0,90f);
        jauge.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        /* float ratio;
         ratio = 1 / jauge.GetComponent<SpriteRenderer>().size.x;
         ratio *= 0.5f;
         particule.transform.localScale = new Vector3(ratio, ratio, ratio);*/

        mask = new GameObject();
        mask.AddComponent<SpriteMask>();
        mask.GetComponent<SpriteMask>().sprite = Resources.Load<Sprite>("rainbow") as Sprite;
        mask.transform.localScale = new Vector3(height, 1f, 0);
        mask.transform.position = new Vector3(10, 3, 0);
        mask.transform.rotation = Quaternion.Euler(0, 0, 90f);

    }

    // Update is called once per frame
    void Update()
    {
        evolOverTime();
    }

    void evolOverTime()
    {
        float quotient = height * (1- current / (max - min));
        quotient -= mask.transform.localScale.x;
        mask.transform.localScale += new Vector3(quotient*vitesse, 0, 0);
        /*
        if( quotient > 0)
        {
            mask.transform.localScale += new Vector3(quotient, 0, 0);
        }
        else
        {
            mask.transform.localScale -= new Vector3( vi, 0, 0);
        }
        */

    }
}
