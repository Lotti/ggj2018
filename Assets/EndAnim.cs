using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnim : MonoBehaviour 
{
    public void EndAnimation()
    {
        this.GetComponent<Animation>().Stop();

        StartSceneManager.Instance.credits.SetActive(false);
    }
}
