using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {
    public static Inventario instance;
    public List<Item> items = new List<Item>();
    public int capacity = 20;

    void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public bool Add(Item item) {
        if (items.Count >= capacity) {
            Debug.Log("Inventario lleno");
            return false;
        }
        items.Add(item);
        Debug.Log("Item añadido: " + item.name);
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
    }
}
