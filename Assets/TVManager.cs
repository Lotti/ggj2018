using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVManager : Singleton<TVManager>
{
    public GameObject razzo;
    public GameObject sbarre;
    
    public void ShowRazzoCazzo()
    {
        if (!razzo.activeSelf)
        {
            razzo.SetActive(true);
            sbarre.SetActive(false);
        }
    }
    public void ShowSbarre()
    {
        if (razzo.activeSelf)
        {
            razzo.SetActive(false);
            sbarre.SetActive(true);
        }
    }

}
