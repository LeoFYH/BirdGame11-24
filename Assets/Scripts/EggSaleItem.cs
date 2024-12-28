using System;
using UnityEngine;
using UnityEngine.UI;

public enum BirdType
{
    Bird_Common = 0,
    Bird_Rare = 1,
    Bird_Epic = 2,
    Bird_Legendary = 3,
    Bird_Mythic = 4
}

public class EggSaleItem : MonoBehaviour
{
    public Action onCheckLimitAction;
    [Header("类型")]
    public BirdType type;
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
