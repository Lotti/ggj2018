using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulosaSector : ISector {
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if(spaceship.HasAction(ActionType.POWER_SHIELD, tick)){
            spaceship.HP -= 20;
        }else{
            spaceship.HP -= 40;
        }
        if(!spaceship.HasAction(ActionType.GRAVITATIONAL_BOOST,tick)){
            spaceship.Fuel -= 50;
        }
    }
}
