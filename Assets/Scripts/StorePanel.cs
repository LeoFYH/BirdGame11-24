using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    private static StorePanel _instance;

    public static StorePanel Instance
    {
        get
        {
            return _instance;
        }
    }

    public Text coinTxt;

    private void Awake()
    {
        _instance = this;
    }

    public void Init()
    {
        coinTxt.text = GameManager.Instance.coin.ToString();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Buy()
    {
        if (GameManager.Instance.noOpenEggs > 0)
        {
            UIManager.Instance.CreatePrompt("还有蛋没孵化");
            return;
        }
        if (GameManager.Instance.coin >= GameManager.Instance.eggPackage)
        {
            GameManager.Instance.coin -= GameManager.Instance.eggPackage;
            coinTxt.text = GameManager.Instance.coin.ToString();
            UIManager.Instance.coinTxt.text = GameManager.Instance.coin.ToString();
            GameManager.Instance.CreateBrid();
            Close();
        }
        else
        {
            UIManager.Instance.CreatePrompt("金币不足");
        }
    }

    public void RefreshCoin()
    {
        coinTxt.text = GameManager.Instance.coin.ToString();
    }
}
