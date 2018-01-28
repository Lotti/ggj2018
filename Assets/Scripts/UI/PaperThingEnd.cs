using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class PaperThingEnd : A3DClickable
{

    public static PaperThing CurrentPaperThing = null;

    private Vector3 prevPosition;
    private Quaternion prevRotation;

    private Transform nextTransform;

    private bool isInFront=false;
    private bool isAnimating = false;
    private bool isMakingFirstAnimating = false;

    public TextMeshPro text;

    Vector3 _origin; 
    Vector3 _target = new Vector3 (0f,0f,1.17f);

    private void Awake() {
        _origin = transform.localPosition;
        GameManager.Instance.OnGameEnd += Instance_OnGameEnd;
    }

    void Instance_OnGameEnd ( bool win ) {
        //SetText("Broken transmission\nWe are receiving some images from ours startships!!");
        //Show();
        StartCoroutine( _DelayPlay() );
    }

    IEnumerator _DelayPlay(){
        yield return new WaitForSeconds( 5f );
        SceneManager.Instance.ChangeScene( Scenes.Player );
    }

    public void Show(){
        gameObject.SetActive( true );
        transform.DOLocalMove( _target, 0.5f );
    }

    public void Hide(){
        transform.DOLocalMove( _origin, 0.5f ).OnComplete(()=>{
            gameObject.SetActive( false );
        });
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public override void OnClick() {
        
    }

}
