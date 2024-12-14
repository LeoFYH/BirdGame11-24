using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel instance;
    public Text titleTxt;
    public Text descTxt;
    public Text levelTxt;
    public Text priceText;
    public Image progressFill;
    public Image IntimacyFill;
    public Image cursor;
    int price;
    GameObject go;
    int level;

    public void Init(GameObject go, int price, string s1, string s2, int level, float progress,float progress2,bool cursorOn)
    {
        gameObject.SetActive(true);
        this.level = level;
        this.go = go;
        this.price = price;
        titleTxt.text = s1;
        descTxt.text = s2;
        levelTxt.text = $"<color=yellow>{level}</color>/min";
        progressFill.fillAmount = progress;
        IntimacyFill.fillAmount= progress2;
        priceText.text = $"Sale x{price}";
        cursor.gameObject.SetActive(cursorOn);
    }

    public void Sell()
    {
        GameManager.Instance.coin += price;
        UIManager.Instance.coinTxt.text = GameManager.Instance.coin.ToString();
        Close();
        Destroy(go);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ToggleBar()
    {
        progressFill.gameObject.SetActive(false);
        IntimacyFill.gameObject.SetActive(true);
        cursor.gameObject.SetActive(true);

    }
    

}
