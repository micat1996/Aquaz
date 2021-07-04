using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScorePanel : MonoBehaviour
{
    private AquazGameSceneInstance _SceneInstance;

    private int _PrevScore;


    [SerializeField]
    private List<Sprite> _NumberSprites;

    [SerializeField]
    private List<Image> _NumberImages;

    [SerializeField]
    private Image _Image_Combo;

    


    private void Start()
    {
        _SceneInstance = SceneManager.Instance.sceneInstance as AquazGameSceneInstance;

        IEnumerator AudoUpdateScoreImages()
        {
            WaitUntil whenScoreChange = new WaitUntil(
                () => _SceneInstance.score != _PrevScore);

            while (true)
            {
                yield return whenScoreChange;

                _PrevScore = _SceneInstance.score;

                UpdateScoreImages();
            }
        }

        StartCoroutine(AudoUpdateScoreImages());

        UpdateScoreImages();
    }

    private void Update()
    {
        _Image_Combo.enabled = _SceneInstance.comboActivated;
    }

    private void UpdateScoreImages()
    {
        string scoreToString = _PrevScore.ToString("0000");

        _NumberImages[0].sprite = _NumberSprites[int.Parse(scoreToString[0].ToString())];
        _NumberImages[1].sprite = _NumberSprites[int.Parse(scoreToString[1].ToString())];
        _NumberImages[2].sprite = _NumberSprites[int.Parse(scoreToString[2].ToString())];
        _NumberImages[3].sprite = _NumberSprites[int.Parse(scoreToString[3].ToString())];
    }


}
