using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject[] bridPre; // 可能生成的鸟的预制体
    public Sprite[] eggSprites; // 蛋动画的每一帧图片
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0; // 当前显示的帧索引

    private void Start()
    {
        // 获取当前物体的 SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the Egg object!");
        }
    }

    private void OnMouseDown()
    {
        
        PlayNextFrame();

        
        if (currentFrame >= eggSprites.Length)
        {
            SpawnBird();
        }
    }

    private void PlayNextFrame()
    {
        if (currentFrame < eggSprites.Length)
        {
            spriteRenderer.sprite = eggSprites[currentFrame];
            currentFrame++;
        }
    }

    private void SpawnBird()
    {
        int val = Random.Range(0, bridPre.Length);
        GameObject go = Instantiate(bridPre[val]);

        // 设置生成鸟的位置
        go.transform.position = new Vector3(transform.position.x, -4, transform.position.z);

        // 更新 GameManager 的未开启蛋数量
        GameManager.Instance.noOpenEggs--;

        // 销毁当前蛋对象
        Destroy(gameObject);
    }
}
