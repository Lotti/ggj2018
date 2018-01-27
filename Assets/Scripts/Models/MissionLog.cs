using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLog : Singleton<MissionLog>
{
    private List<string> _TrasmissionLog;
    private List<string> TrasmissionLog
    {
        get
        {
            return _TrasmissionLog;
        } 
    }

    void Awake()
    {
        _TrasmissionLog = new List<string>();
    }

    public void AddLog(string logToAdd)
    {
        _TrasmissionLog.Add(logToAdd);
    }

}
