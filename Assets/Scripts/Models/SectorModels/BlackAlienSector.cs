using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAlienSector :  GenericSectorCalculator, ISector {

    bool _isDestroyed = false;

    public BlackAlienSector()
    {
        SectorDamage = 2;
        SectorTemperature = 0;
        SectorConsume = 0;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        var app = (int)CalcModDMG(spaceship.ModHP[tick]);
        spaceship.HP += app;

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

        Debug.Log(spaceship.ModHP[tick]);

        if ( app < 0 ) 
        {
            MissionLog.Instance.AddLog("Ehi Boss! An unidentified hostile starship fire at us!!We have some damage on the ship");
        }else{
            MissionLog.Instance.AddLog("We had a figth with an enemy alien, but we are safe!!");
        }
    }


    public ISector Clone() {
        BlackAlienSector c = new BlackAlienSector();
        c._isDestroyed = false;
        return c;
    }

    public string prefabName()
    {
        return "blackAlien";
    }
}
