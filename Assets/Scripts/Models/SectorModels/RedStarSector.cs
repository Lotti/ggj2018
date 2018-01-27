using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RedStarSector : GenericSectorCalculator,ISector {
    
    public RedStarSector()
    {
        SectorDamage = 1;
        SectorTemperature = 2;
        SectorConsume = 0;
    }

    public void RunSector(ISpaceShip spaceship, int tick)
    {
        var appmp = (int)CalcModDMG(spaceship.ModHP[tick]);
        spaceship.HP += appmp;

        var appt = CalcModTEMP(spaceship.ModTEMP[tick]);
        spaceship.Temp += appt;

        spaceship.Fuel += CalcModFUEL(spaceship.ModFUEL[tick]);

        spaceship.Fuel--;

        StringBuilder builder = new StringBuilder();
        builder.Append("Mmmmm...");

        if(appmp < 0){
            builder.Append("we have a damage on the hull...");
        }
        if(appt < 0){
            builder.Append("and the temperature is very high");
        }

        if(appt>=0 && appmp > 0){
            builder.Append("this sector seems to be empty...");
        }

    }

    public ISector Clone() {
        return new RedStarSector();
    }

    public string prefabName()
    {
        return "redStar";
    }

}
