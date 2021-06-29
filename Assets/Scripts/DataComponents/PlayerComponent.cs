using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PlayerComponent : IComponentData
{
    //Player destruction particle system prefab
    public Entity ParticleSystem;
}
