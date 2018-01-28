using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondensedAsteroidsSector : GenericSectorCalculator, ISector
{
    /*
     * ASTEROIDI CONDENSATI MA SENZA GLUTINE
     * 
     * FANNO 1 DANNO
     * NON TOCCANO LA TEMPERATURA
     * CONSUMI UN CARBURANTE
     * 
     */

    public CondensedAsteroidsSector()
    {
        SectorDamage = 2;
        SectorTemperature = 0;
        SectorConsume = 0;
    }

    public void RunSector ( ISpaceShip spaceship, int tick ) 
    {
        var app = (int)CalcModDMG(spaceship.ModHP[tick]);
        spaceship.HP += app;

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

       /* if (app < 0)
        {
            MissionLog.Instance.AddLog("Ehi Boss! we came across a band of asteroids that damaged our ship!");
        }
        else
        {
            MissionLog.Instance.AddLog("An asteroid has scratched our hull! ");
        }*/

        MissionLog.Instance.AddLog("Cond. Asteroid: Hp" + spaceship.HP + " - Temp" + spaceship.Temp + " - Fuel" + spaceship.Fuel);


    }


    public ISector Clone() {
        return new CondensedAsteroidsSector();
    }

    public string prefabName()
    {
        return "condensedAsteroid";
    }
}
