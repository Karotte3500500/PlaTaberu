using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StarMotion : MonoBehaviour
{
    private void Update()
    {
        RectTransform trans = this.GetComponent<RectTransform>();

        Vector3 rot = trans.localRotation.eulerAngles;
        rot.z += 3f;
        trans.localRotation = Quaternion.Euler(rot);
    }
}
