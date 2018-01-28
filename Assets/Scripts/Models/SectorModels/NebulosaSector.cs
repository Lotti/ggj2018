using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulosaSector : GenericSectorCalculator,ISector 
{
    public NebulosaSector()
    {
        SectorDamage = 0;
        SectorTemperature = 2;
        SectorConsume = 1;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        var app = CalcModTEMP(spaceship.ModTEMP[tick]);
        spaceship.Temp += app;

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

        /*if (app < 0)
        {
            MissionLog.Instance.AddLog("Ehi Boss! we don't know what happen but THE ROOF IS ON FIRE!!");
        }
        else
        {
            MissionLog.Instance.AddLog("We are in a nebula but our shields are working well! :)");
        }*/
        MissionLog.Instance.AddLog("Nebola: Hp" + spaceship.HP + " - Temp" + spaceship.Temp + " - Fuel " + spaceship.Fuel);


    }


    public ISector Clone() {
        return new NebulosaSector();
    }

    public string prefabName()
    {
        return "nebulosa";
    }

}
