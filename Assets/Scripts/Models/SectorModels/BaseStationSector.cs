using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStationSector : GenericSectorCalculator, ISector {

    public BaseStationSector()
    {
        SectorDamage = 0;
        SectorTemperature = 0;
        SectorConsume = 0;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        spaceship.HP += 2;

        spaceship.Temp = 5;

        spaceship.Fuel += 2;

        spaceship.Fuel--;

        MissionLog.Instance.AddLog("We found a glimpse of hope. All Systems fixed");

    }

    public ISector Clone() {
        return new BaseStationSector();
    }

    public string prefabName() {
        return "baseStation";
    }
}
