using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        Vector3 result = new();
        result.x = x ?? vector.x;
        result.y = y ?? vector.y;
        result.z = z ?? vector.z;
        return result;
    }

    public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
    {
        Vector2 result = new();
        result.x = x ?? vector.x;
        result.y = y ?? vector.y;
        return result;
    }

    public static Vector2 ToXZ(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public static Vector3 FromXZ(this Vector2 vector, float y = 0)
    {
        return new Vector3(vector.x, y, vector.y);
    }
}