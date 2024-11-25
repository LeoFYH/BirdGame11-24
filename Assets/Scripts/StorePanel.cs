using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    public Text coinTxt;
    public Button btn_Buy;

    private void Awake()
    {
        btn_Buy.onClick.AddListener(Buy);
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

    private void Buy()
    {
        Debug.Log("buy");
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
}
