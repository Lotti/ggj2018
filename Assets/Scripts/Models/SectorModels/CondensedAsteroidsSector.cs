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
        SectorDamage = 1;
        SectorTemperature = 0;
        SectorConsume = 1;
    }

    public void RunSector ( ISpaceShip spaceship, int tick ) 
    {
        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

    }


    public ISector Clone() {
        return new CondensedAsteroidsSector();
    }

    public string prefabName()
    {
        return "condensedAsteroid";
    }
}
