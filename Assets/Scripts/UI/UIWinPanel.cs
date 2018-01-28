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
        if ( !GameManager.Instance.IsWin ){
            ShowGameOver();
            return;
        }
        _txt.text = "YOU WIN!!";
        _txt.color = Color.green;
        _cg.DOFade( 1f, 0.5f );
	}

    public void ShowGameOver(){
        if ( GameManager.Instance.IsWin ) {
            ShowWin();
            return;
        }
        _txt.text = "GAME OVER!!";
        _txt.color = Color.red;
        _cg.DOFade( 1f, 0.5f );
    }
	
	public void Hide () {
        _cg.DOFade( 1f, 0f );
	}

    public void Restart(){
        if (_cg.alpha == 1) {
            AudioManager.Instance.PlayMainTheme();
            SceneManager.Instance.ChangeScene(Scenes.StartScene);
        }
    }

}
