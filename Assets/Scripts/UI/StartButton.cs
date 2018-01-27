using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : A3DClickable
{
    public Action OnClickCallback;
    public static Color activeColor = Color.green;
    public static Color nonActiveColor = Color.red;

    public bool isActive = false;

    public override void OnClick()
    {
        if (!isActive)
        {
            this.isActive = !this.isActive;
            var mov = this.transform.DOLocalMoveY( 1.969f, 0.5f);
            mov.onComplete += () =>
            {
                mov = this.transform.DOLocalMoveY(2.079f, 0.5f);
                mov.onComplete += () =>
                {
                    this.isActive = !this.isActive;
                    OnClickCallback();
                };
            };
        }
        
    }
}
