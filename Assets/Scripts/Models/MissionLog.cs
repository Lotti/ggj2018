using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLog : Singleton<MissionLog>
{
    public event System.Action<string> OnTransmission;

    private List<string> _TrasmissionLog;
    private string _formattedLog;

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
        var str = (_TrasmissionLog.Count) + " " + logToAdd;
        _TrasmissionLog.Add(str);
        MonitorScript.Instance.AppendText(str);
    }

    public void TransmitLog()
    {

        TrasmissionLog[TrasmissionLog.Count - 1] = "<color=#e30e0e>" + TrasmissionLog[TrasmissionLog.Count - 1] + "</color>";

        if (OnTransmission != null){
            OnTransmission(_FormatLog());
            this._TrasmissionLog.Clear();
        }
    }

    string _FormatLog() {
        _formattedLog = String.Join("\n\n", _TrasmissionLog.ToArray());
        return _formattedLog;
    }

}
