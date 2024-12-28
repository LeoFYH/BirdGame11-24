using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    public Text coinTxt;
    public Button btn_Close;
    public GameObject eggPanel;
    public EggSaleItem[] items;
    /// <summary>
    /// 购买最大数量
    /// </summary>
    public const int maxCount = 5;
    private int currentCount = 0;
    private Dictionary<BirdType, int> boughtDic = new Dictionary<BirdType, int>();

    private void OnEnable()
    {
        currentCount = 0;
        CheckLimit();
    }

    private void Start()
    {
        GameManager.Instance.coin.Register(v =>
        {
            coinTxt.text = v.ToString();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        coinTxt.text = GameManager.Instance.coin.ToString();
        
        btn_Close.onClick.AddListener(() =>
        {
            if (currentCount > 0)
            {
                var obj = GameObject.Instantiate(eggPanel, transform.parent);
                var panel = obj.GetComponent<EggPanel>();
                panel.Init(currentCount, boughtDic);
            }
            
            gameObject.SetActive(false);
        });

        foreach (var item in items)
        {
            item.onCheckLimitAction = CheckLimit;
        }
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }

    // private void Buy()
    // {
    //     Debug.Log("buy");
    //     if (GameManager.Instance.noOpenEggs > 0)
    //     {
    //         UIManager.Instance.CreatePrompt("There are also eggs that have not hatched");
    //         return;
    //     }
    //     if (GameManager.Instance.coin.Value >= GameManager.Instance.eggPackage)
    //     {
    //         GameManager.Instance.coin.Value -= GameManager.Instance.eggPackage;
    //         coinTxt.text = GameManager.Instance.coin.ToString();
    //         UIManager.Instance.coinTxt.text = GameManager.Instance.coin.ToString();
    //         GameManager.Instance.CreateBrid();
    //         Close();
    //     }
    //     else
    //     {
    //         UIManager.Instance.CreatePrompt("Insufficient coins");
    //     }
    // }

    private void CheckLimit()
    {
        boughtDic.Clear();
        currentCount = 0;
        foreach (var item in items)
        {
            if (item.BoughtCount > 0)
            {
                boughtDic.Add(item.type, item.BoughtCount);
            }

            currentCount += item.BoughtCount;
        }

        foreach (var item in items)
        {
            item.btn_Add.interactable = currentCount < maxCount && item.salePrice <= GameManager.Instance.coin.Value;
        }
    }
}
