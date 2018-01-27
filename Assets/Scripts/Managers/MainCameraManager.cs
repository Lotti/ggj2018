using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{

    private Camera cam;
    private Vector3 firstForward;
    public Vector2 MaxCameraMovement = Vector2.one;

    public Transform paperThing;
    private void Start()
    {
        cam = this.GetComponent<Camera>();
        firstForward = this.cam.transform.forward;
    }
    Vector3 currentPointOnScreen;
    void Update()
    {
        currentPointOnScreen = Input.mousePosition;

        Vector3 top = MaxCameraMovement.y * this.cam.transform.up * ( (currentPointOnScreen.y / this.cam.pixelHeight) - 0.5f);
        Vector3 right = MaxCameraMovement.x * this.cam.transform.right * ((currentPointOnScreen.x / this.cam.pixelWidth) - 0.5f);

        this.cam.transform.forward = Vector3.Lerp(
            this.cam.transform.forward,
            this.firstForward + top + right, 0.1f);
           
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                var button = hit.transform.GetComponent<SituaButton>();
                if (button != null)
                {
                    button.OnClick();
                    return;
                }

                var paper = hit.transform.GetComponent<PaperThing>();
                if (paper != null)
                {
                    paper.OnClick(this.paperThing);
                    return;
                }
            }
        }
    }
}
