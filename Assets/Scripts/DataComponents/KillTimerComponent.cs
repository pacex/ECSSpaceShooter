using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct KillTimerComponent : IComponentData
{
    public float Timer;
}
