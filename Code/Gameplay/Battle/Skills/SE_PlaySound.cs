using System.Collections.Generic;
using UnityEngine;

public static class SE_PlaySound
{
    public static void Activate(BattleCharacter caster, List<BattleCharacter> battleCharacters, SkillEffectData data)
    {
        var soundData = (SE_PlaySound_Data)data;

        Vector3 soundPosition = Vector3.zero;
        switch (soundData.SoundPosition)
        {
            case SE_PlaySound_Data.ESoundLocation.Self:
                soundPosition = caster.transform.position;
                break;
        }
        
        SoundManager.PlaySound(soundData.Sound);
    }
}