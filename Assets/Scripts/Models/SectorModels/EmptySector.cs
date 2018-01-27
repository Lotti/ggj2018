
public class EmptySector : GenericSectorCalculator,ISector {

    public EmptySector()
    {
            SectorDamage = 1;
            SectorTemperature = 0;
            SectorConsume = 1;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        spaceship.HP += (int)CalcModDMG(spaceship.ModHP[tick]);

        spaceship.Temp += CalcModTEMP(spaceship.ModTEMP[tick]);

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

    }



    public ISector Clone() {
        return new EmptySector();
    }

    public string prefabName() {
        return "empty";
    }


}
