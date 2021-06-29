using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics.Extensions;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class MovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        float2 input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float2 mousePos = new float2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        Entities.ForEach((ref PhysicsVelocity vel, in Translation pos, in Rotation rot, in InputAccelerationComponent inputAcc) => {

            //Movement
            vel.Linear.xy += input * inputAcc.Linear * deltaTime;

            //Orientation

                //Angle between mousePosRel and (1,0) in radians [0,2PI]
            float2 mousePosRel = mousePos - pos.Value.xy;
            float mouseDirection = math.atan2(mousePosRel.y, mousePosRel.x) + math.PI;

                //Current rotation around z-Axis in radians [0,2PI]
            float currentDirection = MoreMath.GetQuaternionEulerAngles(rot.Value).z;

                //Adjust mouseDirection so that sign(mouseDirection - currentDirection) reflects the correct direction of rotation
            if (mouseDirection > currentDirection + math.PI)
            {
                mouseDirection -= 2f * math.PI;
            }
            if (mouseDirection <= currentDirection - math.PI)
            {
                mouseDirection += 2f * math.PI;
            }


            vel.Angular.z += (mouseDirection - currentDirection) * inputAcc.Angular * deltaTime;

            

        }).Run();

        return default;
    }
}
