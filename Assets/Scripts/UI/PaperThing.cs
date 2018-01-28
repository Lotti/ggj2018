using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class PaperThing : A3DClickable
{

    public static PaperThing CurrentPaperThing = null;

    private Vector3 prevPosition;
    private Quaternion prevRotation;

    private Transform nextTransform;

    private bool isInFront=false;
    private bool isAnimating = false;
    private bool isMakingFirstAnimating = false;

    public TextMeshPro text;

    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public void StartAnimation( Transform endAnimationTransform, Action OnComplete )
    {
        this.isMakingFirstAnimating = true;
        var tween = this.transform.DOMove(endAnimationTransform.position, 1);
        tween.onComplete += () => {
            Vector3 final2 = endAnimationTransform.position - 
            Vector3.forward *( 3 - UIManager.Instance.ScartoffieSpawnate.Count*0.5f )
            +Vector3.up*UIManager.Instance.ScartoffieSpawnate.Count * 0.05f;

            tween = this.transform.DOMove(final2, 1);
            tween.onComplete += () =>
            {
                this.isMakingFirstAnimating = false;
                this.InitVar();

                if(OnComplete!=null)
                    OnComplete();
            };
        };
    }
    void InitVar()
    {
        this.prevPosition = this.transform.position;
        this.prevRotation = this.transform.rotation;
    }
    public override void OnClick()
    {
        this.OnClick(MainCameraManager.Instance.paperThing);
    }
    public void OnClick(Transform nextTransform)
    {
        if (this.isMakingFirstAnimating) return;

        if (nextTransform != null && !isInFront)
        {
            if (CurrentPaperThing != null)
            {
                CurrentPaperThing.OnClick(null);
            }

            if (!this.isAnimating)
            {
                this.prevPosition = this.transform.position;
                this.prevRotation = this.transform.rotation;
            }

            CurrentPaperThing = this;
            this.isInFront = true;
            this.isAnimating = true;
            this.nextTransform = nextTransform;
        }
        else
        {
            this.isInFront = false;

        }
            
    }

    private void Update()
    {
        if (this.isMakingFirstAnimating) return;

        if (isInFront)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.nextTransform.position, 0.1f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.nextTransform.rotation, 0.1f);
        }
        else if( Vector3.SqrMagnitude(this.prevPosition -this.transform.position )>0.01f )
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.prevPosition, 0.1f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.prevRotation, 0.1f);
        }
        else if(this.isAnimating)
        {
            this.transform.position = this.prevPosition;
            this.transform.rotation = this.prevRotation;
            this.isAnimating = false;
        }
    }
}
