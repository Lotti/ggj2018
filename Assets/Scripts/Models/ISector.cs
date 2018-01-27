
public interface ISector {

    void RunSector ( ISpaceShip spaceship, int tick );

    ISector Clone();

    string prefabName();
}
