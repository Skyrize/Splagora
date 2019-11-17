using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedSlider : MonoBehaviour
{

    public GameObject BackGroundSelected;
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            BackGroundSelected.SetActive(true);
        }
        else
        {
            BackGroundSelected.SetActive(false);
        }
    }
}
