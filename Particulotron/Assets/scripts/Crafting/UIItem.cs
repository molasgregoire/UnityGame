using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item item;
    //private Image spriteImage;

  private void Start() {
    UpdateItem();
  }

    public void Use() {
      if(item) {
        item.Use();
        Debug.Log("You clicked on : " + item.title);
      }
    }

    public void UseCraft() {
      if(item) {
        item.UseCraft();
      }
    }

    public void UpdateItem() {
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
