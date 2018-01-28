using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnim : MonoBehaviour 
{
    public void EndAnimation() {
        StartSceneManager.Instance.credits.SetActive(false);

        var particles = StartSceneManager.Instance.particles;

        foreach (var par in particles)
        {
            var main = par.Value.GetComponent<ParticleSystemRenderer>();
            main.sortingOrder = 1;
        }

    }
}
