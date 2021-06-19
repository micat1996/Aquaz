using Micat1996UnityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class BubbleInstance : MonoBehaviour,
	IObjectPoolable
{
	[SerializeField] private Image _Image_Bubble;

	private BubbleSpawnerComponent _BubbleSpawner;
	private float _RunningTime;
	private float _FloatingSpeed;
	private Vector2 _InitialPosition;
	private BubbleType _BubbleType;
	

	public RectTransform rectTransform => transform as RectTransform;
	public bool canRecyclable { get; set; }
	public Action onRecycleStartEvent { get; set; }

	public void InitializeBubbleInstance(BubbleSpawnerComponent bubbleSpawner, BubbleType bubbleType, Sprite bubbleSprite, float floatingSpeed = 300.0f)
	{
		_BubbleSpawner = bubbleSpawner;
		_BubbleType = bubbleType;
		_Image_Bubble.sprite = bubbleSprite;
		_RunningTime = 0.0f;
		_FloatingSpeed = floatingSpeed;
		_InitialPosition = rectTransform.anchoredPosition;
	}

	private void Update()
	{
		if (canRecyclable) return;
		FloatingBubble();
	}

	private void FloatingBubble()
	{

		_RunningTime += Time.deltaTime * 10.0f;
		float centerX = _InitialPosition.x;
		float sinTerm = 50.0f;

		rectTransform.anchoredPosition = new Vector2(
			centerX + Mathf.Sin(_RunningTime) * sinTerm,
			rectTransform.anchoredPosition.y + (_FloatingSpeed * Time.deltaTime));

		if (rectTransform.anchoredPosition.y >= 1450.0f)
		{
			Debug.Log("Pop");
			_BubbleSpawner.DestroyBubble(_BubbleType, true);
		}
	}

}
