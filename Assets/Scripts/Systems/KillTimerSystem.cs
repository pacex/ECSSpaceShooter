using Unity.Entities;
using UnityEngine;

public class KillTimerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((Entity entity, ref KillTimerComponent timer) => {

            timer.Timer -= deltaTime;

            if (timer.Timer <= 0)
            {
                EntityManager.DestroyEntity(entity);
            }
        });
    }
}
