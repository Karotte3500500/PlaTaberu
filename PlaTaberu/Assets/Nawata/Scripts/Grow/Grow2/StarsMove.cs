using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsMove : MonoBehaviour
{
    public bool canMove;

    private void Update()
    {
        if (canMove)
        {
            Vector2 pos = transform.localPosition;
            pos.x -= 15;
            pos.y -= 15;
            transform.localPosition = pos;
        }
    }
}
