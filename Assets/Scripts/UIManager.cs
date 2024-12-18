using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public StorePanel storePanel;
    public InfoPanel infoPanel;
    public GameObject promptPre;
    public Text coinTxt;

    private void Awake()
    {
        Instance = this;
        GameManager.Instance.coin.Register(v =>
        {
            coinTxt.text = v.ToString();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        coinTxt.text = GameManager.Instance.coin.Value.ToString();
    }

    public void CreatePrompt(string s)
    {
        GameObject go = Instantiate(promptPre, transform);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<PromptPanel>().Init(s);
    }

    public void OnClick()
    {
        storePanel.Init();
    }

    public void ShowInfoPanel(GameObject go, int price, string s1, string s2, int level, float progress, bool isSmall)
    {
        infoPanel.Init(go, price, s1, s2, level, progress, isSmall);
    }
}
