using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class ElmParticule : Item {

    public ElmParticule(int id, string title, string description, string sprite)
      : base(id, title, description, "Quarks/" + sprite) {
      }

    public override void Use()  {
      Inventory.instance.AddCraft(this.id);
      Debug.Log("Used");
    }

    public override void UseCraft() {
        Inventory.instance.RemoveCraft(this.id);
        Debug.Log("You removed : " + this.title);
    }
}
