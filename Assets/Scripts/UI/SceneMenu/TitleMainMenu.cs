using UnityEngine;
using DG.Tweening;

public class TitleMainMenu : MonoBehaviour
{
    public float animationDuration;
    public float targetScale = 0.3f;

    void Awake ()
    {
        StartAnimation();
	}

    public void StartAnimation()
    {
        transform.DOScale(1.15f, animationDuration).SetLoops(-1,LoopType.Yoyo);
    }

    // serve stopparlo?
    public void StopAnimation()
    {
        DOTween.Kill(transform);
    }
}
