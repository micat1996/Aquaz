using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BubbleSpawnerComponent;

public class JudgmentResultController : MonoBehaviour
{
    [System.Serializable]
    public struct JudgmentPrefabInfos
    {
        public JudgmentType judgment;
        public JudgmentResult judgmentPrefab;
    }

    private ObjectPool<JudgmentResult> _JudgmentResultPool = new ObjectPool<JudgmentResult>();
    private AquazGameSceneInstance _SceneInstance;

    [SerializeField] 
    private List<JudgmentPrefabInfos> _JudgmentPrefabInfos;
    private Dictionary<JudgmentType, JudgmentResult> _JudgmentPrefabs = new Dictionary<JudgmentType, JudgmentResult>();


    private void Awake()
    {
        foreach (JudgmentPrefabInfos judgmentSpriteInfo in _JudgmentPrefabInfos)
            _JudgmentPrefabs.Add(judgmentSpriteInfo.judgment, judgmentSpriteInfo.judgmentPrefab);
    }

    private void Start()
    {
        _SceneInstance = SceneManager.Instance.sceneInstance as AquazGameSceneInstance;
        _SceneInstance.judgmentResultController = this;
    }


    private JudgmentResult GetUsableJudgmentResult(JudgmentType judgmentType)
    {
        var newJudgment = _JudgmentResultPool.GetRecycleObject(
            (JudgmentResult judgmentResult) => judgmentResult.m_JudgmentType == judgmentType) ??
            _JudgmentResultPool.RegisterRecyclableObject(
                Instantiate(_JudgmentPrefabs[judgmentType], transform));

        newJudgment.InitializeJudgmentResult(judgmentType);
        return newJudgment;
    }

    public void ShowResult(JudgmentType judgmentType)
    {
        JudgmentResult judgmentResult = GetUsableJudgmentResult(judgmentType);



        const float randomRange = 20.0f;
        judgmentResult.rectTransform.anchoredPosition = 
            new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)); ;
    }

}
