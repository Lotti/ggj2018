using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent( typeof( Button ) )]
public class UIButtonMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    RectTransform _t;
    Image _i;

    Color _origColor;

    void Awake (){
        _t = GetComponent<RectTransform>();
        _i = GetComponent<Image>();
        _origColor = _i.color;
    }

    
    public void OnPointerEnter ( PointerEventData eventData ) {
        _t.DOScale(Vector3.one*1.1f, 0.2f);
        _i.color = Color.white;
    }

    public void OnPointerExit ( PointerEventData eventData ) {
        _t.DOScale( Vector3.one, 0.2f );
        _i.color = _origColor;
    }

}
