using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Awake()
    {
        BuildItemDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string title)
    {
        return items.Find(item => item.title == title);
    }

    void BuildItemDatabase()
    {
        items = new List<Item>()
        {
            new ElmParticule(1, "Quark Up", "Quark...",
            new Dictionary<string, int> {
                { "charge", 42 },
                { "acharge qs", -1 }
            }),
            new ElmParticule(2, "Quark down", "Another quark",
            new Dictionary<string, int> {
              { "charge", 66 },
              { "acharge qs", 1 }
            }),
            new Baryon(3, "Proton", "That thing in the atom",
            new Dictionary<string, int> {
              { "charge", 42 },
              { "acharge qs", 0 }
            })
        };
    }
}
