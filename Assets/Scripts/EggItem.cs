using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EggItem : MonoBehaviour, IPointerClickHandler
{
    public Action<BirdConfig> onCheckClickAction;
    public EggType EggType {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
            
        }
    }
    public bool IsClicked { get; private set; } = false;
    public Animator egg;
    public GameObject[] birds;
    public Image ThisImage {
        get
        {
            if (thisImage == null)
                thisImage = GetComponent<Image>();
            return thisImage;
        }
    }

    private Image thisImage;
    private EggType _type;

    private bool clicked = false;

    private void Start()
    {
        ThisImage.color = Color.white;
        egg.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clicked) return;
        
        ThisImage.color = new Color32(255, 255, 255, 0);
        egg.Play("Broken");
        clicked = true;
    }

    public void OnAnimationComplete()
    {
        egg.gameObject.SetActive(false);
        var bird = GameManager.Instance.RandomGetBird(EggType);
        birds[(int)bird.birdType].SetActive(true);
        IsClicked = true;
        onCheckClickAction?.Invoke(bird);
    }
}
