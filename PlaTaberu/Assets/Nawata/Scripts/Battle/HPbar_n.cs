using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar_n : MonoBehaviour
{
    [SerializeField]
    private Slider bar;

    [SerializeField]
    private Text maxHp;
    [SerializeField]
    private Text hp;

    public float MaxValue;
    public float NowValue;

    private void Update()
    {
        bar.value = MaxValue > 0 ? NowValue / MaxValue : 0;
        maxHp.text = $"{(int)MaxValue}";
        hp.text = $"{(int)NowValue}";

        Color barColor =
             bar.value > 0.45f ? new Color(0.25f, 0.91f, 0.85f, 1.00f) :
            (bar.value > 0.75f ? new Color(0.25f, 1.00f, 0.55f, 1.00f) :
            (bar.value > 0.00f ? new Color(0.95f, 0.75f, 0.34f, 1.00f) : Color.clear));

        this.transform.Find("bar/Fill Area/Fill").GetComponent<Image>().color = barColor;
    }
}
