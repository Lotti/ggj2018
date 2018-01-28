using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVManager : Singleton<TVManager>
{
    public GameObject razzo;
    public GameObject sbarre;

    public bool ShowingRazzo = true;

    public int okLayer;
    public int nonVisLayer;

    private void Start()
    {
        this.ShowRazzoCazzo();
    }
    public void ShowRazzoCazzo()
    {
        if (!ShowingRazzo)
        {
            this.ShowingRazzo = !ShowingRazzo;
            this.SetLayerRecursively(razzo, okLayer);
            this.SetLayerRecursively(sbarre, nonVisLayer);
        }
    }
    public void ShowSbarre()
    {
        if (ShowingRazzo)
        {
            this.ShowingRazzo = !ShowingRazzo;
            this.SetLayerRecursively(razzo, nonVisLayer);
            this.SetLayerRecursively(sbarre, okLayer);
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
