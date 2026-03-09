
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "SimPlayerTemplateDB", menuName = "Databases/SimPlayerTemplateDB")]
public class SimPlayerTemplateDB : ScriptableObject
{
    [SerializeField] List<Level1CharacterTemplate> Templates = new();

    Dictionary<string, Level1CharacterTemplate> TemplatesByID;

    public void BuildTemplateDB()
    {

        TemplatesByID = new Dictionary<string, Level1CharacterTemplate>(Templates.Count);

        foreach (var Template in Templates)
        {
            if (Template == null || string.IsNullOrWhiteSpace(Template.StableID)) continue;

            if (TemplatesByID.ContainsKey(Template.StableID))
                Debug.LogError($"Duplicate ItemId: {Template.StableID}");
            else
                TemplatesByID.Add(Template.StableID, Template);
        }
    }

    public bool TryGetTemplateDef(string templateID, out Level1CharacterTemplate template)
    {
        if (TemplatesByID == null)
        {
            BuildTemplateDB();
        }
        if (TemplatesByID.TryGetValue(templateID, out var t))
        {
            template = t;
            return true;
        }
        else
        {
            template = null;
            return false;
        }
    }

    public IReadOnlyDictionary<string, Level1CharacterTemplate> GetCharacterTemplateDB() {
        if (TemplatesByID != null)
        {
            return TemplatesByID;
        }
        else {
            return null;
        }
    }
}
