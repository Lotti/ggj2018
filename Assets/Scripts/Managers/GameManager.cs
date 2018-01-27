﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }
    public static bool IsInstanced { get { return _instance != null; } }

    public const int SPACE_SIZE = 10;
    public const int INITIAL_HP = 100;
    public const float INITIAL_FUEL = 1000;
    public const float INITIAL_TEMP = 20;

    List<ISector> _map = new List<ISector>();
    bool _isRunning = false;
    int _currentTick = 0;
    SpaceShip _spaceship;

    static ISector[] _mapSpawner = new ISector[]{
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new EmptySector(),
        new SparseAsteroidSector(),
        new SparseAsteroidSector(),
        new SparseAsteroidSector(),
        new CondensedAsteroidsSector(),
        new WhiteAlienSector(),
        new WhiteAlienSector(),
        new BlackHoleSector(),
        new BlackHoleSector(),
        new BlackAlienSector(),
        new BlackAlienSector(),
        new RedStarSector(),
        new NebulosaSector(),
        new BaseStationSector(),
        new BaseStationSector()
    };

    void _InitMap(){
        _map = new List<ISector>();
        for ( int i = 0; i < SPACE_SIZE;  i++){
            int index = Random.Range(0,_mapSpawner.Length);
            _map.Add( _mapSpawner[index] );
        }
    }

    void Awake () {
        _instance = this;
        _InitMap();
        _spaceship = new SpaceShip();
        _spaceship.Init( new SpaceShipDataInit( INITIAL_HP, INITIAL_FUEL, INITIAL_TEMP, SPACE_SIZE ) );
    }

    void Start () {
        
	}
	
	void Update () {
		
	}

    void OnDestroy () {
        _instance = null;    
    }

    public void Launch(){
        if ( _isRunning ) return;
        _spaceship.Setup( new SpaceShipDataSetup( INITIAL_HP, INITIAL_FUEL, INITIAL_TEMP ) );
        _isRunning = true;
        _currentTick = 0;
        StopAllCoroutines();
        StartCoroutine( _Run() );
    }

    public Dictionary<ActionType, BitArray> GetMatrixSetup(){
        return _spaceship.ActionMatrix;
    }

    public void SetupAction( ActionType type, int tick, bool action ){
        _spaceship.SetAction( type, tick, action );
    }

    readonly WaitForSeconds _waitSeconds = new WaitForSeconds( 1 );
    IEnumerator _Run(){
        while (_currentTick < _map.Count){
            _map[_currentTick].RunSector( _spaceship, _currentTick );
            Debug.Log(_map[_currentTick].ToString() + " XXX " + _spaceship.ToString() );
            _currentTick++;
            yield return _waitSeconds;
        }
        _isRunning = false;
    }
}