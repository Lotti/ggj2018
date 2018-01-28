using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Board : Singleton<Board> {

    public int rows = 7;
    public float rowMargin = 75f;
    public float colMargin = 85f;
    public CinemachineVirtualCamera vCamera;
    public GameObject planetEndGame;
    public GameObject planetStartGame;
    private Dictionary<int, List<GameObject>> cells = new Dictionary<int, List<GameObject>>();
    private List<GameObject> shipObjectives = new List<GameObject>();
    private string prefabPath = "Prefabs/PlayerScene/";
    private int shipCount = 0;

    private List<ISector> WorldMap;
    private List<Dictionary<ActionType, BitArray>> History = new List<Dictionary<ActionType, BitArray>>();
    private int historyCount {
        get {
            return this.History.Count;
        }
    }

	void Awake() {
        this.PrepareCells();

        WorldMap = this.fakeWorldMap();
        History = this.fakeHistory();
        if (GameManager.Instance != null) {
            WorldMap = GameManager.Instance.WorldMap;
            History = GameManager.Instance.History;
        }
        List<ISector> LocalMap = new List<ISector>(WorldMap.Count);
        for (int i = 0; i < WorldMap.Count; i++) {
            LocalMap.Add(WorldMap[i].Clone());
        }

        this.PopulateCells();
	}

	// Use this for initialization
	void Start () {
        shipCount = 0;
        pickShip();
	}

    // Update is called once per frame
	void Update () {
		
	}

    private List<Dictionary<ActionType, BitArray>> fakeHistory() {
        var _actionMatrix = new Dictionary<ActionType, BitArray>();
        _actionMatrix.Add(ActionType.CONSUME, new BitArray(GameManager.SPACE_SIZE, false));
        _actionMatrix.Add(ActionType.PROTECTION, new BitArray(GameManager.SPACE_SIZE, false));
        _actionMatrix.Add(ActionType.TEMPERATURE, new BitArray(GameManager.SPACE_SIZE, false));

        return new List<Dictionary<ActionType, BitArray>>() {_actionMatrix};
    }

    private List<ISector> fakeWorldMap() {
        return new List<ISector>(){
            new SparseAsteroidSector(),
            new WhiteAlienSector(),
            new BlackAlienSector(),
            new BlackHoleSector(),
            new CondensedAsteroidsSector(),
            new BaseStationSector(),
            new NebulosaSector(),
            new RedStarSector(),
            new BlackHoleSector(),
            new BlackHoleSector(),
            new BlackHoleSector(),
        };        
    }

    private void PrepareCells() {
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < GameManager.SPACE_SIZE; col++) {
                if (!cells.ContainsKey(col)) {
                    cells.Add(col, new List<GameObject>());
                }

                GameObject cell = new GameObject(row + "-" + col + " cell");
                cell.transform.SetParent(this.transform);
                cell.transform.localScale = Vector3.one;
                cell.transform.localPosition = new Vector3(col * colMargin, row * -rowMargin, 0f);

                cells[col].Add(cell);
            }
        }
    }

    private void PopulateCells() {
        int prevRandom = -1;
        int random;
        for (int i = 0; i < cells.Count; i++) {
            do {
                random = UnityEngine.Random.Range(0, cells[i].Count);
            } while (prevRandom != -1 && Mathf.Abs(random - prevRandom) <= 2);
            prevRandom = random;

            GameObject c = cells[i][random];
            ISector s = WorldMap[0];

            GameObject g = (GameObject) Instantiate(Resources.Load(prefabPath + s.prefabName()), Vector3.zero, Quaternion.identity, c.transform);
            g.transform.localPosition = Vector3.zero;
            shipObjectives.Add(g);
            WorldMap.RemoveAt(0);
        }
    }

    public GameObject getShipTarget(int i) {
        if (i >= shipObjectives.Count) {
            return planetEndGame;
        }
        else {
            return shipObjectives[i];
        }
    }

    public ShipNavigator spawnShip(int dieAt) {
        GameObject g = (GameObject)Instantiate(Resources.Load(prefabPath + "spaceShip"), Vector3.zero, Quaternion.identity);
        g.transform.position = planetStartGame.transform.position;
        vCamera.Follow = g.transform;
        ShipNavigator sn = g.GetComponent<ShipNavigator>();
        sn.SetDieAt(dieAt);
        return sn;
    }

    public void pickShip() {
        if (shipCount < History.Count) {
            int dieAt = GameManager.SimulateRun(WorldMap, History[shipCount]);
            var ship = spawnShip(dieAt).Move();
            shipCount++;
        }
    }
}
