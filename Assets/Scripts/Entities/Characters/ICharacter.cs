using UnityEngine;

public interface ICharacter
{
    public string StableID { get; }

    public void Say(string words);
}
