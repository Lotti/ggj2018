using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSectorCalculator
{
    public float SectorDamage { get; protected set; }

    protected float SectorTemperature;

    protected float SectorConsume;

    public GenericSectorCalculator()
    {
        
    }

    public float CalcModDMG(float modSpaceship)
    {
        float app = (SectorDamage>0)? (SectorDamage - modSpaceship):0;

        if (app > 0)
        {
            return -app;
        }
        else return 0;
    }


    public float CalcModTEMP(float modSpaceship)
    {
        float tempBonus = 0f;
        if (GameManager.Instance != null) {
            tempBonus = GameManager.Instance.tempBonus;
        }

        Debug.Log("SectorTemperature: " +SectorTemperature+ " modSpaceship: "+modSpaceship+" tempBonus: "+tempBonus);
        float app = SectorTemperature + (modSpaceship + tempBonus);
        return app;
       /* if (app > 0)
        {
            return -app;
        }
        else return 0;*/
    }


    public float CalcModFUEL(float modSpaceship)
    {
        return (SectorConsume + modSpaceship);


    }
}
