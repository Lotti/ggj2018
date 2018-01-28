using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : Singleton<Board> {

    public int rows = 3;
    public float rowMargin = -60f;
    public float colMargin = 60f;
    private Dictionary<int, List<GameObject>> cells = new Dictionary<int, List<GameObject>>();
    private List<GameObject> shipObjectives = new List<GameObject>();
    private string prefabPath = "Prefabs/PlayerScene/";

	void Awake() {
        this.PrepareCells();

        List<ISector> WorldMap = this.fakeWorldMap();
        if (GameManager.Instance != null) {
            WorldMap = GameManager.Instance.WorldMap;    
        }
        List<ISector> LocalMap = new List<ISector>(WorldMap.Count);
        for (int i = 0; i < WorldMap.Count; i++) {
            LocalMap.Add(WorldMap[i].Clone());
        }

        this.PopulateCells(WorldMap);
	}

	// Use this for initialization
	void Start () {
        Transform parentTransform = this.transform;
        GameObject g = (GameObject)Instantiate(Resources.Load(prefabPath + "spaceShip"), Vector3.zero, Quaternion.identity, parentTransform);
        g.transform.position = cells[0][0].transform.position;
        g.transform.localScale = Vector3.one * 0.8f;
        g.GetComponent<ShipNavigator>().Move();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<ISector> fakeWorldMap() {
        return new List<ISector>(){
            new SparseAsteroidSector(),
            new WhiteAlienSector(),
            new BlackAlienSector(),
            new BlackHoleSector(),
            new SparseAsteroidSector(),
            new BlackAlienSector(),
            new SparseAsteroidSector(),
            new BlackAlienSector(),
            new BlackHoleSector(),
            new BlackHoleSector(),
            new BlackHoleSector(),
        };        
    }

    private void PrepareCells()
    {
        
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < GameManager.SPACE_SIZE; col++) {
                if (!cells.ContainsKey(col)) {
                    cells.Add(col, new List<GameObject>());
                }

                GameObject cell = new GameObject(row + "-" + col + " cell");
                cell.transform.SetParent(this.transform);
                cell.transform.localScale = Vector3.one;
                cell.transform.localPosition = new Vector3(col * colMargin, row * rowMargin, 0f);

                cells[col].Add(cell);
            }
        }
    }

    private void PopulateCells(List<ISector> WorldMap) {
        for (int i = 0; i < cells.Count; i++) {
            GameObject c = cells[i][UnityEngine.Random.Range(0, cells[i].Count)];
            ISector s = WorldMap[0];
            GameObject g = (GameObject) Instantiate(Resources.Load(prefabPath + s.prefabName()), Vector3.zero, Quaternion.identity, c.transform);
            g.transform.localPosition = Vector3.zero;
            g.transform.localScale = Vector3.one * 0.8f;
            shipObjectives.Add(g);
            WorldMap.RemoveAt(0);
        }
    }

    public GameObject getShipTarget(int i) {
        if (i >= shipObjectives.Count) {
            return null;
        }
        else {
            return shipObjectives[i];
        }
    }
}
