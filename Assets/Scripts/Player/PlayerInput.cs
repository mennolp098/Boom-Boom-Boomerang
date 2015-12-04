using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    public delegate void IntDelegate(int value);
    public delegate void NormalDelegate();
    public event IntDelegate RightKeyPressed;
    public event NormalDelegate JumpKeyPressed;
    public event IntDelegate LeftKeyPressed;
    public event NormalDelegate MouseButtonPressed;
    public event NormalDelegate NoKeyPressed;

    private void Update()
    {
        Inputs();
    }

    /// <summary>
    /// Handling all inputs and sending events
    /// </summary>
    private void Inputs()
    {
        bool aKeyIsPressed = false;

        //The right and left arrow may be send more then once
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //send right event
            aKeyIsPressed = true;
            if (RightKeyPressed != null)
                RightKeyPressed(1); //Right is always positive
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //send left event
            aKeyIsPressed = true;
            if (LeftKeyPressed != null)
                LeftKeyPressed(-1); //left is always negative
        }

        //The up and mouse button only need to send once
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //send up event
            if (JumpKeyPressed != null)
                JumpKeyPressed();
        }

        if (Input.GetMouseButtonDown(0))
        {
            //send Mousebutton event
            aKeyIsPressed = true;
            if (MouseButtonPressed != null)
                MouseButtonPressed();
        }
        if (!aKeyIsPressed)
        {
            if (NoKeyPressed != null)
                NoKeyPressed();
        }
    }
}
