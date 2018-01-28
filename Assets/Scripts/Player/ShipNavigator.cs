using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipNavigator : MonoBehaviour {
    private int step = 0;
    private int dieAt = -1;
    private List<float> amplitude = new List<float>() { -9f, -5f, -1f, 1f, 5f ,9f};
    public GameObject boom;
    // Use this for initialization

    void Awake() {
        boom.SetActive(false);    
    }

    void Start() {
        step = 0;
    }

    Vector3 targetForward;

    // Update is called once per frame
    void Update() {
        this.transform.right = Vector3.Lerp(this.transform.right, this.targetForward, 0.1f);
    }

    public ShipNavigator SetDieAt(int dieAt) {
        this.dieAt = dieAt;
        return this;
    }

    public ShipNavigator Move() {
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
            int mStep = step;
            this.transform.DOPath(points.ToArray(), floatDist, PathType.CatmullRom, PathMode.Sidescroller2D)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                if (dieAt == -1 || mStep < dieAt) {
                        this.Move();
                    }
                    else {
                        this.SelfDestruct();
                    }
                });
            step++;
        } else {
            Debug.Log("Hai vinto!");
            SceneManager.Instance.ChangeScene( Scenes.StartScene );
        }
        return this;
    }

    public void SelfDestruct() {
        boom.SetActive(true);

        Board.Instance.pickShip();
        Destroy(this.gameObject);
    }
}