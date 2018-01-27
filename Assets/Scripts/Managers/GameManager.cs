using System.Collections;
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
    public const float COUNT_DOWN = 180;
    public const int TOKENS = 8;

    List<ISector> _map = new List<ISector>();
    bool _isRunning = false;
    int _currentTick = 0;
    SpaceShip _spaceship;
    float _gameTimer = 0;
    float _launchTimer = 0;
    int _pinTokens = 0;
    bool _isGameOver = false;
    bool _isWin = false;
    bool _canLaunchTimer = true;

    public float GameTimer { get { return _gameTimer; }}
    public float LaunchTimer { get { return _launchTimer; }}
    public int GameTimerInt { get { return Mathf.FloorToInt(_gameTimer); } }
    public int LaunchTimerInt { get { return Mathf.FloorToInt( _launchTimer ); } }
    public bool IsWin { get { return _isWin; }}
    public bool IsGameOver { get { return _isGameOver; } }
    public SpaceShip SpaceShip { get { return _spaceship; }}
    public int PinTokens { get { return _pinTokens; }}

    public List<ISector> WorldMap { get { return _map; }}

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
        DontDestroyOnLoad( this.gameObject );
        StartGame();
    }

    void Start () {
        
	}
	
	void Update () {
        
	}

    void OnDestroy () {
        _instance = null;    
    }

    bool _isSpaceShipDied(){
        return _spaceship.HP == 0 || _spaceship.Fuel <= 0.1 || _spaceship.Temp >= 150;
    }

    WaitForEndOfFrame _waitFrame = new WaitForEndOfFrame();
    IEnumerator _CountDown(){
        while(_gameTimer > 0){
            _gameTimer -= Time.deltaTime;
            yield return _waitFrame;
        }
        GameOver();
    }

    IEnumerator _LaunchTime () {
        while ( _canLaunchTimer ) {
            _launchTimer += Time.deltaTime;
            yield return _waitFrame;
        }
    }

    public void StartGame(){
        _gameTimer = COUNT_DOWN;
        _launchTimer = 0;
        _isGameOver = false;
        _isWin = false;
        _isRunning = false;
        _currentTick = 0;
        _pinTokens = TOKENS;
        StopAllCoroutines();
        _InitMap();
        _spaceship = new SpaceShip();
        _spaceship.Init( new SpaceShipDataInit( INITIAL_HP, INITIAL_FUEL, INITIAL_TEMP, SPACE_SIZE ) );
        StartCoroutine( _CountDown() );
    }

    public void Launch(){
        if ( _isRunning ) return;
        _spaceship.Setup( new SpaceShipDataSetup( INITIAL_HP, INITIAL_FUEL, INITIAL_TEMP ) );
        _isRunning = true;
        _currentTick = 0;
        _launchTimer = 0;
        this.FillHistory(_spaceship.ActionMatrix);
        StopCoroutine( _LaunchTime() );
        StopCoroutine( _Run() );
        _canLaunchTimer = true;
        StartCoroutine( _LaunchTime() );
        StartCoroutine( _Run() );
    }

    public Dictionary<ActionType, BitArray> GetMatrixSetup(){
        return _spaceship.ActionMatrix;
    }

    public void FillHistory(Dictionary<ActionType,BitArray> actionMatrix)
    {
        HistoryManager.Instance.History.Add(actionMatrix);
    }

    public void SetupAction( ActionType type, int tick, bool action ){
        if ( action && _pinTokens > 0 ) {
            _spaceship.SetAction( type, tick, action );
            _pinTokens--;
        } else if(!action){
            _pinTokens++;
            _pinTokens = ( _pinTokens > TOKENS ) ? TOKENS : _pinTokens;
        }
    }

    readonly WaitForSeconds _waitSeconds = new WaitForSeconds( 1 );
    IEnumerator _Run(){
        while (_currentTick < _map.Count){
            _map[_currentTick].RunSector( _spaceship, _currentTick );

            Debug.Log( _map[_currentTick].ToString() + " XXX " + _spaceship.ToString() );
            if(_isSpaceShipDied()){
                _isRunning = false;
                LaunchFailed();
                yield break;
            }
            _currentTick++;
            yield return _waitSeconds;
        }



        _isRunning = false;
        LaunchSuccess();
    }

    public void LaunchFailed() {
        _pinTokens = TOKENS;
        _canLaunchTimer = false;
        //StopCoroutine( _LaunchTime() );
        Debug.LogWarning("LAUNCH FAILED - FATALITYYYY!!!!");
    }

    public void LaunchSuccess() {
        _isWin = true;
        _isGameOver = false;
        StopAllCoroutines();
        Debug.LogWarning( "WIIIIIIIINNNNNNNNN! DAJE CAZZO" );
    }

    public void GameOver(){
        _isGameOver = true;
        _isWin = false;
        StopAllCoroutines();
        Debug.LogWarning( "GAME OVER - MEGA FATALITYYYY!!!!" );
    }
}
