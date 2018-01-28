using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject PaperLogPrefab;
    public Transform PaperLogStartTransform;
    public Transform PaperLogFirstAnimationTranform;

    public List<GameObject> ScartoffieSpawnate;

    public Dictionary<string,ParticleSystem> particles = new Dictionary<string, ParticleSystem>();
    public Dictionary<String, GameObject> buttons = new Dictionary<string, GameObject>();

    public TextMeshPro HumanText;

    public Generic3DClickable HumanPlusButton;
    public Generic3DClickable HumanMinusButton;

    public StartButton startButton;
    public Scenes scene;
    public event Action OnClick;

    private int _HumansToSend = 0;
    public int HumansToSend { get { return _HumansToSend; }
        set {
            this._HumansToSend = value;
            this.UpdateHumansToSend();
            }
    }

    // HO SONNO PERDONATEMI OH ME CHE HO PECCATO
    private void FillGraphicsComponent()
    {
        foreach (var GO in GameObject.FindObjectsOfType<ParticleSystem>())
        {
            if(!this.particles.ContainsKey(GO.name))
                this.particles.Add(GO.name,GO);
        }
        foreach (var btn in GameObject.FindObjectsOfType<GameObject>())
        {
            if (!this.buttons.ContainsKey(btn.name))
                if(btn.layer == 12) // Ho sempre sonno
                    this.buttons.Add(btn.name, btn);
        }
    }

    private void Start()
    {
        SceneManager.Instance.OnOpenScene += this.OnOpenScene;
        this.FillGraphicsComponent();
        this.ScartoffieSpawnate = new List<GameObject>();
        //this.GeneratePaperLog("AAAHAHAHAHAHAH", null);
        if (this.HumanPlusButton != null)
        {
            HumanPlusButton.OnClickCallback += this.onplusbutton;
            HumanMinusButton.OnClickCallback += this.onminusbutton;
            this.HumansToSend = 0;
            this.startButton.OnClickCallback += this.OnStartButton;
            MissionLog.Instance.OnTransmission += (string log) =>
              {
                  this.GeneratePaperLog(log, null);
              };
        }
    }

    private void OnStartButton()
    {
        GameManager.Instance.Launch(this.HumansToSend);
        this.UpdateHumansToSend();
        TVManager.Instance.ShowMonitor();
       // this.GeneratePaperLog("AHAHAHAH " + this.ScartoffieSpawnate.Count, null);
    }

    private void UpdateHumansToSend()
    {
        this.HumanText.text = "HUMANS\n" + this.HumansToSend+"/"+GameManager.Instance.Peoples;
    }

    private void onminusbutton()
    {
        this.HumansToSend = Math.Max(0, this.HumansToSend-10);
    }

    private void onplusbutton()
    {
        this.HumansToSend = Math.Min(GameManager.Instance.Peoples, this.HumansToSend + 10);
    }

    public void GeneratePaperLog(string log, Action oncomplete)
    {
        var go = GameObject.Instantiate(this.PaperLogPrefab, this.PaperLogStartTransform);
        go.transform.localPosition = Vector3.zero;
        var pt = go.GetComponent<PaperThing>();
        pt.SetText(log);
        pt.StartAnimation(
            this.PaperLogFirstAnimationTranform,
            oncomplete);
        
        this.ScartoffieSpawnate.Add(go);
    }

    public void OnOpenScene()
    {
        this.particles.Clear();
    }
}
