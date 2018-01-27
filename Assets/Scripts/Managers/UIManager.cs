using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager> 
{
    public GameObject PaperLogPrefab;
    public Transform PaperLogStartTransform;
    public Transform PaperLogFirstAnimationTranform;

    public List<GameObject> ScartoffieSpawnate;

    public TextMeshPro HumanText;

    public Generic3DClickable HumanPlusButton;
    public Generic3DClickable HumanMinusButton;

    private int _HumansToSend = 0;
    public int HumansToSend { get { return _HumansToSend; }
        set {
            this._HumansToSend = value;
            this.UpdateHumansToSend();
            }
    }

    private void Awake()
    {
        this.ScartoffieSpawnate = new List<GameObject>();
        this.GeneratePaperLog("AAAHAHAHAHAHAH", null);
        HumanPlusButton.OnClickCallback += this.onplusbutton;
        HumanMinusButton.OnClickCallback += this.onminusbutton;
        this.HumansToSend = 0;
    }

    private void UpdateHumansToSend()
    {
        this.HumanText.text = "HUMANS\n" + this.HumansToSend;
    }

    private void onminusbutton()
    {
        this.HumansToSend = Math.Max(0, this.HumansToSend-1);
    }

    private void onplusbutton()
    {
        this.HumansToSend = Math.Min(1000, this.HumansToSend + 1);
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


}
