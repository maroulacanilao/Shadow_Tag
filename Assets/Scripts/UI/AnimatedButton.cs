using System;
using System.Collections;
using CustomHelpers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.EventSystems;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,ISelectHandler, IDeselectHandler
{
    public Button button;
    public float animationDuration = 0.3f;
    public Ease animationEase = Ease.OutQuad;
    public string selectSfxName = "ButtonSelect";
    public string clickSfxName = "ButtonClick";

    private Vector3 originalScale;

    private void Awake()
    {
        if(button == null) button = GetComponent<Button>();
        originalScale = button.transform.localScale;
    }
    
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        if(button == null) button = GetComponent<Button>();
        if(button == null) return;
        button.transform.localScale = originalScale;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        button.transform.localScale = originalScale;
    }

    private void OnClick()
    {
        StopAllCoroutines();
        if(!gameObject.activeInHierarchy) return;
        Managers.AudioManager.OnPlaySfx.Invoke(clickSfxName);
        StartCoroutine(Co_AnimateButton(originalScale * 0.9f));
    }

    private void OnHover()
    {
        if(!button.interactable) return;
        if(button.transform.localScale != originalScale) return;

        StopAllCoroutines();
        if(!gameObject.activeInHierarchy) return;
        Managers.AudioManager.OnPlaySfx.Invoke(selectSfxName);
        StartCoroutine(Co_AnimateButton(originalScale * 1.1f));
    }

    private void OnIdle()
    {
        if(button.transform.localScale == originalScale) return;
        StopAllCoroutines();
        StartCoroutine(Co_AnimateButton(originalScale));
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        OnIdle();
    }
    public void OnSelect(BaseEventData eventData)
    {
        OnHover();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        OnIdle();
    }
    
    private IEnumerator Co_AnimateButton(Vector3 targetScale_)
    {
        
        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.unscaledDeltaTime;
            float t = timer / animationDuration;
            float easedT = EasingFunction(t);

            button.transform.localScale = Vector3.Lerp(originalScale, targetScale_, easedT);
            yield return null;
        }

        button.transform.localScale = targetScale_;
    }

    private float EasingFunction(float t)
    {
        // Apply desired easing function here
        return t * t * (3f - 2f * t); // Quadratic easing
    }
}
