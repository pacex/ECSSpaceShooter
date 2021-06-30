using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerSystem : ComponentSystem
{
    private Unity.Mathematics.Random random;

    protected override void OnCreate()
    {
        base.OnCreate();
        random = new Unity.Mathematics.Random(42);
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        

        Entities.ForEach((ref SpawnerComponent spawner) =>
        {
            spawner.SpawnTimer -= deltaTime;
            if (spawner.SpawnTimer <= 0f)
            {
                spawner.SpawnTimer = spawner.TimerReset.x + random.NextFloat(0f, 1f) * (spawner.TimerReset.y - spawner.TimerReset.x);

                //Instantiate spawned entity
                Entity spawnedEntity = EntityManager.Instantiate(spawner.SpawnedPrefab);

                //Position spawned entity and set velocity towards point somewhere in the center of the screen
                float3 spawnPos = RandomScreenEdgePos();
                float3 targetPos = RandomScreenEdgePos() * 0.5f; //Requires screen center to be (0,0)

                float speed = spawner.SpeedMinMax.x + random.NextFloat(0f, 1f) * (spawner.SpeedMinMax.y - spawner.SpeedMinMax.x);

                EntityManager.SetComponentData(spawnedEntity, new Translation { Value = spawnPos }) ;
                EntityManager.SetComponentData(spawnedEntity, new PhysicsVelocity { Linear = math.normalize(targetPos - spawnPos) * speed });
            }
        });
    }

    /*
     * Returns random position just outside screen boundaries
     */
    private float3 RandomScreenEdgePos()
    {
        float vertExtent = Camera.main.orthographicSize;
        float horExtent = vertExtent * Screen.width / Screen.height;

        float3 pos = new float3(random.NextFloat2(-1f, 1f), 0f);
        pos += pos.x > pos.y ? new float3(math.sign(pos.x) * horExtent, pos.y * vertExtent, 0f) : new float3(pos.x * horExtent, math.sign(pos.y) * vertExtent, 0f);
        return pos;
    }
}
