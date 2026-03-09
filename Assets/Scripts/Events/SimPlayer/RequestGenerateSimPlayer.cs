using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public readonly struct RequestGenerateSimPlayerEvent : IEvent
{
    public readonly Level1CharacterTemplate templateToGenerate;

    public RequestGenerateSimPlayerEvent(Level1CharacterTemplate template)
    {
        this.templateToGenerate = template;
    }
}
