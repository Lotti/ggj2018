using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class Board : Singleton<Board>
{
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

    private List<ISector> LocalMap;
    private List<ISector> WorldMap;
    private List<Dictionary<ActionType, BitArray>> History;

    private Dictionary<string, bool> alreadyPopulatedCells = new Dictionary<string, bool>();

    private Text _wintText;
    private CanvasGroup _canvas;

	void Awake() {
        WorldMap = this.fakeWorldMap();
        History = this.fakeHistory();
        if (GameManager.IsInstanced) {
            WorldMap = GameManager.Instance.WorldMap;
            History = GameManager.Instance.History;
        }

        // cloning real WorldMap in LocalMap
        LocalMap = new List<ISector>(WorldMap.Count);
        for (int i = 0; i < WorldMap.Count; i++) {
            LocalMap.Add(WorldMap[i].Clone());
        }

        this.PrepareCells();
        this.PopulateCells();
	}

	// Use this for initialization
	void Start () {
        shipCount = 0;
        launchShip();
	}

    // Update is called once per frame
	void Update () {
		
	}

    private List<Dictionary<ActionType, BitArray>> fakeHistory() {
        var _actionMatrix = new Dictionary<ActionType, BitArray>();
        _actionMatrix.Add(ActionType.CONSUME, new BitArray(GameManager.SPACE_SIZE, false));
        _actionMatrix.Add(ActionType.PROTECTION, new BitArray(GameManager.SPACE_SIZE, false));
        _actionMatrix.Add(ActionType.TEMPERATURE, new BitArray(GameManager.SPACE_SIZE, false));

        return new List<Dictionary<ActionType, BitArray>>() {
            _actionMatrix,
            _actionMatrix,
            _actionMatrix,
        };
    }

    private List<ISector> fakeWorldMap() {
        return new List<ISector>(){
            new EmptySector(),
            new BaseStationSector(),
            new WhiteAlienSector(),
            new BlackAlienSector(),
            new BlackHoleSector(),
            new NebulosaSector(),
            new RedStarSector(),
            new SparseAsteroidSector(),
            new CondensedAsteroidsSector(),
            new BaseStationSector(),
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
        // populate path cells
        int prevRandom = -1;
        int random;
        for (int i = 0; i < cells.Count; i++) {
            do {
                random = UnityEngine.Random.Range(0, cells[i].Count);
            } while (prevRandom != -1 && Mathf.Abs(random - prevRandom) <= 2);
            prevRandom = random;

            GameObject c = cells[i][random];
            ISector s = LocalMap[0];
            alreadyPopulatedCells.Add(i + "-" + random, true);

            GameObject g = (GameObject) Instantiate(Resources.Load(prefabPath + s.prefabName()), Vector3.zero, Quaternion.identity, c.transform);
            g.transform.localPosition = Vector3.zero;
            shipObjectives.Add(g);
            LocalMap.RemoveAt(0);
        }

        // populate other cells
        for (int i = 0; i < cells.Count; i++) {
            for (int y = 0; y < cells[i].Count; y++) {
                int rand = UnityEngine.Random.Range(0, 3);

                if (!alreadyPopulatedCells.ContainsKey(i+"-"+y) && rand == 0) {
                    GameObject c = cells[i][y];
                    ISector s = GameManager.MapSpawner()[UnityEngine.Random.Range(0, GameManager.MapSpawner().Length)];
                    GameObject g = (GameObject)Instantiate(Resources.Load(prefabPath + s.prefabName()), Vector3.zero, Quaternion.identity, c.transform);
                    g.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    public GameObject getShipTarget(int i) {
        if (i > shipObjectives.Count) {
            return null;
        } else if (i == shipObjectives.Count) {
            return planetEndGame;
        } else {
            return shipObjectives[i];
        }
    }

    public ShipNavigator spawnShip(int dieAt, bool win) {
        GameObject g = (GameObject)Instantiate(Resources.Load(prefabPath + "spaceShip"), Vector3.zero, Quaternion.identity);
        g.transform.position = planetStartGame.transform.position;
        vCamera.Follow = g.transform;
        ShipNavigator sn = g.GetComponent<ShipNavigator>();
        sn.SetDieAt(dieAt);
        return sn;
    }

    public void launchShip() {
        Debug.Log("shipCount: " + shipCount);
        Debug.Log("History.Count: " + History.Count);
        if (shipCount < History.Count) {
            int dieAt = GameManager.SimulateRun(LocalMap, History[shipCount]);
            var ship = spawnShip(dieAt, dieAt == -1).Move();
            shipCount++;
        } else {
            UIWinPanel.Instance.ShowGameOver();
        }
    }
}
