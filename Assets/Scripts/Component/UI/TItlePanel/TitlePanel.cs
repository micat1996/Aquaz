using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TitlePanel : MonoBehaviour
{

	[SerializeField] private List<RectTransform> _TitlePanelPrefabs;

	private RectTransform _VisiblePanel;

	private void Start()
	{
		BeginTouchPanel beginTouchPanel = 
			(_VisiblePanel = Instantiate(_TitlePanelPrefabs[0], transform)).
			GetComponent<BeginTouchPanel>();

		beginTouchPanel.titlePanel = this;
	}

	public void FloatingMainPanel()
	{
		Destroy(_VisiblePanel.gameObject);
		_VisiblePanel = Instantiate(_TitlePanelPrefabs[1], transform);
	}



}
