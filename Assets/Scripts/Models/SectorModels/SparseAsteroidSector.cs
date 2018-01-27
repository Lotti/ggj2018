﻿
public class SparseAsteroidSector : GenericSectorCalculator, ISector
{
    public SparseAsteroidSector()
    {
        SectorDamage = 1;
        SectorTemperature = 0;
        SectorConsume = 1;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        var app = (int)CalcModDMG(spaceship.ModHP[tick]);
        spaceship.HP += app;

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

        if (app < 0)
        {
            MissionLog.Instance.AddLog("Ehi Boss! we came across a band of asteroids that damaged our ship!");
        }
        else
        {
            MissionLog.Instance.AddLog("An asteroid has scratched our hull! ");
        }

    }

    public ISector Clone() {
        return new SparseAsteroidSector();
    }

    public string prefabName()
    {
        return "sparseAsteroids";
    }

}