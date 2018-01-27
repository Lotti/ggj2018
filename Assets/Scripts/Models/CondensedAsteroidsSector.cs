using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondensedAsteroidsSector : ISector {
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if ( spaceship.HasAction( ActionType.POWER_SHIELD, tick ) ) {
            spaceship.HP -= 40;
        } else {
            spaceship.HP -= 80;
        }
    }

    public ISector Clone() {
        return new CondensedAsteroidsSector();
    }

    public string prefabName()
    {
        return "condensedAsteroid";
    }
}
