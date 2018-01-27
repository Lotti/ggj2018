using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStationSector : ISector {
    public void RunSector ( ISpaceShip spaceship, int tick ) {

    }

    public ISector Clone() {
        return new BaseStationSector();
    }

    public string prefabName() {
        return "baseStation";
    }
}
