using UnityEngine;

public class EggPanel : MonoBehaviour
{
    public GameObject eggItemSample;
    public GameObject birdObj;
    public Transform parent;

    private int eggCount;
    private EggItem[] eggs;

    public void Init(int count)
    {
        eggCount = count;
        eggs = new EggItem[count];
        for (int i = 0; i < eggCount; i++)
        {
            var obj = GameObject.Instantiate(eggItemSample, parent);
            obj.SetActive(true);
            eggs[i] = obj.GetComponent<EggItem>();
            eggs[i].onCheckClickAction = CheckClick;
        }
    }

    private void CheckClick()
    {
        foreach (var egg in eggs)
        {
            if(!egg.IsClicked)
                return;
        }
        
        Invoke(nameof(Next), 0.5f);
    }

    private void Next()
    {
        GameManager.Instance.CreateBirds(eggCount, birdObj);
        Destroy(gameObject);
    }
}
