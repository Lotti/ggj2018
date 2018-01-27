using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStarSector : ISector {
    
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if(spaceship.HasAction(ActionType.TERMIC_SHIELD, tick)){
            spaceship.Temp = 80;
        }else{
            spaceship.Temp = 150;
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
