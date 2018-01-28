using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartSceneManager : Singleton<StartSceneManager> {

    public Dictionary<string, ParticleSystem> particles = new Dictionary<string, ParticleSystem>();
    public Dictionary<String, GameObject> buttons = new Dictionary<string, GameObject>();

    public event Action OnClick;

    // HO SONNO PERDONATEMI OH ME CHE HO PECCATO
    private void FillGraphicsComponent()
    {
        foreach (var GO in GameObject.FindObjectsOfType<ParticleSystem>())
        {
            if (!this.particles.ContainsKey(GO.name))
                this.particles.Add(GO.name, GO);
        }
        foreach (var btn in GameObject.FindObjectsOfType<GameObject>())
        {
            if (!this.buttons.ContainsKey(btn.name))
                if (btn.layer == 12) // Ho sempre sonno
                    this.buttons.Add(btn.name, btn);
        }
    }


    public void Start()
    {
        SceneManager.Instance.OnOpenScene += this.OnOpenScene;
        this.FillGraphicsComponent();
    }

    public void OnOpenScene()
    {
        this.particles.Clear();
    }
}
