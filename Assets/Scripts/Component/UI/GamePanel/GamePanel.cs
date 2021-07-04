using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GamePanel : MonoBehaviour
{
    [SerializeField]
    private MenuButtons _MenuButtons;

    private AquazGameSceneInstance _SceneInstance;

    private void Start()
    {

        _SceneInstance = SceneManager.Instance.sceneInstance as AquazGameSceneInstance;

        _MenuButtons.pauseButton.onClick.AddListener(() =>
        {
            if (_SceneInstance.gameStatus == GameStatus.Pause)
                _SceneInstance.SetGameStatus(GameStatus.Play);
            else if (_SceneInstance.gameStatus == GameStatus.Play)
                _SceneInstance.SetGameStatus(GameStatus.Pause);
        });

    }


}
