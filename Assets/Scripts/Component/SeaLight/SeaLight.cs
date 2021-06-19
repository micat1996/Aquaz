using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SeaLight : MonoBehaviour
{
	[SerializeField] private float _MinLightIntensity = 1800.0f;
	[SerializeField] private float _MaxLightIntensity = 2100.0f;
	[SerializeField] private float _BrightSpeed = 1.0f;

	private Light _ControlledLight;

	private void Awake()
	{
		_ControlledLight = GetComponent<Light>();

		IEnumerator LightControl()
		{
			float runningTime = 0.0f;

			while (true)
			{
				float anchorIntensity = _MinLightIntensity + (_MinLightIntensity.Distance(_MaxLightIntensity) * 0.5f);
				float sinTerm = _MinLightIntensity.Distance(_MaxLightIntensity) * 0.5f;
				runningTime += Time.deltaTime * _BrightSpeed;

				_ControlledLight.intensity = anchorIntensity + Mathf.Sin(runningTime) * sinTerm;
				yield return null;
			}
		}

		StartCoroutine(LightControl());
	}

}
