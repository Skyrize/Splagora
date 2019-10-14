using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Inputs")]
    public string xAxis;
    public KeyCode downAction;
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

    private void checkInput()
    {
        direction = new Vector3(Input.GetAxis(xAxis), 0, 0);
        if (Input.GetKey(downAction) == true) {
            onDownAction.Invoke();
        }
        if (Input.GetKeyDown(jump) == true) {
            timeJumpPressed = Time.time;
        }
        if (Input.GetKeyUp(jump) == true) {
            if (Time.time - timeJumpPressed > timeForLongInput) {
                onLongJump.Invoke();
            } else {
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
