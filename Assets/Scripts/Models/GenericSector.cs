using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSectorCalculator
{
    protected float SectorDamage;

    protected float SectorTemperature;

    protected float SectorConsume;

    public GenericSectorCalculator()
    {
        
    }

    public float CalcModDMG(float modSpaceship)
    {
        float app = SectorDamage - modSpaceship;

        if (app > 0)
        {
            return -app;
        }
        else return 0;
    }


    public float CalcModTEMP(float modSpaceship)
    {
        float app = SectorTemperature - modSpaceship;

        if (app > 0)
        {
            return -app;
        }
        else return 0;
    }


    public float CalcModFUEL(float modSpaceship)
    {
        float app = SectorConsume - modSpaceship;

        if (app > 0)
        {
            return -app;
        }
        else return 0;
    }
}
