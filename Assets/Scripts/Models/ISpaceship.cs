
public interface ISpaceShip {

    bool HasAction ( ActionType type ,int tick );
    void Init ( ISpaceShipDataInit data );
    void Setup ( ISpaceShipDataInit data );
    void SetAction ( ActionType type, int tick, bool active );
    string ToString ();

    int HP { get; set; }
    float Fuel { get; set; }
    float Temp { get; set; }
	
}

public class ISpaceShipDataInit {}

public enum ActionType {
    NONE,
    POWER_SHIELD,
    GRAVITATIONAL_BOOST,
    LOG,
    WEAPONS,
    TERMIC_SHIELD
}
