using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class HpBar : MonoBehaviour
{
    [SerializeField] private Image _Image_HpForeground_Fill;

    private AquazGameSceneInstance _SceneInstance;
    private float _HpValue = 100.0f;

    private void Start()
    {
        _SceneInstance = SceneManager.Instance.sceneInstance as AquazGameSceneInstance;
        
        IEnumerator AutoUpdateHpBar()
        {
            WaitUntil whenHpValueChange = new WaitUntil(
                () => !Mathf.Approximately(_SceneInstance.hp, _HpValue));

            while (_SceneInstance.gameStatus != GameStatus.Finish ||
                _SceneInstance.gameStatus != GameStatus.GameOver)
            {
                yield return whenHpValueChange;

                _HpValue = _SceneInstance.hp;

                UpdateHpBar();
            }
        }

        StartCoroutine(AutoUpdateHpBar());
    }


    private void UpdateHpBar()
    {
        _Image_HpForeground_Fill.fillAmount = _SceneInstance.hp / AquazGameSceneInstance.maxHp;
    }


}
