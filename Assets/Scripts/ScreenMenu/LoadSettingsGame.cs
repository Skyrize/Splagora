using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettingsGame : MonoBehaviour
{
    public SettingsUI dataUI;

    IEnumerable test()
    {

        yield return new WaitForSeconds(2);
    }

    void Start()
    {
        Debug.Log("load");
        dataUI.LoadGameSettings();
    }   
}
