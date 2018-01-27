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

    public int historyCount
    {
        get
        {
            return this.History.Count;
        }
    }
    
    private GameManager gManager
    {
        get
        {
            return GameManager.Instance;
        }
    }

    /// <summary>
    /// Return all values of the launch 
    /// </summary>
    /// <returns>The bools.</returns>
    /// <param name="numberLaunch">Number lunch.</param>
    public List<bool> GetBools(int numberLaunch)
    {
        var listBool = new List<bool>();

        foreach (var log in this.History[numberLaunch - 1])
        {
            listBool.Add(log.Value.Get(numberLaunch));
        }

        return listBool;
    }



}
