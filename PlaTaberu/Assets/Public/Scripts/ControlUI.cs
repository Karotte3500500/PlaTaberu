using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [SerializeField, Header("ここにcanvasを格納")]
    private RectTransform canvas;

    //UIのプレハブを座標を指定して簡単に複製
    public GameObject SetUI(GameObject ui, Vector2 point)
    {
        return Instantiate(ui, point, Quaternion.identity, canvas);
    }
    //UIのプレハブを座標を指定せず簡単に複製
    public GameObject SetUI(GameObject ui)
    {
        return Instantiate(ui, canvas);
    }

    //シーンを切り替える
    public void SwitchScene(string sceneName)
    {
        GlobalValue._PreviousScene = GlobalSwitch.SwitchingScenes;
        GlobalSwitch.SwitchingScenes = sceneName;
        this.SetUI((GameObject)Resources.Load("Nawata/Transition"));
    }
    public void SwitchScene(string sceneName,bool memory)
    {
        GlobalSwitch.SwitchingScenes = sceneName;
        this.SetUI((GameObject)Resources.Load("Nawata/Transition"));
    }
}
