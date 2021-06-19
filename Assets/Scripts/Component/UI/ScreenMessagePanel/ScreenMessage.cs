using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScreenMessage : MonoBehaviour
{
	[SerializeField] private Text _Text_Message;
	[SerializeField] private Image _Image_Background;

	private float _CreatedTime;
	private float _Duration = -1.0f;

	public void SetText(string text, Color color)
	{
		_Text_Message.text = text;
		_Text_Message.color = color;
	}

	public void SetDuration(float duration)
	{
		_CreatedTime = Time.time;
		_Duration = duration;
	}

	private void Update()
	{
		if (_Duration < 0.0f) return;

		if (_CreatedTime + (_Duration * 0.5f) < Time.time)
		{
			float fadeDuration = (_Duration * 0.5f);
			float remainTime = _CreatedTime + _Duration - Time.time;
			Color textColor = _Text_Message.color;
			Color backgroundColor = _Image_Background.color;
			textColor.a = backgroundColor.a = remainTime / fadeDuration;
			_Text_Message.color = textColor;
			_Image_Background.color = backgroundColor;
		}

		if (_CreatedTime + _Duration < Time.time)
			Destroy(gameObject);
	}





}
