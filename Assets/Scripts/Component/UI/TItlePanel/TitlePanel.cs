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

	Color showColor = Color.white;
	static bool mark = false;

    private void OnGUI()
    {
		if (mark) return;

		GUIStyle guiStyle = new GUIStyle();
		guiStyle.fontSize = 30;

		showColor.a -= Time.deltaTime * 0.6f;
		showColor.a = Mathf.Clamp(showColor.a, 0.0f, 1.0f);
		guiStyle.normal.textColor = showColor;

		Rect bounds = new Rect(
			new Vector2(0.0f, Screen.height - 40.0f),
			new Vector2(40.0f, Screen.height));

		//new Rect(Vector2.zero, Vector2.one * 500.0f)

		GUI.Label(bounds, "Dev By Micat", guiStyle);

		if (Mathf.Approximately(showColor.a, 0.0f)) mark = true;

	}


}
