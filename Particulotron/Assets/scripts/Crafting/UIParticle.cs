using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticle : MonoBehaviour
{
    public Item item;
    public CraftingRecipe craftingRecipe;
    //private Image spriteImage;

  private void Start() {
    UpdateParticle();
  }

  public void Craft() {
    Inventory.instance.ChangeParticle(craftingRecipe.Craft());
    UpdateParticle();
    Debug.Log("Craft showed");
  }

    public void UpdateParticle() {

      Image displayImage = transform.Find("Icon").GetComponent<Image>();

      if (item) {
        displayImage.sprite = item.icon;
        displayImage.color = Color.white;
      }
      else {
        displayImage.sprite = null;
        displayImage.color = Color.clear;
      }
    }
}
