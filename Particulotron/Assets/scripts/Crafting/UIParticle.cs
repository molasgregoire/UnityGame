using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticle : MonoBehaviour
{
    public Baryon item;

  private void Start() {
    UpdateParticle(null);
  }

  void Update() {
    Inventory.instance.Craft();
    //UpdateParticle();
  }

  public void Craft() {
    //Inventory.instance.Craft();
  }

    public void UpdateParticle(Baryon particle) {

      this.item = particle;
      Image displayImage = transform.Find("Icon").GetComponent<Image>();
      Text displayText = transform.Find("Name").GetComponent<Text>();
      Text displayS = transform.Find("S").GetComponent<Text>();
      Text displayB = transform.Find("B").GetComponent<Text>();
      Text displayC = transform.Find("C").GetComponent<Text>();
      Text displayQ = transform.Find("Q").GetComponent<Text>();

      if (this.item != null) {
        displayText.text = item.title;
        displayImage.sprite = item.icon;
        displayImage.color = Color.white;

        displayQ.text = displayplus(item.Q);
        displayS.text = displayplus(item.S);
        displayC.text = displayplus(item.C);
        displayB.text = displayplus(item.B);
      }
      else {
        displayText.text = "";
        displayImage.sprite = null;
        displayImage.color = Color.clear;

        displayQ.text = "0";
        displayS.text = "0";
        displayC.text = "0";
        displayB.text = "0";
      }
    }

    string displayplus(int X) {
      if (X>0) {
        string res = "+"+X.ToString();
        return res;
      }
      else {
        string res = X.ToString();
        return res;
      }
    }
}
