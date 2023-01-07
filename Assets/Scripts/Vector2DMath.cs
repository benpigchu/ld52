using UnityEngine;

public static class Vector2DMath
{
	public static Vector2 FromRotationAngle(float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}
	public static Vector2 Rotate(this Vector2 v, float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector2(
			v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian),
			v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian)
		);
	}
}