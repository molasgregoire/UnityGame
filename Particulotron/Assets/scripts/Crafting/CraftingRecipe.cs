using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
	public Item Item;
	[Range(1, 3)]
	public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
	public List<ItemAmount> Materials;
	public Item Result;

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
      return Result;
		}
    else {
      return null;
    }
	}

}
