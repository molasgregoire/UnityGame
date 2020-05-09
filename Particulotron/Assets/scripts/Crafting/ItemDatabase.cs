using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Baryon> baryons = new List<Baryon>();
    public List<ElmParticule> quarks = new List<ElmParticule>();

    private void Awake() {
      BuildBaryons();
      BuildQuarks();
    }

    public Item GetBaryon(int id) {
      return baryons.Find(item => item.id == id);
    }

    public Item GetBaryon(string title) {
      return baryons.Find(item => item.title == title);
    }

    public Item GetQuark(int id) {
      return quarks.Find(item => item.id == id);
    }

    public Item GetQuark(string title) {
      return quarks.Find(item => item.title == title);
    }

    void BuildBaryons() {
      baryons = new List<Baryon>() {
                new Baryon(211, "Proton", "lol",1,0,0,0),
                new Baryon(221, "Neutron", "lol",0,0,0,0),
      };
    }

    void BuildQuarks() {
      quarks = new List<ElmParticule>() {
                new ElmParticule(1, "Quark Up", "u"),
                new ElmParticule(2, "Quark Down", "d"),
      };
    }
}
