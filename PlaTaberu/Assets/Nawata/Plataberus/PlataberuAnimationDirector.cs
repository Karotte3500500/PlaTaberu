using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlataberuAnimationDirector : MonoBehaviour
{
    //�\��
    public List<GameObject> faces;
    public Animator anim;

    public CharacterManager_n characterManager;

    private void Start()
    {
        /*������*/
        anim = this.GetComponent<Animator>();
        characterManager = transform.parent.GetComponent<CharacterManager_n>();
        faces = new List<GameObject>()
        {
            transform.Find("face1").gameObject,
            transform.Find("face2").gameObject,
            transform.Find("face3").gameObject,
            transform.Find("face4").gameObject,
        };

        start();
    }

    public virtual void start() { }

    private void LateUpdate()
    {
        //���ʂ̑I��
        transform.Find("kao/question").gameObject.SetActive(characterManager.Question);
        transform.Find("kao/exclamation").gameObject.SetActive(characterManager.Exclamation);
        transform.Find("kao/tere").gameObject.SetActive(characterManager.tere);
        transform.Find("kao/doyon").gameObject.SetActive(characterManager.douyo);

        //�\��̑I��
        int index = 1;
        foreach (var face in faces)
        {
            face.SetActive(characterManager.CharacterFace == index);
            index++;
        }

        //�A�j���[�V�����̑I��
        anim.SetInteger("MotionNum", characterManager.CharacterAnimation);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idol")
        {
            characterManager.CharacterAnimation = 1;
        }

        update();
    }

    public virtual void update() { }
}
