﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ButtonType
{
    NewGame,
    Options,
    Credits,
    Quit
}

public class ButtonMainMenuItem : MonoBehaviour
{


    public string textString;
    public ButtonType buttonType;

    public Button btn;
    //private Text stringText;

    private void Awake()
    {
       // var child = this.transform.GetChild(0);
        
        if (this.btn == null)
            this.btn = this.GetComponent<Button>();


        
        this.Init();
    }

    public void Init()
    {
        //this.stringText.text = textString;

        switch (buttonType)
        {
            case ButtonType.NewGame:
                this.btn.onClick.AddListener(delegate { this.NewGame(); });
              break;
            case ButtonType.Options:
                this.btn.onClick.AddListener(delegate { this.Options(); });
                break;
            case ButtonType.Credits:
                this.btn.onClick.AddListener(delegate { this.Credits(); });
                break;
            case ButtonType.Quit:
                this.btn.onClick.AddListener(delegate { this.Quit(); });
                break;
        }

    }

    private void NewGame()
    {
        AudioManager.Instance.PlayMenuButtonPressedSound();
        this.StartCoroutine(SceneManager.Instance.ChangeScene(Scenes.Main_1, Fade()));
       
    }

    private IEnumerator Fade()
    {
        var btns = StartSceneManager.Instance.buttons;

        var time = 0.85f;
        
        var particles = StartSceneManager.Instance.particles;

       
        //Destroy(UIManager.Instance.gameObject);

        foreach (var particleName in particles.Keys)
        {
            var main = particles[particleName].main;

            yield return new WaitForEndOfFrame();

            main.startSpeed = 115;
        }

        yield return new WaitForSecondsRealtime(0.45f);

        foreach (var btnName in btns.Keys)
        {
            
            btns[btnName].transform.DOScale(new Vector3(1.10f,1.10f,1.10f),
                                            time).
                         OnComplete(()=>
                         {
                            btns[btnName].transform.DOScale(new Vector3(0, 0, 0), 0.25f);
                         });
            AudioManager.Instance.PlayShipLaunchedSound();
                         
        }

        yield return new WaitForSecondsRealtime(1.55f);


    }

    private void Options()
    {
        AudioManager.Instance.PlayMenuButtonPressedSound();
        Debug.Log("Options");
    }


    private void Credits()
    {
        AudioManager.Instance.PlayMenuButtonPressedSound();

        if (!StartSceneManager.Instance.credits.activeInHierarchy)
            StartSceneManager.Instance.credits.SetActive(true);

        var particles = StartSceneManager.Instance.particles;

        foreach (var par in particles)
        {
            var main = par.Value.GetComponent<ParticleSystemRenderer>();
            main.sortingOrder = 5;
        }


        Debug.Log("Credits");
    }

    private void Quit()
    {
        AudioManager.Instance.PlayMenuButtonPressedSound();
#if !UNITY_EDITOR
        Application.Quit();
#else
        Debug.Log(" QUIT ");
#endif
    }
}
