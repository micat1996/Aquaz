using Micat1996UnityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BubbleSpawnerComponent;

public class JudgmentResult : MonoBehaviour,
    IObjectPoolable
{
    [HideInInspector]
    public JudgmentType m_JudgmentType;

    public bool canRecyclable { get; set; }
    public Action onRecycleStartEvent { get; set; }

    private Image _Image_JudgmentResult;

    private float _LastInitializeTime;

    public RectTransform rectTransform => transform as RectTransform;

    public void Awake()
    {
        _Image_JudgmentResult = GetComponent<Image>();
    }

    public JudgmentResult InitializeJudgmentResult(JudgmentType judgmentType)
    {
        m_JudgmentType = judgmentType;

        _Image_JudgmentResult.color = Color.white;
        _LastInitializeTime = Time.time;

        return this;
    }

    private void Update()
    {
        if (canRecyclable) return;

        if (Time.time < _LastInitializeTime + 1.0f) return;

        Color currentColor = _Image_JudgmentResult.color;
        currentColor.a = Mathf.MoveTowards(currentColor.a, 0.0f, 2.0f * Time.deltaTime);
        _Image_JudgmentResult.color = currentColor;

        if (Mathf.Approximately(currentColor.a, 0.0f))
        {
            canRecyclable = true;

        }
    }

}
