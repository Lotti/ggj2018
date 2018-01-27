using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : A3DClickable
{

    public static Color activeColor = Color.green;
    public static Color nonActiveColor = Color.red;

    public int Tick = 0;

    //public Light buttonLight;

    public bool isActive = false;

    public override void OnClick()
    {
        this.isActive = !this.isActive;
        //this.buttonLight.gameObject.SetActive(this.isActive);

        //this.rend.GetComponent<Renderer>().material.SetColor("_Color", (this.isActive ? activeColor : nonActiveColor));

        this.transform.DOLocalMoveY(((!this.isActive) ? 2.079f : 1.969f), 1);

    }
}
