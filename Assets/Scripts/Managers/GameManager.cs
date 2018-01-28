using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }
    public static bool IsInstanced { get { return _instance != null; } }

    public const int SPACE_SIZE = 10;
    public const int INITIAL_HP = 5;
    public const float INITIAL_FUEL = 12;
    public const float INITIAL_TEMP = 5;
    public const float COUNT_DOWN = 180;
    public const int PEOPLES = 100;

    List<ISector> _map = new List<ISector>();
    bool _isRunning = false;
    int _currentTick = 0;
    SpaceShip _spaceship;
    float _gameTimer = 0;
    float _launchTimer = 0;
    bool _isGameOver = false;
    bool _isWin = false;
    bool _canLaunchTimer = true;
    int _peoples = PEOPLES;

    public float tempBonus = 0;

    public float GameTimer { get { return _gameTimer; }}
    public float LaunchTimer { get { return _launchTimer; }}
    public int GameTimerInt { get { return Mathf.FloorToInt(_gameTimer); } }
    public int LaunchTimerInt { get { return Mathf.FloorToInt( _launchTimer ); } }
    public bool IsWin { get { return _isWin; }}
    public bool IsGameOver { get { return _isGameOver; } }
    public SpaceShip SpaceShip { get { return _spaceship; }}
    public int Peoples { get { return _peoples; }}

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
        return _spaceship.HP == 0 || _spaceship.Fuel <= 0.1 || _spaceship.Temp >= 10 || _spaceship.Temp <= 0;
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

        tempBonus = 0;
        _gameTimer = COUNT_DOWN;
        _launchTimer = 0;
        _isGameOver = false;
        _isWin = false;
        _isRunning = false;
        _currentTick = 0;
        _peoples = PEOPLES;
        StopAllCoroutines();
        _InitMap();
        _spaceship = new SpaceShip();
        _spaceship.Init( new SpaceShipDataInit( INITIAL_HP, INITIAL_FUEL, INITIAL_TEMP, SPACE_SIZE ) );

    }

    public void Launch(int peoples){
        
        if ( _isRunning ) return;

        if (peoples < 10 || (peoples%10) != 0) return;

        if(_peoples >= peoples){
            _peoples -= peoples;
        }else{
            peoples = _peoples;
            _peoples = 0;
        }

        _spaceship.Setup( new SpaceShipDataSetup( INITIAL_HP, INITIAL_FUEL, INITIAL_TEMP, peoples ) );
        _isRunning = true;
        _currentTick = 0;
        _launchTimer = 0;
        this.FillHistory(_spaceship.ActionMatrix);
        StopCoroutine( _LaunchTime() );
        StopCoroutine( _Run() );
        _canLaunchTimer = true;
        StartCoroutine( _LaunchTime() );

        //Calcolo i modificatori
        _spaceship.CalculateMod();

        StartCoroutine( _Run() );
    }

    public Dictionary<ActionType, BitArray> GetMatrixSetup(){
        return _spaceship.ActionMatrix;
    }

    public void FillHistory(Dictionary<ActionType,BitArray> actionMatrix)
    {
        HistoryManager.Instance.History.Add(actionMatrix);
    }

    public bool SetupAction( ActionType type, int tick, bool action ){
        _spaceship.SetAction(type, tick, action);
        return true;
    }

    readonly WaitForSeconds _waitSeconds = new WaitForSeconds( 1 );
    IEnumerator _Run(){
        tempBonus = 0;

        while (_currentTick < _map.Count)
        {
            _map[_currentTick].RunSector( _spaceship, _currentTick );

            if(_spaceship.ActionMatrix[ActionType.PROTECTION][_currentTick]==true)
            {
                tempBonus++;
            }
            else
            {
                tempBonus--;
                if (tempBonus < 0)
                    tempBonus = 0f;
            }

            Debug.Log(" TEMP BONUS " + tempBonus);

            Debug.Log( _map[_currentTick].ToString() + " XXX " + _spaceship.ToString(_currentTick));
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
        _canLaunchTimer = false;
        Debug.LogWarning("LAUNCH FAILED - FATALITYYYY!!!!");
        if (_peoples <= 0)
        {
            GameOver();
        }
        else
        {
            MissionLog.Instance.TransmitLog();
        }

    }

    public void LaunchSuccess() {
        _isWin = true;
        _isGameOver = false;
        StopAllCoroutines();
        MissionLog.Instance.TransmitLog();
        Debug.LogWarning( "WIIIIIIIINNNNNNNNN! DAJE CAZZO" );
        SceneManager.Instance.ChangeScene(Scenes.Player);
    }

    public void GameOver(){
        _isGameOver = true;
        _isWin = false;
        StopAllCoroutines();
        MissionLog.Instance.TransmitLog();
        Debug.LogWarning( "GAME OVER - MEGA FATALITYYYY!!!!" );
        SceneManager.Instance.ChangeScene(Scenes.Player);
    }

}
