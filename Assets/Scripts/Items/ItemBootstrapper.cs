
using UnityEngine;

public class ItemBootstrapper : MonoBehaviour
{
    ItemDB dataBase;
    ItemFactory itemFactory;
    public void Bootstrap(ItemDB database, ItemFactory factory) {

        this.dataBase = database;
        this.itemFactory = factory;

        
    }
}
