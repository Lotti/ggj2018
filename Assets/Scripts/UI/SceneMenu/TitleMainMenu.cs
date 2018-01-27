using UnityEngine;
using DG.Tweening;

public class TitleMainMenu : MonoBehaviour
{
    private float targetScale = 0.3f;
    private float animationDuration = 0.4f;

    void Start ()
    {
        StartAnimation();
	}

    public void StartAnimation()
    {
        transform.DOScale(targetScale, animationDuration).SetLoops(-1, LoopType.Yoyo);
    }

    // serve stopparlo?
    public void StopAnimation()
    {
        DOTween.Kill(transform);
    }
}
