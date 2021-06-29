using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

public class CollisionSystem : JobComponentSystem
{

    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;
    private EndSimulationEntityCommandBufferSystem endSimEcbSystem;


    private struct CollisionJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<AsteroidComponent> asteroidGroup;
        public ComponentDataFromEntity<BulletComponent> bulletGroup;
        public ComponentDataFromEntity<Translation> translationGroup;
        public ComponentDataFromEntity<PlayerComponent> playerGroup;
        public EntityCommandBuffer ecb;


        public void Execute(TriggerEvent triggerEvent)
        {
            //Asteroid <=> Bullet
            if (asteroidGroup.HasComponent(triggerEvent.EntityA) && bulletGroup.HasComponent(triggerEvent.EntityB))
            {
                //Instantiate particle system
                Entity spawnedEntity = ecb.Instantiate(asteroidGroup[triggerEvent.EntityA].ParticleSystem);

                    //Position particle system
                ecb.SetComponent(spawnedEntity, translationGroup[triggerEvent.EntityA]);

                //Destroy asteroid and bullet
                ecb.DestroyEntity(triggerEvent.EntityA);
                ecb.DestroyEntity(triggerEvent.EntityB);
            }

            //Asteroid <=> Player
            else if (asteroidGroup.HasComponent(triggerEvent.EntityA) && playerGroup.HasComponent(triggerEvent.EntityB))
            {
                //Instantiate particle system
                Entity spawnedEntity = ecb.Instantiate(playerGroup[triggerEvent.EntityB].ParticleSystem);

                    //Position particle system
                ecb.SetComponent(spawnedEntity, translationGroup[triggerEvent.EntityB]);

                //Destroy player
                ecb.DestroyEntity(triggerEvent.EntityB);
            }

        }
    }

    protected override void OnCreate()
    {
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        endSimEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var collisionJob = new CollisionJob
        {
            //Pass data to collisionJob
            asteroidGroup = GetComponentDataFromEntity<AsteroidComponent>(),
            bulletGroup = GetComponentDataFromEntity<BulletComponent>(),
            translationGroup = GetComponentDataFromEntity<Translation>(),
            playerGroup = GetComponentDataFromEntity<PlayerComponent>(),
            ecb = endSimEcbSystem.CreateCommandBuffer()
        };

        JobHandle jobHandle = collisionJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);
        jobHandle.Complete();

        return jobHandle;
    }
}
