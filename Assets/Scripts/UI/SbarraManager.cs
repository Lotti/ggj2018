using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SbarraManager : Singleton<SbarraManager>
{
    public Transform Barra1;
    public Transform Barra2;
    public Transform Barra3;

    private Vector3 endScale1 = Vector3.zero;
    private Vector3 endScale2 = Vector3.zero;
    private Vector3 endScale3 = Vector3.zero;

    private void Update()
    {
        try
        {
            this.Barra1.localScale = Vector3.Lerp(this.Barra1.localScale, this.endScale1, 0.1f);
            this.Barra2.localScale = Vector3.Lerp(this.Barra2.localScale, this.endScale2, 0.1f);
            this.Barra3.localScale = Vector3.Lerp(this.Barra3.localScale, this.endScale3, 0.1f);
        }
        catch(Exception exc) { }
        
    }

    public void UpdateBarra(float val1, float val2, float val3)
    {
        TVManager.Instance.ShowSbarre();
        this.endScale1 = new Vector3(1, 3 * val1, 3);
        this.endScale2 = new Vector3(1, 3 * val2, 3);
        this.endScale3 = new Vector3(1, 3 * val3, 3);

    }

}
