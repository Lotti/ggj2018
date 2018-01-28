using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : Singleton<MainCameraManager>
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
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
            
                var button = hit.transform.GetComponent<A3DClickable>();
                if (button != null)
                {
                    button.OnClick();
                    try
                    {
                        var bbb = (SituaButton)button;
                        if (bbb != null)
                        {
                            var mod = GameManager.Instance.SpaceShip.GetModForTick(bbb.Tick);
                            SbarraManager.Instance.UpdateBarra(mod[1], mod[2], mod[3]);
                        }
                    }
                    catch(Exception ex) { TVManager.Instance.ShowRazzoCazzo(); }
                    

                    return;
                }
                /*
                var paper = hit.transform.GetComponent<PaperThing>();
                if (paper != null)
                {
                    paper.OnClick(this.paperThing);
                    return;
                }*/
            }
            else
            {
                var button = hit.transform.GetComponent<SituaButton>();
                if (button != null)
                {
                    var mod=GameManager.Instance.SpaceShip.GetModForTick(button.Tick);
                    SbarraManager.Instance.UpdateBarra(mod[1], mod[2], mod[3]);
                    return;
                }
                TVManager.Instance.ShowRazzoCazzo();
            }
        }
    }
}
