using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    //public Dictionary<string, int> stats = new Dictionary<string, int>();

    protected Item(int id, string title, string description, string sprite)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>(sprite);
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
