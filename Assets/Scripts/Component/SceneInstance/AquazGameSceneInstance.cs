using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BubbleSpawnerComponent;

public enum GameStatus
{
    Idle,
    Play,
    Pause,
    GameOver,
    Finish
}

public sealed class AquazGameSceneInstance : AquazSceneInstance
{
    [SerializeField] private AudioClip _Bgm;
    [SerializeField] private AudioClip _GameOver;
    [SerializeField] private AudioClip _Victory;
    public SoundInstance bgmSoundInstance { get; private set; }

    public JudgmentResultController judgmentResultController { get; set; }

    public GameStatus gameStatus;// { get; private set; } = GameStatus.Idle;

    private SoundInstance _BgmSoundInstance;

    public System.Action onGameFinished;

    public bool comboActivated { get; set; }

    public const float maxHp = 100.0f;
    private float _Hp = maxHp;
    public float hp
    {
        get => _Hp;
        set
        {
            _Hp = value;
            _Hp = Mathf.Clamp(_Hp, 0.0f, 100.0f);
        }
    }

    private int _Score;
    public int score {
        get => _Score;
        set
        {
            _Score = value;
            _Score = Mathf.Clamp(_Score, 0, 9999);
        } 
    }

    private void Start()
    {
        _BgmSoundInstance = SoundManager.Instance.PlayEffectSound(_Bgm);

        _BgmSoundInstance.onSoundStopped += () =>
        {
            if (gameStatus != GameStatus.Pause)
            {
                if (Mathf.Approximately(_Hp, 0.0f))
                    SetGameStatus(GameStatus.GameOver);
                else if (_BgmSoundInstance._InitTime + _BgmSoundInstance._Length <= Time.time)
                    SetGameStatus(GameStatus.Finish);
            }
        };
    }

    private void Update()
    {
        if (Mathf.Approximately(_Hp, 0.0f))
        {
            _BgmSoundInstance.Stop();
        }
    }

    public void SetGameStatus(GameStatus newGameStatus)
    {

        IEnumerator GoToTitle(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.Instance.LoadScene("TitleScene");
        }

        if (gameStatus == newGameStatus) return;
        GameStatus prevGameStatus = gameStatus;

        gameStatus = newGameStatus;

        switch (gameStatus)
        {
            case GameStatus.Idle:
                break;

            case GameStatus.Play:

                if (prevGameStatus == GameStatus.Pause)
                {
                    Time.timeScale = 1.0f;
                    _BgmSoundInstance.Play();
                }
                else
                {
                    judgmentResultController.ShowResult(JudgmentType.Start);
                    PlayAnimationIdle();
                }
                break;

            case GameStatus.Pause:
                Time.timeScale = 0.0f;
                _BgmSoundInstance.Pause();
                break;

            case GameStatus.GameOver:
                {
                    judgmentResultController.ShowResult(JudgmentType.GameOver);
                    SoundManager.Instance.PlayEffectSound(_GameOver);
                    PlayAnimationIdle();
                    onGameFinished?.Invoke();

                    StartCoroutine(GoToTitle(6.0f));
                }
                break;

            case GameStatus.Finish:
                judgmentResultController.ShowResult(JudgmentType.Victory);
                SoundManager.Instance.PlayEffectSound(_Victory);
                PlayAnimationIdle();
                onGameFinished?.Invoke();

                StartCoroutine(GoToTitle(3.0f));
                break;
        }
    }



}
