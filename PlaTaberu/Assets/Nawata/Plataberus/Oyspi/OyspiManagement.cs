using UnityEngine;

public class OyspiManagement : PlataberuAnimationDirector
{
    private int count = 0;
    private bool after = false;

    public override void update()
    {
        if (base.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "eat" ||
            characterManager.CharacterAnimation == 3)
        {
            characterManager.CharacterFace = 3;
            after = true;
        }
        else if (after)
        {
            characterManager.CharacterFace = 1;
            count = 0;
            after = false;
        }
        else
        {
            count++;
            if (count > 200)
            {
                characterManager.CharacterFace = Random.Range(1, 5);
                count = 0;
            }
        }
    }
}
