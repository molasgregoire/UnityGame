using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElmParticule : Item {

    public ElmParticule(int id, string title, string description, Dictionary<string, int> stats)
      : base(id, title, description, stats) {

      }

    public override void Use()  {
      Inventory.instance.AddCraft(this);
      Debug.Log("Used");
    }

    public override void UseCraft() {
        Inventory.instance.RemoveCraft(this);
        Debug.Log("You removed : " + this.title);
    }
}
