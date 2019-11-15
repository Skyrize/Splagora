using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject menuPrincipal, intro;

    public void ActiveMenu()
    {
        intro.SetActive(false);
        menuPrincipal.SetActive(true);
    }
}
