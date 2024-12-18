using System;
using UnityEngine;
using UnityEngine.UI;

public class EggSaleItem : MonoBehaviour
{
    public Action onCheckLimitAction;
    
    [Header("单个鸟蛋的价格")]
    public int salePrice = 15;
    public Text text_Price;
    public Text text_Num;
    public Button btn_Add;
    public Button btn_Subtract;

    public int BoughtCount { get; private set; } = 0;

    private void OnEnable()
    {
        BoughtCount = 0;
        text_Num.text = BoughtCount.ToString();
        btn_Subtract.interactable = false;
        CheckCoinLess();
    }

    private void Start()
    {
        text_Price.text = salePrice.ToString();
        text_Num.text = BoughtCount.ToString();
        btn_Add.onClick.AddListener(OnAddClick);
        btn_Subtract.onClick.AddListener(OnSubtractClick);
    }

    private void OnAddClick()
    {
        BoughtCount++;
        text_Num.text = BoughtCount.ToString();
        GameManager.Instance.coin.Value -= salePrice;
        onCheckLimitAction?.Invoke();
        CheckCoinLess();
    }

    private void OnSubtractClick()
    {
        BoughtCount--;
        text_Num.text = BoughtCount.ToString();
        GameManager.Instance.coin.Value += salePrice;
        onCheckLimitAction.Invoke();
        CheckCoinLess();
    }

    private void CheckCoinLess()
    {
        btn_Subtract.interactable = BoughtCount > 0;
    }
}
