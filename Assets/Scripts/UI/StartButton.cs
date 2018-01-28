using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : A3DClickable
{
    public Action OnClickCallback;
    public static Color activeColor = Color.red;
    public static Color nonActiveColor = Color.gray;
    public Renderer rend;
    public bool isActive = false;
    public bool Enabled = false;

    private void Update()
    {
        this.Enabled = (UIManager.Instance.HumansToSend > 0);

        rend.material.SetColor("_Color", this.Enabled ? activeColor : nonActiveColor);
        rend.material.SetColor("_EmissionColor", this.Enabled ? activeColor : nonActiveColor);

    }
    public override void OnClick()
    {
        if (!this.Enabled) return;

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
