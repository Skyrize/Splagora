using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    public Button selectFirst;

    public void OnEnable()
    {
        StartCoroutine(SelectButtonLater());
    }
    IEnumerator SelectButtonLater()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectFirst.gameObject);
    }
}
