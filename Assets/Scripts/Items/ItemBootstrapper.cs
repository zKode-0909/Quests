using Codice.CM.Common.Tree;
using UnityEngine;

public class ItemBootstrapper : MonoBehaviour
{
    public void Bootstrap(ItemDB database, ItemFactory factory) {
        database.BuildItemDB();
        factory.InitializeFactory(database);
    }
}
