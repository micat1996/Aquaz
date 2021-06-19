using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class BubbleSpawnerComponent : MonoBehaviour
{
	public enum JudgmentType
	{ 
		None,
		Miss,
		Bad,
		Good,
		Perfect
	}

	[System.Serializable]
	public struct RhythmJudgmentRange
	{
		public JudgmentType judgmentType;
		public Color gizmoColor;
		public float rangeTop, rangeBottom;
	}

	[System.Serializable]
	public struct BubbleSpriteElem
	{
		public BubbleType bubbleType;
		public Sprite bubbleSprite;
	}


	[SerializeField] private BubblesPanel _Panel_Bubbles;
	[SerializeField] private Canvas _Canvas;
	[SerializeField] private BubbleInstance _BubbleInstancePrefab;
	[SerializeField] private bool _DrawRhythmJudgmentRanges = true;

	[Header("Judgment Range")]
	[Tooltip("판정 범위들을 나타냅니다. 낮은 랭크부터 추가되어야 합니다. ([0]Miss, [1]Bad, [2]Good, [3]Perfect)")]
	[SerializeField] private List<RhythmJudgmentRange> _RhythmJudgmentRanges;
	[SerializeField] private List<BubbleSpriteElem> _BubbleSprites;

	private Dictionary<BubbleType, Vector2> _SpawnPositions;

	private ObjectPool<BubbleInstance> _BubblesPool = new ObjectPool<BubbleInstance>();

	private Dictionary<BubbleType, Queue<BubbleInstance>> _SpawnedBubble = new Dictionary<BubbleType, Queue<BubbleInstance>>();


	private void Awake()
	{
		_SpawnPositions = new Dictionary<BubbleType, Vector2>();
		_SpawnPositions.Add(BubbleType.Long, new Vector2(150.0f, 137.1841f));
		_SpawnPositions.Add(BubbleType.Octo, new Vector2(450.0f, 137.1841f));
		_SpawnPositions.Add(BubbleType.Star, new Vector2(750.0f, 137.1841f));

		_SpawnedBubble = new Dictionary<BubbleType, Queue<BubbleInstance>>();
		_SpawnedBubble.Add(BubbleType.Long, new Queue<BubbleInstance>());
		_SpawnedBubble.Add(BubbleType.Octo, new Queue<BubbleInstance>());
		_SpawnedBubble.Add(BubbleType.Star, new Queue<BubbleInstance>());
	}

	private void Start()
	{
		_Panel_Bubbles.bubbleButtons[BubbleType.Long].onBubbleButtonClicked += OnBubbleButtonClicked;
		_Panel_Bubbles.bubbleButtons[BubbleType.Octo].onBubbleButtonClicked += OnBubbleButtonClicked;
		_Panel_Bubbles.bubbleButtons[BubbleType.Star].onBubbleButtonClicked += OnBubbleButtonClicked;


		StartRandomSpawn();
	}

	// Test
	private void StartRandomSpawn()
	{
		IEnumerator RandomSpawn()
		{

			WaitForSeconds wait1Sec = new WaitForSeconds(1.0f);
			while (true)
			{
				//Debug.Log("Random Spawn");

				SpawnBubbleInstance((BubbleType)Random.Range(0, 3));
				yield return wait1Sec;
			}
		}
		StartCoroutine(RandomSpawn());

	}

	private BubbleInstance SpawnBubbleInstance(BubbleType bubbleType)
	{
		BubbleInstance newBubbleInstance = _BubblesPool.GetRecycleObject() ?? _BubblesPool.RegisterRecyclableObject(
			Instantiate(_BubbleInstancePrefab, transform));

		Sprite bubbleSprite = _BubbleSprites.Find((BubbleSpriteElem elem) => elem.bubbleType == bubbleType).bubbleSprite;

		newBubbleInstance.rectTransform.anchoredPosition = _SpawnPositions[bubbleType];
		newBubbleInstance.InitializeBubbleInstance(this, bubbleType, bubbleSprite);
		_SpawnedBubble[bubbleType].Enqueue(newBubbleInstance);


		return newBubbleInstance;
	}

	private void OnBubbleButtonClicked(BubbleType bubbleButtonType)
	{
		if (_SpawnedBubble[bubbleButtonType].Count == 0) return;


		DestroyBubble(bubbleButtonType);
	}

	public void DestroyBubble(BubbleType bubbleType, bool forceDestroy = false)
	{
		BubbleInstance nearestBubble = _SpawnedBubble[bubbleType].Peek();

		JudgmentType judgmentType = GetJudgmentType(nearestBubble);

		Debug.Log($"judgmentType = [{judgmentType}]");
		if (judgmentType == JudgmentType.None && !forceDestroy) return;

		_SpawnedBubble[bubbleType].Dequeue();
		nearestBubble.canRecyclable = true;
		nearestBubble.rectTransform.anchoredPosition = Vector2.down * 500.0f;

		Debug.Log(judgmentType.ToString());


	}

	public JudgmentType GetJudgmentType(BubbleInstance bubbleInstance)
	{
		Vector2 bubblePosition = bubbleInstance.rectTransform.anchoredPosition;

		for (int i = _RhythmJudgmentRanges.Count - 1; i > -1; --i)
		{
			if (bubblePosition.y > _RhythmJudgmentRanges[i].rangeBottom &&
				bubblePosition.y < _RhythmJudgmentRanges[i].rangeTop)
			{
				return _RhythmJudgmentRanges[i].judgmentType;
			}
		}

		return JudgmentType.None;
	}



#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Matrix4x4 GetCanvasMatrix(Canvas canvas)
		{
			RectTransform rectTr = canvas.transform as RectTransform;
			Matrix4x4 canvasMatrix = rectTr.localToWorldMatrix;
			canvasMatrix *= Matrix4x4.Translate(-rectTr.sizeDelta / 2);
			return canvasMatrix;
		}

		if (_Canvas == null) return;
		Gizmos.matrix = Handles.matrix = GetCanvasMatrix(_Canvas);

		Handles.Label(new Vector2(0.0f, 1650.0f),
			$"판정 표시 / 해제 : BubbleSpawnerComponent._DrawRhythmJudgmentRanges({(_DrawRhythmJudgmentRanges ? "true" : "false")})");

		if (!_DrawRhythmJudgmentRanges) return;




		if (_RhythmJudgmentRanges != null)
		{
			for (int i = 0; i < _RhythmJudgmentRanges.Count; ++i)
			{
				RhythmJudgmentRange judgmentRange = _RhythmJudgmentRanges[i];

				Vector2 min = new Vector2(0.0f - (i * 30.0f), judgmentRange.rangeBottom);
				Vector2 max = new Vector2(900.0f + (i * 30.0f), judgmentRange.rangeTop);
				Gizmos.color = judgmentRange.gizmoColor;

				Gizmos.DrawCube(((max - min) * 0.5f) + min, min - max);

				Gizmos.color = Color.white;
				Gizmos.DrawLine(new Vector2(0.0f, min.y), new Vector2(1000.0f, min.y));
				Gizmos.DrawLine(new Vector2(0.0f, max.y), new Vector2(1000.0f, max.y));

				Handles.Label(new Vector2(1000.0f, min.y + 20.0f), judgmentRange.judgmentType.ToString());
				Handles.Label(new Vector2(1000.0f, max.y + 20.0f), judgmentRange.judgmentType.ToString());
				
			}
		}
	}
#endif


}
;