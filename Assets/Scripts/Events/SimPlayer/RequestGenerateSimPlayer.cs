using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public readonly struct RequestGenerateSimPlayerEvent : IEvent
{
    public readonly string templateToGenerate;

    public RequestGenerateSimPlayerEvent(string template)
    {
        this.templateToGenerate = template;
    }
}
