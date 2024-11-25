using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Text titleTxt;
    public Text descTxt;
    public Text levelTxt;
    public Text priceText;
    public Image progressFill;
    int price;
    GameObject go;
    int level;

    public void Init(GameObject go, int price, string s1, string s2, int level, float progress)
    {
        gameObject.SetActive(true);
        this.level = level;
        this.go = go;
        this.price = price;
        titleTxt.text = s1;
        descTxt.text = s2;
        levelTxt.text = $"<color=yellow>{level}</color>/分钟";
        progressFill.fillAmount = progress;
        priceText.text = $"售卖 x{price}";
    }

    public void Sell()
    {
        GameManager.Instance.coin += price * level;
        UIManager.Instance.coinTxt.text = GameManager.Instance.coin.ToString();
        Close();
        Destroy(go);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
