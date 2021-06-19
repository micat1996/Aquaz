using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainPanel : MonoBehaviour
{
	[SerializeField] private MenuButtons _MenuButtons;
	[SerializeField] private Button _Button_GameStart;

	private void Awake()
	{
		_Button_GameStart.onClick.AddListener(
			() => Micat1996UnityFramework.SceneManager.Instance.LoadScene("GameScene"));

		_MenuButtons.pauseButton.onClick.AddListener(
			() => (Micat1996UnityFramework.SceneManager.Instance.sceneInstance as DefaultSceneInstance).
			screenMessagePanel.FloatingScreenMessage("���� �������� �ƴմϴ�."));

		_MenuButtons.settingButton.onClick.AddListener(
			() => (Micat1996UnityFramework.SceneManager.Instance.sceneInstance as DefaultSceneInstance).
			screenMessagePanel.FloatingScreenMessage("�̱���", 1.0f, Color.red));
	}



}
