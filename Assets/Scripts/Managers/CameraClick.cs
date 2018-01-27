using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour {

    private Camera cam;
    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray=cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                var button = hit.transform.GetComponent<SituaButton>();
                if (button != null)
                {
                    button.OnClick();
                }
            }
        }
	}
}
