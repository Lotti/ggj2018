using System;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum Scenes
{
    Main,
    Player,
    StartScene
}

public class SceneManager : Singleton<SceneManager>
{
    public Scenes CurrentScene
    {
        get
        {
            return scene;
        }
    }

    private Scenes scene = Scenes.StartScene;

    public void ChangeScene(Scenes scene)
    {
        this.scene = scene;

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ScenesToString());
    }

    public void ChangeScene(Scenes scene,Action<YieldInstruction> onFade)
    {
        
    }

    private void Awake()
    {
        this.scene = Scenes.StartScene;
    }
}
