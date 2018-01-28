
public class EmptySector : GenericSectorCalculator,ISector {

    public EmptySector()
    {
            SectorDamage = 0;
            SectorTemperature = 0;
            SectorConsume =0;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

        //MissionLog.Instance.AddLog("For now it seems to be all right in thi sector, dude");
        MissionLog.Instance.AddLog("Empty: Hp" + spaceship.HP + " - Temp" + spaceship.Temp + " - Fuel" + spaceship.Fuel);

    }



    public ISector Clone() {
        return new EmptySector();
    }

    public string prefabName() {
        return "empty";
    }


}
