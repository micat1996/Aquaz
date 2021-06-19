using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class BeginTouchPanel : MonoBehaviour,
	IPointerClickHandler
{
	public TitlePanel titlePanel { get; set; }

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData) =>
		titlePanel.FloatingMainPanel();
}
