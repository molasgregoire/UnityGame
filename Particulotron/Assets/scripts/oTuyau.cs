using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oTuyau : MonoBehaviour
{
    public GameObject backCircle;
    public GameObject frontCircle;
    public GameObject moveCircle;

    public List<GameObject> circles;
    public int nbCircles = 5;

    // Start is called before the first frame update
    void Start()
    {
        
        
        backCircle = new GameObject();
        backCircle.AddComponent<SpriteRenderer>();
        backCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
        float ratioC = 1 / backCircle.GetComponent<SpriteRenderer>().size.x;
        
        backCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0);

        frontCircle = new GameObject();
        frontCircle.AddComponent<SpriteRenderer>();
        frontCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
        frontCircle.transform.localScale = new Vector3(1.1f, 1.1f, 0);
        /*
        moveCircle = new GameObject();
        moveCircle.AddComponent<SpriteRenderer>();
        moveCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("circle") as Sprite;
        moveCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0);*/

        float dist = 1 / (nbCircles - 1.0f);
        for( int i = 0; i < nbCircles ; i++ )
        {
            GameObject tmp = new GameObject();
            
            tmp.AddComponent<SpriteRenderer>();
            tmp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("neonCircle") as Sprite;
            tmp.transform.localScale = new Vector3(i*dist,i*dist , 0);
            tmp.transform.position = new Vector3(0, 0, -1);
            circles.Add(tmp);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        // growAndBack(moveCircle);
        for (int i = 0; i < nbCircles; i++)
        {
            growAndBack(circles[i]);
        }
    }

    void growAndBack( GameObject subject )
    {
        if (subject.transform.localScale.x < 1.1f)
        {
            subject.transform.localScale += new Vector3(0.003f, 0.003f, 0);
        }
        else
        {
            subject.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        }
    }
}
