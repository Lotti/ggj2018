using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : Singleton<Board> {

    public int rows = 3;

	void Awake() {
        for (int y = 0; y < rows; y++) {
            GameObject row = new GameObject(y + " row");
            row.transform.SetParent(this.transform);
            row.transform.localScale = Vector3.one;
            HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            for(int i = 0; i < GameManager.SPACE_SIZE; i++) {
                GameObject cell = new GameObject(y + "-" + i + " cell");
                cell.AddComponent<RectTransform>();
                cell.transform.SetParent(row.transform);
                cell.transform.localScale = Vector3.one;
			}
		}

        List<ISector> WorldMap = new List<ISector>(){
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
        if (GameManager.Instance != null) {
            WorldMap = GameManager.Instance.WorldMap;    
        }
        List<ISector> LocalMap = new List<ISector>(WorldMap.Count);
        for (int i = 0; i < WorldMap.Count; i++) {
            LocalMap.Add(WorldMap[i].Clone());
        }


	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
