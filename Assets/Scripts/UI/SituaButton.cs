using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SituaButton : A3DClickable
{
    public static Color activeColor = Color.green;
    public static Color nonActiveColor = Color.red;

    public int Tick = 0;
    public ActionType type = ActionType.NONE;

    public Light buttonLight;

    public bool isActive = false;

    public GameObject rend;

    public override void OnClick()
    {
        this.isActive = !this.isActive;
        this.buttonLight.gameObject.SetActive(this.isActive);

        this.rend.GetComponent<Renderer>().material.SetColor("_Color", (this.isActive ? activeColor : nonActiveColor));

        this.rend.transform.DOLocalMoveY(((!this.isActive)?0:-0.066f), 1);
        
    }
}
