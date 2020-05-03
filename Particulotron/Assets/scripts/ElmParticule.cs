using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElmParticule : Item {

    public ElmParticule(int id, string title, string description, Dictionary<string, int> stats)
      : base(id, title, description, stats) {

      }

    public override void Use()  {
      Debug.Log("Used");
    }
}
