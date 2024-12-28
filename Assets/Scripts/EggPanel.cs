using System.Collections.Generic;
using UnityEngine;

public class EggPanel : MonoBehaviour
{
    public Sprite[] eggBGs;
    public GameObject eggItemSample;
    public Transform parent;

    private int eggCount;
    private EggItem[] eggs;
    private Dictionary<BirdType, int> boughts;

    public void Init(int count, Dictionary<BirdType, int> boughts)
    {
        eggCount = count;
        eggs = new EggItem[count];
        int index = 0;
        this.boughts = boughts;
        foreach (var bird in boughts)
        {
            for (int i = 0; i < bird.Value; i++)
            {
                var obj = GameObject.Instantiate(eggItemSample, parent);
                obj.SetActive(true);
                eggs[index] = obj.GetComponent<EggItem>();
                eggs[index].ThisImage.sprite = eggBGs[(int)bird.Key];
                eggs[index].BirdType = bird.Key;
                eggs[index].onCheckClickAction = CheckClick;
                index++;
            }
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
        //GameManager.Instance.CreateBirds(eggCount, birdObj);
        GameManager.Instance.CreateBirds(boughts);
        Destroy(gameObject);
    }
}
