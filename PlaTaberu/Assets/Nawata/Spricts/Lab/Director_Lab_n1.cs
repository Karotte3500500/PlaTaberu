using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director_Lab_n1 : MonoBehaviour
{
    [SerializeField]
    private GameObject test;
    [SerializeField]
    private RectTransform testTrans;


    void Start()
    {
        Instantiate(test, testTrans);
    }
}
