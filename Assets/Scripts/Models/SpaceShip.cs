using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SpaceShip : ISpaceShip {

    int _hp = 0;
    float _fuel = 0;
    float _temp = 0;
    int _peoples = 0;

    float[] _modHP;
    float[] _modFUEL;
    float[] _modTEMP;

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

    public float[] ModHP
    {
        set
        {
            _modHP = value;
        }

        get
        {
            return _modHP;
        }
    }

    public float[] ModTEMP
    {
        set
        {
            _modTEMP = value;
        }


        get
        {
            return _modTEMP;
        }
    }


    public float[] ModFUEL
    {
        set
        {
            _modFUEL = value;
        }

        get
        {
            return _modFUEL;
        }
    }

    public void CalculateMod()
    {
        ModHP = new float[GameManager.SPACE_SIZE];
        ModFUEL = new float[GameManager.SPACE_SIZE];
        ModTEMP = new float[GameManager.SPACE_SIZE];

        BitArray protectionArray = new BitArray(GameManager.SPACE_SIZE);
        ActionMatrix.TryGetValue(ActionType.PROTECTION, out protectionArray);

        BitArray tempArray = new BitArray(GameManager.SPACE_SIZE);
        ActionMatrix.TryGetValue(ActionType.TEMPERATURE, out tempArray);

        BitArray fuelArray = new BitArray(GameManager.SPACE_SIZE);
        ActionMatrix.TryGetValue(ActionType.CONSUME, out fuelArray);

        for (int i = 0; i < GameManager.SPACE_SIZE; i++)
        {
            /*//PROTECTION
            if(protectionArray[i]==true)
            {
                ModHP[i] = 2;
                ModFUEL[i] = 1;
                ModTEMP[i] = 2;
            }
            else
            {
                ModHP[i] = 0;
                ModFUEL[i] = 0;
                ModTEMP[i] = 0;
            }
        
            //TEMPERATURE
            if (tempArray[i] == true)
            {
                ModHP[i] += -2;
                ModFUEL[i] += -1;
                ModTEMP[i] += +1;
            }
            else
            {
                ModHP[i] += 0;
                ModFUEL[i] += 0;
                ModTEMP[i] += 0;
            }
        
            //FUEL
            if (fuelArray[i] == true)
            {
                ModHP[i] += -1;
                ModFUEL[i] += +2;
                ModTEMP[i] += -2;
            }
            else
            {
                ModHP[i] += 0;
                ModFUEL[i] += 0;
                ModTEMP[i] += 0;
            }
            */
            var mod = this.GetModForTick(i);
            ModHP[i] = mod[(int)ActionType.PROTECTION];
            ModFUEL[i] = mod[(int)ActionType.CONSUME];
            ModTEMP[i] = mod[(int)ActionType.TEMPERATURE];

        }

       // Debug.Log("ModHP => " + string.Join(",", ModHP.Select(x => x.ToString()).ToArray()));
      //  Debug.Log("ModFUEL => " + string.Join(",", ModFUEL.Select(x => x.ToString()).ToArray()));
      //  Debug.Log("ModTEMP => " + string.Join(",", ModTEMP.Select(x => x.ToString()).ToArray()));


    }

    public int[] GetModForTick(int i)
    {
        BitArray protectionArray = new BitArray(GameManager.SPACE_SIZE);
        ActionMatrix.TryGetValue(ActionType.PROTECTION, out protectionArray);

        BitArray tempArray = new BitArray(GameManager.SPACE_SIZE);
        ActionMatrix.TryGetValue(ActionType.TEMPERATURE, out tempArray);

        BitArray fuelArray = new BitArray(GameManager.SPACE_SIZE);
        ActionMatrix.TryGetValue(ActionType.CONSUME, out fuelArray);

        int[] mod = new int[4];

        //PROTECTION
        if (protectionArray[i] == true)
        {
            mod[(int) ActionType.PROTECTION] = 2;
            mod[(int)ActionType.CONSUME] = 1;
            mod[(int)ActionType.TEMPERATURE] = 2;
        }
        else
        {
            mod[(int)ActionType.PROTECTION] = 0;
            mod[(int)ActionType.CONSUME] = 0;
            mod[(int)ActionType.TEMPERATURE] = 0;
        }

        //TEMPERATURE
        if (tempArray[i] == true)
        {
            mod[(int)ActionType.PROTECTION] += -2;
            mod[(int)ActionType.CONSUME] += 1;
            mod[(int)ActionType.TEMPERATURE] += -1;
        }
        else
        {
            mod[(int)ActionType.PROTECTION] += 0;
            mod[(int)ActionType.CONSUME] += 0;
            mod[(int)ActionType.TEMPERATURE] += 0;
        }

        //FUEL
        if (fuelArray[i] == true)
        {
            mod[(int)ActionType.PROTECTION] += -1;
            mod[(int)ActionType.CONSUME] += +2;
            mod[(int)ActionType.TEMPERATURE] += -2;
        }
        else
        {
            mod[(int)ActionType.PROTECTION] += 0;
            mod[(int)ActionType.CONSUME] += 0;
            mod[(int)ActionType.TEMPERATURE] += 0;
        }
        return mod;
    }

    public bool HasAction ( ActionType type, int tick ) {
        return _actionMatrix[type][tick];
    }

    public string ToString(int actTick){
        return string.Format( "HP => {0}; FUEL => {1}; TEMP => {2}; MODHP => {3}; MODTEMP => {4}; MODFUEL => {5}; ", HP, Fuel, Temp,ModHP[actTick],ModTEMP[actTick],ModFUEL[actTick]);
    }

    public void Init(int spaceSize, int hp, float fuel, float temp) 
    {
        _hp = hp;
        _fuel = fuel;
        _temp = temp;

        _modHP = new float[spaceSize];
        _modFUEL = new float[spaceSize];
        _modTEMP = new float[spaceSize];
    }

    public void Init ( ISpaceShipDataInit data ) 
    {
        var ddata = ( SpaceShipDataInit )data;
        _hp = ddata.hp;
        _fuel = ddata.fuel;
        _temp = ddata.temp;

        _actionMatrix = new Dictionary<ActionType, BitArray>();
        _actionMatrix.Add( ActionType.CONSUME, new BitArray( ddata.spaceSize, false ) );
        _actionMatrix.Add( ActionType.PROTECTION, new BitArray( ddata.spaceSize, false ) );
        _actionMatrix.Add( ActionType.TEMPERATURE, new BitArray( ddata.spaceSize, false ) );


        _modHP = new float[GameManager.SPACE_SIZE];
        _modFUEL = new float[GameManager.SPACE_SIZE];
        _modTEMP = new float[GameManager.SPACE_SIZE];

    }

    public int GetPeopleBonus()
    {
        return _peoples / 10;
    }

    public void SetAction ( ActionType type, int tick, bool active ) {
        _actionMatrix[type][tick] = active;
    }

    public void Setup ( ISpaceShipDataInit data ) {
        var ddata = (SpaceShipDataSetup)data;
        _hp = ddata.hp;
        _fuel = ddata.fuel;
        _temp = ddata.temp;
        _peoples = ddata.peoples;

        _modHP = new float[GameManager.SPACE_SIZE];
        _modFUEL = new float[GameManager.SPACE_SIZE];
        _modTEMP = new float[GameManager.SPACE_SIZE];
    }

    public string prefabName()
    {
        return "spaceShip";
    }

    public void SetActionMatrix(Dictionary<ActionType, BitArray> matrix){
        _actionMatrix = matrix;
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
    public int peoples;

    public SpaceShipDataSetup ( int h, float f, float t, int p ) {
        hp = h;
        fuel = f;
        temp = t;
        peoples = p;
    }
}
