using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIWinPanel : Singleton<UIWinPanel> {

    [SerializeField]
    Text _txt;

    [SerializeField]
    CanvasGroup _cg;

	public void ShowWin() {
        _txt.text = "YOU WIN!!";
        _txt.color = Color.green;
        _cg.DOFade( 1f, 0.5f );
	}

    public void ShowGameOver(){
        _txt.text = "GAME OVER!!";
        _txt.color = Color.red;
        _cg.DOFade( 1f, 0.5f );
    }
	
	public void Hide () {
        _cg.DOFade( 1f, 0f );
	}

    public void Restart(){
        SceneManager.Instance.ChangeScene( Scenes.StartScene );
    }

}
