using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class BubbleButton : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private BubbleType _BubbleButtonType;

	public event System.Action<BubbleType> onBubbleButtonClicked;



	public BubbleType bubbleButtonType => _BubbleButtonType;
	public RectTransform rectTransform => transform as RectTransform;

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData) =>
		onBubbleButtonClicked?.Invoke(_BubbleButtonType);
}
