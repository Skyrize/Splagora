using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettingsMenu : MonoBehaviour
{
    public SettingsUI dataUI;

    void Start()
    {
        Debug.Log("load");
        dataUI.LoadMenuSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
