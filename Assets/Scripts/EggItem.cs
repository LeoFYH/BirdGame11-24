using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EggItem : MonoBehaviour, IPointerClickHandler
{
    public Action onCheckClickAction;
    public bool IsClicked { get; private set; } = false;
    public Animator egg;
    public Image bird;

    private Image thisImage;

    private bool clicked = false;

    private void Start()
    {
        thisImage = GetComponent<Image>();
        thisImage.color = Color.white;
        egg.gameObject.SetActive(true);
        bird.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clicked) return;
        
        thisImage.color = new Color32(255, 255, 255, 0);
        egg.Play("Broken");
        clicked = true;
    }

    public void OnAnimationComplete()
    {
        egg.gameObject.SetActive(false);
        bird.gameObject.SetActive(true);
        IsClicked = true;
        onCheckClickAction?.Invoke();
    }
}
