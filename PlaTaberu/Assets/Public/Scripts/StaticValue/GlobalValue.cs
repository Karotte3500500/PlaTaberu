using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCharacterManagement;
using Unity.VisualScripting;

public static class GlobalValue
{
    public static string _PreviousScene = "Start";
    public static Plataberu enemy = new Grass();

    public static bool _BattleResult = false;

    public static int _Victory = -1;
}
