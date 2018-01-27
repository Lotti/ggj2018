using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> 
{
    public GameObject PaperLogPrefab;
    public Transform PaperLogStartTransform;
    public Transform PaperLogFirstAnimationTranform;

    public List<GameObject> ScartoffieSpawnate;

    private void Awake()
    {
        this.ScartoffieSpawnate = new List<GameObject>();
        this.GeneratePaperLog("AAAHAHAHAHAHAH", null);
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
