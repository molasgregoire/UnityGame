using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : MonoBehaviour
{
    public float x0 = 3.59f;
    public float y0 = 1.18f;
    public float xF = 0f;
    public float yF = -1f;

    public float scale0 = 0.4f;
    public float scaleF = 0.75f;

    public float cumulTime = 0.0f;
    public float transitTime = 1f;
    public float vitesse = 0f;
    public Vector2 vector = new Vector2();

    public GameObject ecranTransition ;
    public GameObject font;

    //levelCreator lancement = new levelCreator();

    // Start is called before the first frame update
    void Start()
    {
        ecranTransition = new GameObject();
        ecranTransition.AddComponent<SpriteRenderer>();
        ecranTransition.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("écran_clean") as Sprite;
        ecranTransition.transform.localScale = new Vector3(0.405f, 0.39f, 1f);
        ecranTransition.transform.position = new Vector3(3.59f, 1.18f, 3f);

        font = new GameObject();
        font.AddComponent<SpriteRenderer>();
        font.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("layout_crafting_EXEMPLE") as Sprite;
        font.transform.localScale = new Vector3(0.93f, 0.93f, 0);
        font.transform.position = new Vector3(0.0f, 0.0f, 4.0f);

        vector = new Vector2(xF - x0, yF - y0);
        float dist = vector.magnitude;
        vector.Normalize();
        vitesse = dist / transitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (cumulTime < transitTime)
        {
            cumulTime += Time.deltaTime;

            //scale

            float newScale = Time.deltaTime * (scaleF - scale0) /transitTime;
            ecranTransition.transform.localScale += new Vector3(newScale, newScale, 0);

            //x y
            float newX = vector.x * Time.deltaTime * vitesse;
            float newY = vector.y * Time.deltaTime * vitesse;
            ecranTransition.transform.position += new Vector3(newX, newY, 0.0f);
        }
        else {
            
            /*GameObject tmp = new GameObject();
            tmp.AddComponent<oEnv>();*/

            //passer par levelCreator ici
            levelCreator.level = 0;
            GameObject tmp = new GameObject();
            levelCreator lancement = tmp.AddComponent<levelCreator>();
            //lancement.Go();*/



            Destroy(this);
        }
    }
}
