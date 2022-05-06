using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class UITweener : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool _isTweening = false;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void TweenY(float tweenLength)
    {
        if (!_isTweening)
        {
            _isTweening = true;
            _rectTransform.DOLocalMoveY(tweenLength, 0.15f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                _isTweening = false;
            });
        }
    }
}
