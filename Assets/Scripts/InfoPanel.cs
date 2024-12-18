using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Text titleTxt;
    public Text descTxt;
    public Text levelTxt;
    public Text priceText;
    public Image progressFill;
    public Image loveFill;
    public GameObject progress_Exp;
    public GameObject progress_Love;
    int price;
    GameObject go;
    int level;

    public void Init(GameObject go, int price, string s1, string s2, int level, float progress, bool isSmall)
    {
        gameObject.SetActive(true);
        this.level = level;
        this.go = go;
        this.price = price;
        titleTxt.text = s1;
        descTxt.text = s2;
        levelTxt.text = $"<color=yellow>{level}</color>/min";
        if (isSmall)
        {
            progress_Exp.SetActive(true);
            progressFill.fillAmount = progress;
            progress_Love.SetActive(false);
        }
        else
        {
            progress_Love.SetActive(true);
            loveFill.fillAmount = progress;
            progress_Exp.SetActive(false);
        }

        priceText.text = $"Sale x{price}";
    }

    private void Start()
    {
        GameManager.Instance.coin.Register(v =>
        {
            UIManager.Instance.coinTxt.text = v.ToString();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        UIManager.Instance.coinTxt.text = GameManager.Instance.coin.Value.ToString();
    }

    public void Sell()
    {
        GameManager.Instance.coin.Value += price;
        
        Close();
        Destroy(go);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
