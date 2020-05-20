using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class ElmParticule : Item {

    public ElmParticule(int id, string title, string description, string sprite, string icon)
      : base(id, title, description, "Quarks/" + sprite, icon) {
      }

    public override void Use()  {
      Inventory.instance.AddCraft(this.id);
    }

    public override void UseCraft() {
        Inventory.instance.RemoveCraft(this.id);
    }
}
