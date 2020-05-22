using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
	public Item Item;
	[Range(1, 3)]
	public int Amount;
}

//[CreateAssetMenu]
public class Baryon : Item {

  public int Q;
  public int S;
  public int B;
  public int C;
  public List<ItemAmount> Materials;

    public Baryon(int id, string title, string description, int Q, int S, int C, int B)
      : base(id, title, description, "neonCircle", "Particule_blanche") {
        this.Q = Q;
        this.S = S;
        this.B = B;
        this.C = C;
      }

    public override void Use()  {
    }
    public override void UseCraft() {
    }

  	public bool CanCraft()
  	{
  		return HasMaterials();
  	}

  	private bool HasMaterials()
  	{
  		foreach (ItemAmount itemAmount in Materials)
  		{
  			if (Inventory.instance.ItemCount(itemAmount.Item.id) < itemAmount.Amount)
  			{
  				Debug.LogWarning("You don't have the required materials.");
  				return false;
  			}
  		}
  		return true;
  	}

  	public Item Craft()
  	{
  		if (CanCraft())
  		{
        Debug.Log("Can Craft");
        return this;
  		}
      else {
        return null;
      }
  	}
}
