using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using UnityEngine;

public class InputShootSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Shoot
            Entities.ForEach((ref InputShootComponent inputShoot, ref Translation pos, ref Rotation rot, ref PhysicsVelocity vel) => {

                //Instantiate Bullet
                Entity spawnedEntity = EntityManager.Instantiate(inputShoot.BulletPrefab);

                //Set Bullet position and velocity
                EntityManager.SetComponentData(spawnedEntity, new Translation { Value = pos.Value });
                EntityManager.SetComponentData(spawnedEntity, new PhysicsVelocity { Linear = vel.Linear + inputShoot.InitialSpeed * math.mul(rot.Value, new float3(-1f, 0f, 0f)) });
                
            });
        }
    }
}
