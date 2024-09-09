using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyManagement : PlataberuAnimationDirector
{
    private int count = 0;
    private bool after = false;

    public override void update()
    {
        if (base.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "eat" ||
            characterManager.CharacterAnimation == 3)
        {
            characterManager.CharacterFace = 1;
            characterManager.tere = true;
            characterManager.Exclamation = true;
            after = true;
        }
        else if (after)
        {
            characterManager.CharacterFace = 4;
            characterManager.tere = false;
            characterManager.Exclamation = false;
            count = 0;
            after = false;
        }
        else
        {
            count++;
            if (count > 300)
            {
                characterManager.CharacterFace = Random.Range(1, 5);
                count = 0;
            }
        }
    }
}
