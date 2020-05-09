using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticle : MonoBehaviour
{
    public Item item;

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

    public void UpdateParticle(Item particle) {

      this.item = particle;
      Image displayImage = transform.Find("Icon").GetComponent<Image>();
      Text displayText = transform.Find("Name").GetComponent<Text>();

      if (this.item != null) {
        displayText.text = item.title;
        displayImage.sprite = item.icon;
        displayImage.color = Color.white;
      }
      else {
        displayText.text = "";
        displayImage.sprite = null;
        displayImage.color = Color.clear;
      }
    }
}
