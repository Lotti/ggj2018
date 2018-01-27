using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : Singleton<Board> {

    public int rows = 3;
    private Dictionary<int, List<GameObject>> cells = new Dictionary<int, List<GameObject>>();
    private List<GameObject> shipObjectives = new List<GameObject>();

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<ISector> fakeWorldMap() {
        return new List<ISector>(){
            new SparseAsteroidSector(),
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
        for (int y = 0; y < rows; y++) {
            GameObject row = new GameObject(y + " row");
            row.transform.SetParent(this.transform);
            row.transform.localScale = Vector3.one;
            HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childAlignment = TextAnchor.MiddleCenter;

            for (int i = 0; i < GameManager.SPACE_SIZE; i++)
            {
                if (!cells.ContainsKey(i)) {
                    cells.Add(i, new List<GameObject>());
                }

                GameObject cell = new GameObject(y + "-" + i + " cell");
                cell.AddComponent<RectTransform>();
                cell.transform.SetParent(row.transform);
                cell.transform.localScale = Vector3.one;

                cells[i].Add(cell);
            }
        }
    }

    private void PopulateCells(List<ISector> WorldMap) {
        for (int i = 0; i < cells.Count; i++) {
            GameObject c = cells[i][UnityEngine.Random.Range(0, cells[i].Count)];
            ISector s = WorldMap[0];
            GameObject g = (GameObject) Instantiate(Resources.Load("Prefabs/PlayerScene/" + s.prefabName()), Vector3.zero, Quaternion.identity, c.transform);
            shipObjectives.Add(g);
            WorldMap.RemoveAt(0);
        }
    }

    public GameObject CalculateShipMovements(int i) {
        return shipObjectives[i];
    }
}
