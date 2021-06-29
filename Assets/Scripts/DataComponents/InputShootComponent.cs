using Unity.Entities;

[GenerateAuthoringComponent]
public struct InputShootComponent : IComponentData
{
    //Prefab to instantiate
    public Entity BulletPrefab;

    //Bullet Speed
    public float InitialSpeed;
}
