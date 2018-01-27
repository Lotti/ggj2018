using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITokens : MonoBehaviour {

    Text _text;

    void Awake () {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if ( GameManager.IsInstanced ) {
            _text.text = GameManager.Instance.PinTokens.ToString();
        }
	}
}
