using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GGJExtension 
{

    public static string ScenesToString(this Scenes scene)
    {
        switch (scene)
        {
            case Scenes.StartScene:
                return "StartScene";
            case Scenes.Main:
                return "Main";
            case Scenes.Main_1:
                return "Main_1";
            default:
                return "NoScene"; 
        }   
    }
}
