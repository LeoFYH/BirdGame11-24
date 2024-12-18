using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinAnim : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector3.zero;
        var target = transform.position;
        if (target.x < 0)
        {
            target = transform.position + Vector3.right;
        }
        else
        {
            target = transform.position + Vector3.left;
        }

        transform.DOJump(target, 1.5f, 1, 0.5f);
        transform.DOScale(1, 0.5f).OnComplete(() =>
        {
            var pos = new Vector3(7.5f, -4f, 0);
            transform.DOJump(pos, -1.5f, 1, 0.8f);
            transform.DOScale(0, 0.8f).OnComplete(() =>
            {
                GameManager.Instance.coin.Value += Random.Range(1, 4);
                Destroy(gameObject);
            });
        });
    }
}
