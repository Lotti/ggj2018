﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSector : GenericSectorCalculator, ISector
{

    public BlackHoleSector()
    {
        SectorDamage = 1;
        SectorTemperature = 2;
        SectorConsume = 2;
    }


    public void RunSector(ISpaceShip spaceship, int tick)
    {

        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

        MissionLog.Instance.AddLog("BlackHole: Hp" + spaceship.HP + " - Temp" + spaceship.Temp + " - Fuel " + spaceship.Fuel);


        //MissionLog.Instance.AddLog("Flyed near a BlackHole: " + );

    }


    public ISector Clone()
    {
        return new BlackHoleSector();
    }

    public string prefabName()
    {
        return "blackHole";
    }

}
