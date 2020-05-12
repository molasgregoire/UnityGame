using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item item;
    //private Image spriteImage;

  private void Start() {
    UpdateItem(null);
  }

    public void Use() {
      if(this.item != null) {
        item.Use();
        Debug.Log("You clicked on : " + item.title);
      }
    }

    public void UseCraft() {
      if(this.item != null) {
        item.UseCraft();
      }
    }

    public void UpdateItem(Item quark) {
      this.item = quark;
      Image displayImage = transform.Find("Icon").GetComponent<Image>();

      if (this.item != null) {
        displayImage.sprite = item.icon;
        displayImage.color = Color.white;
      }
      else {
        displayImage.sprite = null;
        displayImage.color = Color.clear;
      }
    }
}
