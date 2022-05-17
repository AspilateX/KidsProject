using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MenuAnimator : MonoBehaviour
{
    [SerializeField]
    private RectTransform _bottomMenu;

    public void HideBottomMenu(float time, TweenCallback callback = null)
    {
        if (_bottomMenu != null)
            _bottomMenu.DOMoveY(-_bottomMenu.sizeDelta.y / 2f, time).SetEase(Ease.InOutSine).OnComplete(callback);
    }
    public void ShowBottomMenu(float time, TweenCallback callback = null)
    {
        if (_bottomMenu != null)
            _bottomMenu.DOMoveY(_bottomMenu.sizeDelta.y / 2f, time).SetEase(Ease.InOutSine).OnComplete(callback);
    }
}
