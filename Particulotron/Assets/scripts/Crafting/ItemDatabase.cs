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

    public Baryon GetBaryon(int id) {
      return baryons.Find(item => item.id == id);
    }

    public Baryon GetBaryon(string title) {
      return baryons.Find(item => item.title == title);
    }

    public ElmParticule GetQuark(int id) {
      return quarks.Find(item => item.id == id);
    }

    public ElmParticule GetQuark(string title) {
      return quarks.Find(item => item.title == title);
    }

    void BuildBaryons() {
      baryons = new List<Baryon>() {
                new Baryon(8,"?","Does not exist",0,0,0,0),
                //Baryon J0.5
                new Baryon(211,"p","proton",1,0,0,0),
                new Baryon(221,"n","neutron",0,0,0,0),
                new Baryon(311,"Σ+","Sigma",1,-1,0,0),
                new Baryon(321,"Λ0","Lambda",0,-1,0,0),
                //new Baryon(321,"Σ0","Sigma",0,-1,0,0),
                new Baryon(322,"Σ-","Sigma",-1,-1,0,0),
                new Baryon(331,"Ξ0","Xi",0,-2,0,0),
                new Baryon(332,"Ξ-","Xi",-1,-2,0,0),
                new Baryon(411,"Σ++c","charmed Sigma",2,0,1,0),
                new Baryon(421,"Λ+c","charmed Lambda",1,0,1,0),
                //new Baryon(421,"Σ+c","charmed Sigma",1,0,1,0),
                new Baryon(422,"Σ0c","charmed Sigma",0,0,1,0),
                new Baryon(431,"Ξ+c","charmed Xi",1,-1,1,0),
                //new Baryon(431,"Ξ′+c","charmed Xi prime",1,-1,1,0),
                new Baryon(432,"Ξ0c","charmed Xi",0,-1,1,0),
                //new Baryon(432,"Ξ′0c","charmed Xi prime",0,-1,1,0),
                new Baryon(433,"Ω0c","charmed Omega",0,-2,1,0),
                new Baryon(441,"Ξ++cc","double charmed Xi",2,0,2,0),
                new Baryon(442,"Ξ+cc","double charmed Xi",1,0,2,0),
                new Baryon(443,"Ω+cc","double charmed Omega",1,-1,2,0),
                new Baryon(511,"Σ+b","bottom Sigma",1,0,0,-1),
                new Baryon(521,"Λ0b","bottom Lambda",0,0,0,-1),
                //new Baryon(521,"Σ0b","bottom Sigma",0,0,0,-1),
                new Baryon(522,"Σ-b","bottom Sigma",-1,0,0,-1),
                new Baryon(531,"Ξ0b","bottom Xi or Cascade B",0,-1,0,-1),
                //new Baryon(531,"Ξ′0b","bottom Xi prime",0,-1,0,-1),
                new Baryon(532,"Ξ-b","bottom Xi or Cascade B",-1,-1,0,-1),
                //new Baryon(532,"Ξ′-b","bottom Xi prime",-1,-1,0,-1),
                new Baryon(533,"Ω-b","bottom Omega",-1,-2,0,-1),
                new Baryon(541,"Ξ+cb","charmed bottom Xi",1,0,1,-1),
                //new Baryon(541,"Ξ′+cb","charmed bottom Xi prime",1,0,1,-1),
                new Baryon(542,"Ξ0cb","charmed bottom Xi",0,0,1,-1),
                //new Baryon(542,"Ξ′0cb","charmed bottom Xi prime",0,0,1,-1),
                new Baryon(543,"Ω0cb","charmed bottom Omega",0,-1,1,-1),
                //new Baryon(543,"Ω′0cb","charmed bottom Omega prime",0,-1,1,-1),
                new Baryon(544,"Ω+ccb","double charmed bottom Omega",1,0,2,-1),
                new Baryon(551,"Ξ0bb","double bottom Xi",0,0,0,-2),
                new Baryon(552,"Ξ-bb","double bottom Xi",-1,0,0,-2),
                new Baryon(553,"Ω-bb","double bottom Omega",-1,-1,0,-2),
                new Baryon(554,"Ω0cbb","charmed double bottom Omega",0,0,1,-2),
                //Baryon J1.5
                new Baryon(111,"Δ++\n(1232)","Delta",2,0,0,0),
                //new Baryon(211,"Δ+(1232)","Delta",1,0,0,0),
                //new Baryon(221,"Δ0(1232)","Delta",0,0,0,0),
                new Baryon(222,"Δ-\n(1232)","Delta",-1,0,0,0),
                //new Baryon(311,"Σ∗+(1385)","Sigma",1,-1,0,0),
                //new Baryon(321,"Σ∗0(1385)","Sigma",0,-1,0,0),
                //new Baryon(322,"Σ∗-(1385)","Sigma",-1,-1,0,0),
                //new Baryon(331,"Ξ∗0(1530)","Xi",0,-2,0,0),
                //new Baryon(332,"Ξ∗-(1530)","Xi",-1,-2,0,0),
                new Baryon(333,"Ω-","Omega",-1,-3,0,0),
                //new Baryon(411,"Σ∗++c(2520)","charmed Sigma",2,0,1,0),
                //new Baryon(421,"Σ∗+c(2520)","charmed Sigma",1,0,1,0),
                //new Baryon(421,"Ξ∗+c(2545)","charmed Xi",1,-1,1,0),
                //new Baryon(422,"Σ∗0c(2520)","charmed Sigma",0,0,1,0),
                //new Baryon(432,"Ξ∗0c(2545)","charmed Xi",0,-1,1,0),
                //new Baryon(433,"Ω∗0c(2770)","charmed Omega",0,-2,1,0),
                //new Baryon(441,"Ξ∗++cc","double charmed Xi",2,0,2,0),
                //new Baryon(442,"Ξ∗+cc","double charmed Xi",1,0,2,0),
                //new Baryon(443,"Ω∗+cc","double charmed Omega",1,-1,2,0),
                new Baryon(444,"Ω++ccc","triple charmed Omega",2,0,3,0),
                //new Baryon(511,"Σ∗+b","bottom Sigma",1,0,0,-1),
                //new Baryon(521,"Σ∗0b","bottom Sigma",0,0,0,-1),
                //new Baryon(522,"Σ∗-b","bottom Sigma",-1,0,0,-1),
                //new Baryon(531,"Ξ∗0b","bottom Xi",0,-1,0,-1),
                //new Baryon(532,"Ξ∗-b","bottom Xi",-1,-1,0,-1),
                //new Baryon(533,"Ω∗-b","bottom Omega",-1,-2,0,-1),
                //new Baryon(541,"Ξ∗+cb","charmed bottom Xi",1,0,1,-1),
                //new Baryon(542,"Ξ∗0cb","charmed bottom Xi",0,0,1,-1),
                //new Baryon(543,"Ω∗0cb","charmed bottom Omega",0,-1,1,-1),
                //new Baryon(544,"Ω∗+ccb","double charmed bottom Omega",1,0,2,-1),
                //new Baryon(551,"Ξ∗0bb","double bottom Xi",0,0,0,-2),
                //new Baryon(552,"Ξ∗-bb","double bottom Xi",-1,0,0,-2),
                //new Baryon(553,"Ω∗-bb","double bottom Omega",-1,-1,0,-2),
                //new Baryon(554,"Ω∗0cbb","charmed double bottom Omega",0,0,1,-2),
                new Baryon(555,"Ω-bbb","triple bottom Omega",-1,0,0,-3)
      };
    }

    void BuildQuarks() {
      quarks = new List<ElmParticule>() {
                new ElmParticule(1, "Quark Up", "u", "postit_UP", "icon_UP"),
                new ElmParticule(2, "Quark Down", "d", "postit_DOWN", "icon_DOWN"),
                new ElmParticule(3, "Quark Strange", "s", "postit_STRANGE", "icon_STRANGE"),
                new ElmParticule(4, "Quark Charm", "c", "postit_CHARM", "icon_CHARM"),
                new ElmParticule(5, "Quark Bottom", "b", "postit_BOTTOM", "icon_BOTTOM"),
                new ElmParticule(6, "Quark Top", "t", "postit_TOP", "icon_TOP")
      };
    }
}
