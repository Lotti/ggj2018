
public class EmptySector : ISector {
    
    public void RunSector ( ISpaceShip spaceship, int tick ) {
        spaceship.Fuel -= 100;
    }

}
