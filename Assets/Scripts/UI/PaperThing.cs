using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperThing : MonoBehaviour
{

    public static PaperThing CurrentPaperThing = null;

    public Vector3 prevPosition;
    public Quaternion prevRotation;

    public Transform nextTransform;

    public bool isInFront=false;
    public bool isAnimating = false;
    public void Start()
    {
        this.prevPosition = this.transform.position;
        this.prevRotation = this.transform.rotation;
    }
    public void OnClick(Transform nextTransform)
    {
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
