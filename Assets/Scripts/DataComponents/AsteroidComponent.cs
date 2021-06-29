using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AsteroidComponent : IComponentData
{
    public Entity ParticleSystem;
}
