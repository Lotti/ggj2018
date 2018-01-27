using System;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum Scenes
{
    Main,
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

    private void Awake()
    {
        this.scene = Scenes.StartScene;
    }
}
