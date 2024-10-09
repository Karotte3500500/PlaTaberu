using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollectUIsDirector : MonoBehaviour
{
    [SerializeField]
    private Sprite[] infoImgs;
    [SerializeField]
    private Image infObj;

    private int infoIndex = 0;

    void Start()
    {

    }
    private void Update()
    {
        infObj.sprite = infoImgs[infoIndex];
    }

    public void SetImg(int num)
    {
        if ((infoIndex > 0 && infoIndex < infoImgs.Length - 1) || (infoIndex <= 0 && num > 0) || (infoIndex >= infoImgs.Length - 1 && num < 0))
            infoIndex += num;
    }
}
