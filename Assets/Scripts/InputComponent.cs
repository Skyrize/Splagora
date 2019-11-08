using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XboxCtrlrInput;

public class InputComponent : MonoBehaviour
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
    public float timeForLongInput = 0.05f;

    private bool blocked = false;

    private float timeJumpPressed = 0;

    //For XBOX plug_in
    public XboxController controller;
    public CharacterController charaController;
    bool startJump;

    public Animator anim;

    public void Block()
    {
        blocked = true;
    }
    
    public void Release()
    {
        blocked = false;
    }

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        charaController = GetComponent<CharacterController>();
    }
    private void checkInput()
    {
        if (blocked) {
            direction = Vector3.zero;
        } else {

            direction = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, controller), 0, 0);
            if (XCI.GetAxis(XboxAxis.LeftStickY, controller) <= -0.7)
                onDownAction.Invoke();

            if (XCI.GetButtonDown(XboxButton.A, controller))
            {
                startJump = true;
                timeJumpPressed = 0;
            }

            if (XCI.GetButtonDown(XboxButton.A, controller))
            {
                int randomFigure = Random.Range(0, 3);
                anim.SetFloat("RandomFigure", randomFigure);

                float velocity = charaController.velocity.magnitude;
                if (velocity < 3f)
                {
                    onQuickJump.Invoke();


                }
                else
                {
                    onLongJump.Invoke();
                }

            }
        }


        // if (blocked) {
        //     direction = Vector3.zero;
        // } else {
        //     direction = new Vector3(Input.GetAxis(xAxis), 0, 0);
        //     if (Input.GetKey(downAction))
        //     {
        //         onDownAction.Invoke();
        //     }
        //     if (Input.GetKey(jump))
        //     {
        //         float velocity = charaController.velocity.magnitude;
        //         if (velocity < 3f)
        //         {
        //             onQuickJump.Invoke();
        //         }
        //         else
        //         {
        //             onLongJump.Invoke();
        //         }
                
        //     }
        // }

    }
    // Update is called once per frame
    void Update()
    {
        checkInput();
    }
}
