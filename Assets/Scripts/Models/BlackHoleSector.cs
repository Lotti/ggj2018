using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSector : ISector {
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if(spaceship.HasAction(ActionType.GRAVITATIONAL_BOOST,tick)){
            spaceship.Fuel -= 100;
        }else{
            spaceship.Fuel -= 200;
        }
    }

    public ISector Clone() {
        return new BlackHoleSector();
    }

    public string prefabName()
    {
        return "blackHole";
    }

}
