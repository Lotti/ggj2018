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
        spaceship.HP = 5;

        spaceship.Temp = 5;

        spaceship.Fuel += 1;

        spaceship.Fuel--;

        MissionLog.Instance.AddLog("Space Station: Hp" + spaceship.HP + " - Temp" +spaceship.Temp+ " - Fuel" +spaceship.Fuel);

    }

    public ISector Clone() {
        return new BaseStationSector();
    }

    public string prefabName() {
        return "baseStation";
    }
}
