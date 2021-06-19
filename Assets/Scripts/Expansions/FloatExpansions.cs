using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExpansions
{
	public static float Distance(this float a, float b)
	{
		return Mathf.Sqrt(Mathf.Pow(a - b, 2.0f));
	}

}
