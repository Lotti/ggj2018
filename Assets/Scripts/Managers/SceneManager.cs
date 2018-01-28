using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum Scenes
{
    Main,
    Main_1,
    Player,
    Credits,
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

    public event Action OnOpenScene;

    private WaitForEndOfFrame _wait = new WaitForEndOfFrame();

    private Scenes scene = Scenes.StartScene;

    public void ChangeScene(Scenes scene)
    {
        this.scene = scene;

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ScenesToString());

        this.OnOpenScene();


    }

    public IEnumerator ChangeScene(Scenes scene,IEnumerator onFade)
    {
        yield return _wait;

        yield return onFade;

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ScenesToString());
    }

    private void Awake()
    {
        this.scene = Scenes.StartScene;
    }

}
