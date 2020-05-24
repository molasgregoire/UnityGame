using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    Image firstSprite;

    [SerializeField]
    Image secondSprite;

    [SerializeField]
    Image thirdSprite;

    private Baryon baryon;
 
    
    public void OnClick()
    {
       
        Inventory.instance.CraftPreviouslyCrafted(baryon);
       /* for(int i =0; i < 3; i++)
        {
            Inventory.instance.RemoveCraft(Inventory.instance.itemCraft[i].id);
           
        }
        for(int j =0; j<3; j++)
        {
            Inventory.instance.AddCraft(Inventory.instance.previouslyCrafted[baryon][j].id);
        }
        Inventory.instance.Craft();*/
        
    }
    public void SetBaryonQuarks(Baryon baryon,List<ElmParticule> quarks )
    {

        this.baryon = baryon;

        SetText(baryon.Q, baryon.S, baryon.C, baryon.B);
        firstSprite.sprite = quarks[0].icon;
        secondSprite.sprite = quarks[1].icon;
        thirdSprite.sprite = quarks[2].icon;

    }
   public void SetText(int q,int s, int c, int b)
    {
        myText.text = "Q :" + q + " , S :" + s + " , C :" + c + " , B:" + b + "";
    }

   
}
