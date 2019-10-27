using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XboxCtrlrInput;

public class InputComponent : MonoBehaviour
{
    [Header("Inputs")]
    public string xAxis;
    //public KeyCode downAction;
    public KeyCode jump;

    [Space]
    [Header("Action Event")]
    public UnityEvent onDownAction = new UnityEvent();
    public UnityEvent onQuickJump = new UnityEvent();
    public UnityEvent onLongJump = new UnityEvent();

    [Space]
    [Header("Values")]
    public Vector3 direction = Vector3.zero;
    public float timeForLongInput = 1;

    private float timeJumpPressed = 0;

    //For XBOX plug_in
    public XboxController controller;

    private void checkInput()
    {

        direction = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, controller), 0, 0);
        if (XCI.GetAxis(XboxAxis.LeftStickY, controller) <= -0.7)
        {
            onDownAction.Invoke();
        }

        //if (Input.GetKeyDown(jump) == true) 
        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            timeJumpPressed = Time.time;
        }
        //if (Input.GetKeyUp(jump) == true) 
        if (XCI.GetButtonUp(XboxButton.A, controller))
        {
            if (Time.time - timeJumpPressed > timeForLongInput)
            {
                onLongJump.Invoke();
            }
            else
            {
                onQuickJump.Invoke();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkInput();
    }
}
