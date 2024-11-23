using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject[] bridPre;

    private void OnMouseDown()
    {
        int val = Random.Range(0, bridPre.Length);
        GameObject go = Instantiate(bridPre[val]);
        go.transform.position = transform.position;
        GameManager.Instance.noOpenEggs--;
        Destroy(gameObject);
    }
}
