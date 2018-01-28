using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Axis
{
    X,
    Y,
    Z,
    ALL
}

public enum MovementType
{
    Scale,
    Translation,
    Rotation
}

public class MoveBackgroundElements : MonoBehaviour
{
    public float speedMovement;
    public float offsetMovement;
    public float durationBetweenAnimation;

    public LoopType loopType = LoopType.Yoyo;
    public Axis axisType = Axis.Z;
    public MovementType movementType = MovementType.Translation;

    public void Awake()
    {

        this.SetMovement(this.axisType);
    }

    public void SetMovement(Axis axisType)
    {
        switch (this.movementType)
        {
            case MovementType.Translation:
                switch (this.axisType)
                {
                    case Axis.X:
                        this.transform.DOMoveX(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                    case Axis.Y:
                        this.transform.DOMoveY(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                    case Axis.Z:
                        this.transform.DOMoveZ(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                    case Axis.ALL:
                             this.transform.DOMove(new Vector3(this.offsetMovement,
                                                               this.offsetMovement,
                                                               this.offsetMovement), this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                             break;
                }
                break;

            case MovementType.Scale:
                switch (this.axisType)
                {

                    case Axis.X:
                        this.transform.DOScaleX(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                    case Axis.Y:
                        this.transform.DOScaleY(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                    case Axis.Z:
                        this.transform.DOScaleZ(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                    case Axis.ALL:
                        this.transform.DOScale(this.offsetMovement,
                                               this.durationBetweenAnimation).SetLoops(-1, this.loopType);
                        break;
                }
                break;
        }
    }
}
