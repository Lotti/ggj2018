using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulosaSector : GenericSectorCalculator,ISector 
{
    public NebulosaSector()
    {
        SectorDamage = 1;
        SectorTemperature = 0;
        SectorConsume = 1;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

    }


    public ISector Clone() {
        return new NebulosaSector();
    }

    public string prefabName()
    {
        return "nebulosa";
    }

}
