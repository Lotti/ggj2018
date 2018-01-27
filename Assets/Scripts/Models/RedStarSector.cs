using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStarSector : ISector {
    
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if(spaceship.HasAction(ActionType.TERMIC_SHIELD, tick)){
            
        }else{
            spaceship.Temp = 100;
        }
    }

}
