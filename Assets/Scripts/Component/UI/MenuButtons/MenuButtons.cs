using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class MenuButtons : MonoBehaviour
{
	[SerializeField] private Button _Button_Pause;
	public bool m_PauseButtonVisiblity = true;
	[SerializeField] private Button _Button_Setting;
	public bool m_SettingButtonVisiblity = true;

	public Button pauseButton => _Button_Pause;
	public Button settingButton => _Button_Setting;

	private void Awake()
	{
		_Button_Pause.gameObject.SetActive(m_PauseButtonVisiblity);
		_Button_Setting.gameObject.SetActive(m_SettingButtonVisiblity);
	}
}
