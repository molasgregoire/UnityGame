using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inventory 
{
    private List<Particules> particulesList;
    public Inventory()
    {
        particulesList = new List<Particules>();
        particulesList.Add(new Particules { particuleType = Particules.ParticuleType.Proton });

       
    }
}
