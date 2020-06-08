using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public Sprite icon2;
    //public Dictionary<string, int> stats = new Dictionary<string, int>();

    protected Item(int id, string title, string description, string sprite, string sprite2)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>(sprite);
        this.icon2 = Resources.Load<Sprite>(sprite2);
    }

    protected Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = item.icon;
    }

    public virtual void Use() {
    }

    public virtual void UseCraft() {
    }
}
