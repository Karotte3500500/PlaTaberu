using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDirector_n : MonoBehaviour
{
    [SerializeField]
    private GameObject transition;
    [SerializeField]
    private RectTransform canvas;

    public void ToGrowScene()
    {
        Instantiate(transition, canvas);
    }
}
