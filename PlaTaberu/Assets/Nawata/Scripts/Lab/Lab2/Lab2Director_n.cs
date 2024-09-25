using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab2Director_n : MonoBehaviour
{
    public List<int> test = PlayerData._RecodedPlataberu;

    private void Start()
    {
        PlayerData.LoadPlayerData();
    }

    private void Update()
    {
        PlayerData.SavePlayerData();
    }
}
