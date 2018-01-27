using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    NewGame,
    Options,
    Credits,
    Quit
}

public class ButtonMainMenuItem : MonoBehaviour 
{


    public string textString;
    public ButtonType buttonType;

    private Button btn;
    private Text stringText;

    private void Awake()
    {
        if (this.btn == null)
            this.btn = this.GetComponent<Button>();

        if (this.stringText == null)
            this.stringText = this.transform.GetChild(0).GetComponent<Text>();

        this.Init();
    }

    public void Init()
    {
        this.stringText.text = textString;

        switch (buttonType)
        {
            case ButtonType.NewGame:
                this.btn.onClick.AddListener(delegate { this.NewGame(); });
              break;
            case ButtonType.Options:
                this.btn.onClick.AddListener(delegate { this.Options(); });
                break;
            case ButtonType.Quit:
                this.btn.onClick.AddListener(delegate { this.Quit(); });
                break;
        }

    }

    private void NewGame()
    {
        SceneManager.Instance.ChangeScene(Scenes.Main);
    }

    private void Options()
    {
        Debug.Log("Options");
    }

    private void Quit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        Debug.Log(" QUIT ");
#endif
    }
}
