using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    ActionType _type;
    int _tick;
    bool _isActive;
    bool _isHeader;

    [SerializeField]
    Image _img;

    [SerializeField]
    Text _txt;

    public void Set ( ActionType type, int tick, bool active, bool isHeader = false ) {
        _type = type;
        _tick = tick;
        _isActive = active;
        _isHeader = isHeader;
        _Refresh();
    }

    public void Switch () {
        if ( _isHeader ) return;
        _isActive = !_isActive;
        _Refresh();
        GameManager.Instance.SetupAction( _type, _tick, _isActive );
    }

    private void _Refresh () 
    {
        if(_isHeader){
            _txt.gameObject.SetActive( true );
            _txt.text = _type.ToString();
            return;
        }
        _txt.gameObject.SetActive( false );
        if ( _isActive ) {
            _img.color = Color.white;
        } else {
            _img.color = Color.gray;
        }
    }

}
