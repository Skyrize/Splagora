using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteText : MonoBehaviour
{
    public Text target,myTxt;
    public void Start()
    {
        myTxt = GetComponent<Text>();
    }
    public void Update()
    {
        myTxt.text = target.text;
    }
}
