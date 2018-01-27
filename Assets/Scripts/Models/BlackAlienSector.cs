using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAlienSector : ISector {

    bool _isDestroyed = false;

    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if ( !_isDestroyed ) {
            if ( !spaceship.HasAction( ActionType.POWER_SHIELD, tick ) ) {
                spaceship.HP -= 50;
            }
            if ( spaceship.HasAction( ActionType.WEAPONS, tick ) ) {
                _isDestroyed = true;
            }
        }
    }

    public ISector Clone() {
        BlackAlienSector c = new BlackAlienSector();
        c._isDestroyed = false;
        return c;
    }

    public string prefabName()
    {
        return "blackAlien";
    }
}
