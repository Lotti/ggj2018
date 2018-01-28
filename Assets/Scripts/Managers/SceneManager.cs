using System;
using System.Collections;
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

    private WaitForEndOfFrame _wait = new WaitForEndOfFrame();

    private Scenes scene = Scenes.StartScene;

    public void ChangeScene(Scenes scene)
    {
        this.scene = scene;

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ScenesToString());
    }

    public IEnumerator ChangeScene(Scenes scene,IEnumerator onFade)
    {
        yield return _wait;

        Debug.Log("I am WAITING");

        yield return onFade;

        Debug.Log("I am OK ");

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ScenesToString());
    }

    private void Awake()
    {
        this.scene = Scenes.StartScene;
    }
}
