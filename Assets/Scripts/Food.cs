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
        StartCoroutine(nameof(DestroyDelay));
    }

    private IEnumerator DestroyDelay()
    {
        var frame = new WaitForFixedUpdate();
        float time = 0f;
        while (time < 4f)
        {
            if (isTargeted)
            {
                yield break;
            }
            time += Time.deltaTime;
            yield return frame;
        }
        GameManager.Instance.RecycleFood(this);
    }
}
