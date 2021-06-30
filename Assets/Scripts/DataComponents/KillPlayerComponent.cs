using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct KillPlayerComponent : IComponentData
{
    //Use this component to tag entities that kill player upon collision
}
