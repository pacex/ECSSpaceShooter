using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class AlienSystem : ComponentSystem
{
    private Unity.Mathematics.Random random;

    protected override void OnCreate()
    {
        random = new Unity.Mathematics.Random(42);
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        NativeArray<Entity> playerEntities = EntityManager.CreateEntityQuery(typeof(PlayerComponent)).ToEntityArray(Unity.Collections.Allocator.Temp);
        

        Entities.ForEach((ref AlienComponent alien, ref Translation pos) =>
        {
            if (alien.ShootTimer <= 0f && playerEntities.Length > 0)
            {
                //Shoot Player

                    //Instantiate bullet
                Entity spawnedEntity = EntityManager.Instantiate(alien.AlienBullet);

                    //Set bullet position and speed
                float3 playerPos = EntityManager.GetComponentData<Translation>(playerEntities[0]).Value;
                

                EntityManager.SetComponentData(spawnedEntity, new Translation { Value = pos.Value });
                EntityManager.SetComponentData(spawnedEntity, new PhysicsVelocity { Linear = math.normalize(playerPos - pos.Value) * alien.BulletVelocity });

                //Reset shoot timer
                alien.ShootTimer = alien.ShootTimerResetMinMax.x + random.NextFloat(0f, alien.ShootTimerResetMinMax.y - alien.ShootTimerResetMinMax.x);
                

            }

            alien.ShootTimer -= deltaTime;
        });
    }
}
