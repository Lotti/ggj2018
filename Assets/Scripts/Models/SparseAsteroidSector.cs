
public class SparseAsteroidSector : ISector {
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
        if(spaceship.HasAction(ActionType.POWER_SHIELD, tick)){
            spaceship.HP -= 20;
        }else{
            spaceship.HP -= 60;
        }
    }

    public ISector Clone() {
        return new SparseAsteroidSector();
    }

    public string prefabName()
    {
        return "sparseAsteroids";
    }

}
