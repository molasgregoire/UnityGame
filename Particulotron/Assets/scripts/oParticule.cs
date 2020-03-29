/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oParticule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oParticule : MonoBehaviour
{

    //coordonnées
    public float x = 0;
    public float y = 0;

    public GameObject particule;
    public GameObject cercle;

    public float vitesse = 0.1f;


    // Use this for initialization
    void Start()
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
        cercle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("circle") as Sprite;

        cercle.transform.localScale = new Vector3(2, 2, 0);

    }

    // Update is called once per frame
    void Update()
    {

        deplacement();


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



        particule.transform.position = new Vector3(x, y, 0);
    }
}

