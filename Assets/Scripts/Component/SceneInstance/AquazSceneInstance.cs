using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AquazSceneInstance : DefaultSceneInstance
{
    public Dictionary<CharacterType, AquazCharacter> Characters = new Dictionary<CharacterType, AquazCharacter>();

    public Dictionary<CharacterType, AquazCharacter> characters => Characters;

    public void PlayAnimationIdle()
    {
        foreach (var characterInfo in Characters)
            characterInfo.Value.PlayIdleAnimation();
    }

    public void PlayAnimationPlay()
    {
        foreach (var characterInfo in Characters)
            characterInfo.Value.PlayPlayAnimation();
    }

    public void PlayAnimationCombo()
    {
        foreach (var characterInfo in Characters)
            characterInfo.Value.PlayComboAnimation();
    }




}
