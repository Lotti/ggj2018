using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnim : MonoBehaviour 
{
    public void EndAnimation() {
        StartSceneManager.Instance.credits.SetActive(false);
    }
}
