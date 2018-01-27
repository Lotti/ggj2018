using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : Singleton<Board> {

    public int rows = 3;

	void Awake() {
        for (int y = 0; y < rows; y++) {
            GameObject row = new GameObject(y + " row");
            row.transform.parent = this.transform;
            row.transform.localScale = Vector3.zero;
            HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            for(int i = 0; i < GameManager.SPACE_SIZE; i++) {
                GameObject cell = new GameObject(y + "-" + i + " cell");
                cell.transform.parent = row.transform;
                cell.transform.localScale = Vector3.zero;
			}
		}

        List<ISector> WorldMap = GameManager.Instance.WorldMap;
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
