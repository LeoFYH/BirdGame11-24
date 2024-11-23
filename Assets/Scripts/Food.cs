using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    public bool isTargeted = false;
    public int hp = 1;
    float y;

    void Start()
    {
        y = transform.position.y;
        transform.DOMoveY(y-0.5f, 0.3f);
    }
}
