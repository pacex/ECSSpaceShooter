using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AlienComponent : IComponentData
{
    public Entity ParticleSystem;
    public Entity AlienBullet;

    public float BulletVelocity;
    public float ShootTimer;
    public float2 ShootTimerResetMinMax;

}
