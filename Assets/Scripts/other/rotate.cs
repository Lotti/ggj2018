using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public float vel=5;

	void Update ()
    {
        this.transform.RotateAround(this.transform.position, this.transform.up, this.vel * Time.deltaTime);
	}
}
