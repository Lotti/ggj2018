using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetup : MonoBehaviour {

    [SerializeField]
    Transform _vConatiner;

    [SerializeField]
    GameObject _rowPrefab;
    [SerializeField]
    GameObject _itemPrefab;

    void Start () {
        Make();    
    }

    public void Make(){
        var matrix = GameManager.Instance.GetMatrixSetup();
        foreach( var m in matrix){
            var row = Instantiate(_rowPrefab, _vConatiner);
            var item = Instantiate( _itemPrefab, row.transform );
            item.GetComponent<UIItem>().Set( m.Key, 0, false, true );
            for ( int c = 0; c < m.Value.Count; c++){
                item = Instantiate( _itemPrefab, row.transform );
                item.GetComponent<UIItem>().Set( m.Key, c, m.Value[c] );
            }
        }
    }
}
