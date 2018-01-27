
public interface ISpaceShip {

    bool HasAction ( ActionType type ,int tick );
    void Init ( ISpaceShipDataInit data );
    void Setup ( ISpaceShipDataInit data );
    void SetAction ( ActionType type, int tick, bool active );
    string ToString ();
    void CalculateMod();

    int HP { get; set; }
    float Fuel { get; set; }
    float Temp { get; set; }

    float[] ModHP { get; set; }
    float[] ModFUEL { get; set;  }
    float[] ModTEMP { get; set;  }
}

public class ISpaceShipDataInit {}

public enum ActionType {
    NONE,
    PROTECTION,
    TEMPERATURE,
    CONSUME,
}
