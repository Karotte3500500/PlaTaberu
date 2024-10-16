using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionDirector : MonoBehaviour
{
    private List<GameObject> objs = new List<GameObject>();

    private int count = 0;

    private void Start()
    {
        objs.Add(this.gameObject);
        //�q�I�u�W�F�N�g��S�Ď擾
        for (int i = 0; i < this.transform.childCount; i++)
            objs.Add(this.transform.GetChild(i).gameObject);

        //�����ɏ�����
        foreach (var obj in objs)
        {
            Image image;
            image = obj.GetComponent<Image>();
            Color color = image.color;
            color.a = 0;
            image.color = color;
        }
    }

    private void Update()
    {
        //�t�F�[�h�C��
        foreach (var obj in objs)
        {
            Image image;
            image = obj.GetComponent<Image>();
            Color color = image.color;
            color.a += 0.04f;
            image.color = color;
        }

        //180�t���[���őJ��
        if (count > 80)
            SceneManager.LoadScene(GlobalSwitch.SwitchingScenes);
        count++;
    }
}
