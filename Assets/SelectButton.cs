using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Button selectFirst;

    public void OnEnable()
    {
        selectFirst.Select();
    }
}
