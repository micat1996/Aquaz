using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessagePanel : MonoBehaviour
{
	[SerializeField] private ScreenMessage _ScreenMessagePrefab;

	private void Start()
	{
		(SceneManager.Instance.sceneInstance as DefaultSceneInstance).screenMessagePanel = this;
	}

	public void FloatingScreenMessage(string screenMessage, float duration = 1.0f) =>
		FloatingScreenMessage(screenMessage, duration, Color.white);

	public void FloatingScreenMessage(string message, float duration, Color messageColor)
	{
		ScreenMessage newScreenMessage = Instantiate(_ScreenMessagePrefab, transform);
		newScreenMessage.SetDuration(duration);
		newScreenMessage.SetText(message, messageColor);
	}
}
