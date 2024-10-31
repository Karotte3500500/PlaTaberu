using UnityEngine;

public class SelectBattleDirector_n : MonoBehaviour
{
    [SerializeField]
    private ControlUI controlUI;

    public void ToBattleSelect(bool multi)
    {
        GlobalSwitch._IsMultiplayer = multi;

        controlUI.SwitchScene(multi ? "ToBattle" : "Battle");
    }
}
