using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class HandleScreenEdgeSystem : ComponentSystem
{

    public enum Behaviour
    {
        Wrap = 0,
        Destroy = 1
    }

    protected override void OnUpdate()
    {
        float vertExtent = Camera.main.orthographicSize;
        float horExtent = vertExtent * Screen.width / Screen.height;

        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entities.ForEach((Entity entity, ref ScreenEdgeBehaviourComponent edgeBehaviour, ref Translation pos) =>
        {
            if (edgeBehaviour.wasOnScreen)
            {
                if (!IsPositionOnScreen(pos.Value))
                {
                    switch (edgeBehaviour.Behaviour)
                    {
                        case Behaviour.Wrap:
                            //Wrap position to other side of screen
                            pos.Value.x += math.abs(pos.Value.x) > horExtent ? -math.sign(pos.Value.x) * 2f * horExtent : 0f;
                            pos.Value.y += math.abs(pos.Value.y) > vertExtent ? -math.sign(pos.Value.y) * 2f * vertExtent : 0f;
                            break;
                        case Behaviour.Destroy:
                            entityManager.DestroyEntity(entity);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                edgeBehaviour.wasOnScreen = IsPositionOnScreen(pos.Value);
            }
        });
    }

    /*
     * Return true if pos is on screen
     */
    private bool IsPositionOnScreen(float3 pos)
    {
        float vertExtent = Camera.main.orthographicSize;
        float horExtent = vertExtent * Screen.width / Screen.height;

        return pos.x <= horExtent && pos.x >= -horExtent && pos.y <= vertExtent && pos.y >= -vertExtent;
    }
}
