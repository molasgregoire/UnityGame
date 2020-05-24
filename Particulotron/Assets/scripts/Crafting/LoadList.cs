using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadList : MonoBehaviour
{
    [SerializeField]
    public GameObject buttonTemplate;


    public void GenButton(Baryon baryon,List<ElmParticule> quarks)
    {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            //button.AddComponent<ButtonListButton>();

            button.GetComponent<ButtonListButton>().SetBaryonQuarks(baryon, quarks);

            button.transform.SetParent(buttonTemplate.transform.parent, false);

            button.name = baryon.title;
    }
}
