using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : Singleton<HistoryManager>
{
    public List<Dictionary<ActionType, BitArray>> History = 
           new List<Dictionary<ActionType, BitArray>>();

    public Dictionary<ActionType, BitArray> getActionMatrix
    {
        get
        {
            return gManager.GetMatrixSetup();
        }
    }
    
    private GameManager gManager
    {
        get
        {
            return GameManager.Instance;
        }
    }



}
