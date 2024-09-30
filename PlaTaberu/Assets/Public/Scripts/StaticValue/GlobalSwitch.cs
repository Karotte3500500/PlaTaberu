using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalSwitch
{
    //FPSを表示するか
    public static bool _DisplaysFPS = true;
    //切り替え先のシーン名
    public static string SwitchingScenes = "Start";
    //Bookで表示するキャラクターのID
    public static int _CharacterID = 1;
}
