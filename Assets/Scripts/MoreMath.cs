using Unity.Mathematics;

/*
 * As Unity.Mathematics does not include a function to convert quaternions to Euler angles,
 * I use the following method from: https://forum.unity.com/threads/is-there-a-conversion-method-from-quaternion-to-euler.624007/
 */

public class MoreMath
{
    public static float3 GetQuaternionEulerAngles(quaternion rot)
    {
        float4 q1 = rot.value;
        float sqw = q1.w * q1.w;
        float sqx = q1.x * q1.x;
        float sqy = q1.y * q1.y;
        float sqz = q1.z * q1.z;
        float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
        float test = q1.x * q1.w - q1.y * q1.z;
        float3 v;

        if (test > 0.4995f * unit)
        { // singularity at north pole
            v.y = 2f * math.atan2(q1.y, q1.x);
            v.x = math.PI / 2f;
            v.z = 0;
            return NormalizeAngles(v);
        }
        if (test < -0.4995f * unit)
        { // singularity at south pole
            v.y = -2f * math.atan2(q1.y, q1.x);
            v.x = -math.PI / 2;
            v.z = 0;
            return NormalizeAngles(v);
        }

        rot = new quaternion(q1.w, q1.z, q1.x, q1.y);
        v.y = math.atan2(2f * rot.value.x * rot.value.w + 2f * rot.value.y * rot.value.z, 1 - 2f * (rot.value.z * rot.value.z + rot.value.w * rot.value.w));     // Yaw
        v.x = math.asin(2f * (rot.value.x * rot.value.z - rot.value.w * rot.value.y));                             // Pitch
        v.z = math.atan2(2f * rot.value.x * rot.value.y + 2f * rot.value.z * rot.value.w, 1 - 2f * (rot.value.y * rot.value.y + rot.value.z * rot.value.z));      // Roll
        return NormalizeAngles(v);
    }

    static float3 NormalizeAngles(float3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    static float NormalizeAngle(float angle)
    {
        while (angle > math.PI * 2f)
            angle -= math.PI * 2f;
        while (angle < 0)
            angle += math.PI * 2f;
        return angle;
    }
}
