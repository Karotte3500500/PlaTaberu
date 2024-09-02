using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliManagement : MonoBehaviour
{
    //表情
    private List<GameObject> faces;
    private Animator anim;

    private CharacterManager_n characterManager;

    private void Start()
    {
        /*初期化*/
        anim = this.GetComponent<Animator>();
        characterManager = transform.parent.GetComponent<CharacterManager_n>();
        faces = new List<GameObject>()
        {
            transform.Find("face1").gameObject,
            transform.Find("face2").gameObject,
            transform.Find("face3").gameObject,
            transform.Find("face4").gameObject,
        };
    }

    private void LateUpdate()
    {
        //効果の選択
        transform.Find("kao/question").gameObject.SetActive(characterManager.Question);
        transform.Find("kao/exclamation").gameObject.SetActive(characterManager.Exclamation);
        transform.Find("kao/tere").gameObject.SetActive(characterManager.tere);
        transform.Find("kao/doyon").gameObject.SetActive(characterManager.douyo);

        //表情の選択
        int index = 1;
        foreach(var face in faces)
        {
            face.SetActive(characterManager.CharacterFace == index);
            index++;
        }

        //アニメーションの選択
        anim.SetInteger("MotionNum", characterManager.CharacterAnimation);
        if(characterManager.CharacterAnimation == 2 || characterManager.CharacterAnimation == 3)
        {
            characterManager.CharacterAnimation = 1;
        }
    }
}
