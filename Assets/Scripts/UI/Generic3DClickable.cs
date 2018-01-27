using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic3DClickable : A3DClickable {

    public Action OnClickCallback;

    public Transform feedbackTrans;
    public override void OnClick()
    {   
        OnClickCallback();
        var asd=this.feedbackTrans.DOLocalMoveY( 1.969f, 0.5f);
        asd.onComplete += () => {
            this.feedbackTrans.DOLocalMoveY( 2.245f, 0.5f);
        };
    }
}
