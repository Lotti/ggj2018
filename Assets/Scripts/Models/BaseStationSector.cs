using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStationSector : ISector {
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        spaceship.Fuel += 200;
        spaceship.HP += 20;
    }

    public ISector Clone() {
        return new BaseStationSector();
    }
}
