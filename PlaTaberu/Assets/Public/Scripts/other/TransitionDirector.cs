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
        //子オブジェクトを全て取得
        for (int i = 0; i < this.transform.childCount; i++)
            objs.Add(this.transform.GetChild(i).gameObject);

        //透明に初期化
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
        //フェードイン
        foreach (var obj in objs)
        {
            Image image;
            image = obj.GetComponent<Image>();
            Color color = image.color;
            color.a += 0.04f;
            image.color = color;
        }

        //180フレームで遷移
        if (count > 80)
            SceneManager.LoadScene(GlobalSwitch.SwitchingScenes);
        count++;
    }
}
