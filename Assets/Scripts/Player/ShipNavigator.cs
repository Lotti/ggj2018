using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipNavigator : MonoBehaviour {
    private int step = 0;
    private List<float> amplitude = new List<float>() { -9f, -5f, -1f, 1f, 5f ,9f};
    private MiniSpaceScene currentMiniScene;

    void Start() {
        step = 0;
    }

    Vector3 targetForward;

    // Update is called once per frame
    void Update() {
        this.transform.right = Vector3.Lerp(this.transform.right, this.targetForward, 0.1f);
    }

    public void Move() {
        GameObject target = Board.Instance.getShipTarget(step);
        if (target != null) {
            var dist = this.transform.position - target.transform.position;
            this.targetForward = -dist.normalized;
            List<Vector3> points = new List<Vector3>();
            Vector3 midPoint = -dist * 0.5f + this.transform.position;
            int r = UnityEngine.Random.Range(0, amplitude.Count);
            midPoint += Vector3.Cross(-this.targetForward, new Vector3(0, 0, -1)) * amplitude[r] * 10;
            points.Add(midPoint);
            float floatDist = dist.magnitude * 0.02f;
            points.Add(target.transform.position);
            this.transform.DOPath(points.ToArray(), floatDist, PathType.CatmullRom, PathMode.Sidescroller2D)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    this.PlayStopAnimation(target);
                });
            step++;
        }
    }

    public void PlayStopAnimation(GameObject target)
    {
        currentMiniScene = target.GetComponent<MiniSpaceScene>();
        currentMiniScene.OnEndAnimation += PlayNextStep;
    }

    private void PlayNextStep()
    {
        currentMiniScene.OnEndAnimation -= PlayNextStep;
        Move();
    }
}