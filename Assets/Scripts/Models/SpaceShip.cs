using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : ISpaceShip {

    int _hp = 0;
    float _fuel = 0;
    float _temp = 0;

    Dictionary<ActionType, BitArray> _actionMatrix = new Dictionary<ActionType, BitArray>();

    public int HP { 
        get {
            return _hp; 
        } 
        set {
            _hp = value;
            if( _hp < 0 ){
                _hp = 0;
            }
        }
    }

    public float Fuel { 
        get {
            return _fuel;    
        }

        set {
            _fuel = value;
            if(_fuel < 0){
                _fuel = 0;
            }
        } 
    }

    public float Temp { 
        get {
            return _temp;    
        }

        set {
            _temp = value;
        } 
    }

    public Dictionary<ActionType, BitArray> ActionMatrix { get { return _actionMatrix; }}

    public bool HasAction ( ActionType type, int tick ) {
        return _actionMatrix[type][tick];
    }

    public override string ToString(){
        return string.Format( "HP => {0}; FUEL => {1}; TEMP => {2}", HP, Fuel, Temp );
    }

    public void Init(int spaceSize, int hp, float fuel, float temp) {
        
        _hp = hp;
        _fuel = fuel;
        _temp = temp;
    }

    public void Init ( ISpaceShipDataInit data ) {
        var ddata = ( SpaceShipDataInit )data;
        _hp = ddata.hp;
        _fuel = ddata.fuel;
        _temp = ddata.temp;
        _actionMatrix = new Dictionary<ActionType, BitArray>();
        _actionMatrix.Add( ActionType.LOG, new BitArray( ddata.spaceSize, true ) );
        _actionMatrix.Add( ActionType.GRAVITATIONAL_BOOST, new BitArray( ddata.spaceSize, false ) );
        _actionMatrix.Add( ActionType.POWER_SHIELD, new BitArray( ddata.spaceSize, false ) );
        _actionMatrix.Add( ActionType.TERMIC_SHIELD, new BitArray( ddata.spaceSize, false ) );
        _actionMatrix.Add( ActionType.WEAPONS, new BitArray( ddata.spaceSize, false ) );

    }

    public void SetAction ( ActionType type, int tick, bool active ) {
        _actionMatrix[type][tick] = active;
    }

    public void Setup ( ISpaceShipDataInit data ) {
        var ddata = (SpaceShipDataSetup)data;
        _hp = ddata.hp;
        _fuel = ddata.fuel;
        _temp = ddata.temp;
    }
}

public class SpaceShipDataInit: ISpaceShipDataInit {
    public int hp;
    public float fuel;
    public float temp;
    public int spaceSize;

    public SpaceShipDataInit(int h, float f, float t, int ss){
        hp = h;
        fuel = f;
        temp = t;
        spaceSize = ss;
    }
}

public class SpaceShipDataSetup : ISpaceShipDataInit {
    public int hp;
    public float fuel;
    public float temp;

    public SpaceShipDataSetup ( int h, float f, float t ) {
        hp = h;
        fuel = f;
        temp = t;
    }
}
