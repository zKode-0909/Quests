using UnityEngine;

public class ItemFactory
{
    ItemDB database;

    public void InitializeFactory(ItemDB db)
    {
        database = db;


    }

    public bool TryCreateQuestFromID(string itemID, int receiverRuntimeID, out IRuntimeItem item)
    {
        if (database.TryGetItemDef(itemID, out var i))
        {
            var ItemSettings = i;
            item = null;
            //item = //new Quest(QuestSettings.QuestName, "blank", QuestSettings.ID, 3, QuestSettings.RequiredLevel, QuestSettings.objectives.BuildRuntimeQuestStages(), receiverRuntimeID);
            return true;
        }
        else
        {
            item = null;
            return false;
        }


    }
}
