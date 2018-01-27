using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteAlienSector : GenericSectorCalculator, ISector {

    bool _isDestroyed = false;

    public WhiteAlienSector()
    {
        SectorDamage = 0;
        SectorTemperature = 0;
        SectorConsume = 1;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += 2;

        spaceship.Fuel--;

        MissionLog.Instance.AddLog("Ah ah! nice gift from an unexpected friend!");

    }

    public ISector Clone() {
        return new WhiteAlienSector();
    }

    public string prefabName()
    {
        return "whiteAlien";
    }
}
