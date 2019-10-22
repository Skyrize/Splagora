using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SpeedPowerup
{
    [SerializeField]
    public string name;
    [SerializeField]
    public float duration;

    public UnityEvent startAction;
    public UnityEvent endAction;

    public void End()
    {
        if (endAction != null)
            endAction.Invoke();
    }

    public void Start()
    {
        if (startAction != null)
            startAction.Invoke();
    }


}
