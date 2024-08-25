using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GRPbar_n : MonoBehaviour
{
    Slider bar;

    private void Start()
    {
        bar = this.GetComponent<Slider>();
    }

    private void Update()
    {
        bar.value = CharacterData._Plataberu.GrpRatio;
    }
}
