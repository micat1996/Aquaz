using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BubbleType { Long, Octo, Star }

public sealed class BubblesPanel : MonoBehaviour
{
	[SerializeField] private BubbleButton[] _BubbleButtons;
	public Dictionary<BubbleType, BubbleButton> bubbleButtons { get; private set; }

	private void Awake()
	{
		bubbleButtons = new Dictionary<BubbleType, BubbleButton>();
		foreach (BubbleButton bubbleButton in _BubbleButtons)
			bubbleButtons.Add(bubbleButton.bubbleButtonType, bubbleButton);

	}


}
