using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Long, Octo, Start }

[RequireComponent(typeof(Animator))]
public sealed class AquazCharacter : MonoBehaviour
{
    [SerializeField] private CharacterType _CharacterType;

    private AquazSceneInstance _SceneInstance;

    public Animator animator { get; private set; }
    public CharacterType characterType => _CharacterType;



    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

        _SceneInstance = SceneManager.Instance.sceneInstance as AquazSceneInstance;
        _SceneInstance.characters.Add(_CharacterType, this);

        PlayIdleAnimation();
    }

    public void PlayIdleAnimation() =>
        animator.SetFloat("Blend", 0.0f);

    public void PlayPlayAnimation() =>
        animator.SetFloat("Blend", 1.0f);

    public void PlayComboAnimation()=>
        animator.SetFloat("Blend", 2.0f);

}
