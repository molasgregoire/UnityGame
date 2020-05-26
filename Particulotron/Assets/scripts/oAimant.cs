using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class oAimant : MonoBehaviour
{
    public List<GameObject> listAimants = new List<GameObject>();
    public List<int> listCharge = new List<int>();
    public float chronoGreen = -10f;
    public float chronoPurple = -10f;
    public float cstAngle = 3.141f / 8f;
    public oParticule Part;
    public float currentAngle = 0f;

    float brillance = 0.2f;

    public GameObject speed;

    // doit contenir des liste de 9 float
    // 1 pour le temps, et les 8 autres -1 0 ou +1 pour les aimants
    public List<List<float>> magnetTab = new List<List<float>>();

    // Start is called before the first frame update
    void Start()
    {
        aimants();
        //listCharge = new List<int> { 1, 1, 0, 0, -1, -1, 0, 0 };
        listCharge = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
        //listCharge = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
        //listCharge = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
        //listCharge = new List<int> { -1, -1, 1, 1, -1, -1, 1, 1 };
        
    }

    // Update is called once per frame
    void Update()
    {
        manageMagnets();
        colorEachMagnet();
        colorCurrentMagnet();
        

        if (chronoPurple + 0.5f > oTimer.tps)
        {
            colorMagnets(new Color(1, 0, 1));
            
        }
        else if (chronoGreen + 0.5f > oTimer.tps)
        {
            colorMagnets(new Color(0, 1, 0));
            speed.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else { speed.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime*2f); }
        // else { tmpAltAimants(); }
        affectRayon();
    }

    public void aimants()
    {
        ///listAimants
        //nord
        GameObject nord = new GameObject();
        nord.AddComponent<SpriteRenderer>();
        nord.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        nord.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        nord.transform.position = new Vector3(0.02f, 2.473f+1f, 0.0f);
        nord.transform.rotation = Quaternion.Euler(0, 0, 22.5f);
        listAimants.Add(nord);
        //nord est
        GameObject nordEst = new GameObject();
        nordEst.AddComponent<SpriteRenderer>();
        nordEst.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        nordEst.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        nordEst.transform.position = new Vector3(1.768f + 0.71f, 1.768f + 0.71f, 0.0f);
        nordEst.transform.rotation = Quaternion.Euler(0, 0, 337.5f);
        listAimants.Add(nordEst);
        //est
        GameObject est = new GameObject();
        est.AddComponent<SpriteRenderer>();
        est.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        est.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        est.transform.position = new Vector3(2.47f+1f, 0.02f, 0.0f);
        est.transform.rotation = Quaternion.Euler(0, 0, 292.5f);
        listAimants.Add(est);
        //sud est
        GameObject sudEst = new GameObject();
        sudEst.AddComponent<SpriteRenderer>();
        sudEst.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        sudEst.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        sudEst.transform.position = new Vector3(1.71f + 0.71f, -1.739f - 0.71f, 0.0f);
        sudEst.transform.rotation = Quaternion.Euler(0, 0, 247.5f);
        listAimants.Add(sudEst);
        //sud
        GameObject sud = new GameObject();
        sud.AddComponent<SpriteRenderer>();
        sud.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        sud.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        sud.transform.position = new Vector3(-0.029f, -2.437f-1f, 0.0f);
        sud.transform.rotation = Quaternion.Euler(0, 0, 202.5f);
        listAimants.Add(sud);
        //sud ouest
        GameObject sudOuest = new GameObject();
        sudOuest.AddComponent<SpriteRenderer>();
        sudOuest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        sudOuest.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        sudOuest.transform.position = new Vector3(-1.769f - 0.71f, -1.7f - 0.71f, 0.0f);
        sudOuest.transform.rotation = Quaternion.Euler(0, 0, 157.5f);
        listAimants.Add(sudOuest);
        //ouest
        GameObject ouest = new GameObject();
        ouest.AddComponent<SpriteRenderer>();
        ouest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        ouest.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        ouest.transform.position = new Vector3(-2.467f-1f, 0.046f, 0.0f);
        ouest.transform.rotation = Quaternion.Euler(0, 0, 112.5f);
        listAimants.Add(ouest);
        //nord ouest
        GameObject nordouest = new GameObject();
        nordouest.AddComponent<SpriteRenderer>();
        nordouest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aimant") as Sprite;
        nordouest.transform.localScale = new Vector3(0.65f, 0.65f, 0);
        nordouest.transform.position = new Vector3(-1.735f - 0.71f, 1.762f + 0.71f, 0.0f);
        nordouest.transform.rotation = Quaternion.Euler(0, 0, 67.5f);
        listAimants.Add(nordouest);
    }

    /*public void tmpAltAimants()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((int)oTimer.tps % 2 == 0)
            {
                listAimants[2 * i].GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
                listAimants[2 * i + 1].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1f);
            }
            else
            {
                listAimants[2 * i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1f);
                listAimants[2 * i + 1].GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
            }
        }
    }*/

    public void colorMagnets(Color newcol)
    {
        foreach (GameObject magnet in listAimants)
        {
            magnet.GetComponent<SpriteRenderer>().color = newcol;
        }  
    }

    public int indexTab()
    {
        //float angle = currentAngle % (2f * 3.141f);
        float angle = Part.angle % (2f * 3.141f);
        angle *= 8/3.141f;

        if( angle < 0f) { angle += 16f; }
        

        if( angle > 1f && angle < 3f ){ return 1; }
        if( angle > 3f && angle < 5f ){ return 0; }
        if( angle > 5f && angle < 7f ){ return 7; }
        if( angle > 7f && angle < 9f ){ return 6; }
        if( angle > 9f && angle < 11f ){ return 5; }
        if( angle > 11f && angle < 13f ){ return 4; }
        if( angle > 13f && angle < 15f ){ return 3; }
        return 2;
    }

    public void colorCurrentMagnet()
    {
        //listAimants[indexTab()].GetComponent<SpriteRenderer>().color = newcol;
        listAimants[indexTab()].GetComponent<SpriteRenderer>().color /= brillance;
    }

    void colorEachMagnet()
    {
        
        for( int i = 0 ; i < 8 ; i++ ) 
        {
            Color newcol = new Color();

            if( listCharge[i] == 0) { newcol = new Color(brillance, brillance, brillance); }
            if( listCharge[i] == -1) { newcol = new Color(0f, brillance/2f, brillance); }
            if( listCharge[i] == 1) { newcol = new Color(brillance, 0f, 0f); }

            listAimants[i].GetComponent<SpriteRenderer>().color = newcol;
        }
    }

    float factor = 1.5f;
    void affectRayon()
    {
        Part.rayon -= factor*Time.deltaTime * Part.magnetisme * listCharge[indexTab()];
    }

    public void manageMagnets()
    {
        if (magnetTab.Count > 0)
        {
            if (oTimer.tps > magnetTab[0][0])
            {
                listCharge = new List<int>();
                for ( int i = 1; i < 9; i++ )
                {
                    listCharge.Add((int)magnetTab[0][i]);
                }

                magnetTab.RemoveAt(0);
            }

        }
    }
}
