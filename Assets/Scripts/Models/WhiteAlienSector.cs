using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteAlienSector : ISector {

    bool _isDestroyed = false;

    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if ( !_isDestroyed ) {
            if ( spaceship.HasAction( ActionType.WEAPONS, tick ) ) {
                _isDestroyed = true;
            } else {
                spaceship.Fuel += 100;
            }
        }
    }

}
