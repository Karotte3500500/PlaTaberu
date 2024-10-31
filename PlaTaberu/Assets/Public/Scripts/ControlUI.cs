using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [SerializeField, Header("������canvas���i�[")]
    private RectTransform canvas;

    //UI�̃v���n�u�����W���w�肵�ĊȒP�ɕ���
    public GameObject SetUI(GameObject ui, Vector2 point)
    {
        return Instantiate(ui, point, Quaternion.identity, canvas);
    }
    //UI�̃v���n�u�����W���w�肹���ȒP�ɕ���
    public GameObject SetUI(GameObject ui)
    {
        return Instantiate(ui, canvas);
    }

    //�V�[����؂�ւ���
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
