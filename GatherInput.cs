using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls myControl;


    public float valueX { get; private set; }
    public bool isJump { get; private set; }

    private void Awake()
    {
        myControl = new Controls();
    }

    private void OnEnable()
    {

        myControl.Player.Move.performed += StartMove;
        myControl.Player.Move.canceled += StopMove;

        myControl.Player.Jump.performed += JumpStart;
        myControl.Player.Jump.canceled += JumpStop;

        myControl.Player.Enable();
    }

    private void OnDisable()
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;
        myControl.Player.Jump.performed -= JumpStart;
        myControl.Player.Jump.canceled -= JumpStop;

        myControl.Player.Disable();
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        try
        {
            valueX = ctx.ReadValue<Vector2>().x;
        }
        catch
        {
            valueX = ctx.ReadValue<float>();
        }
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }

    private void JumpStart(InputAction.CallbackContext ctx)
    {
        isJump = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        isJump = false;
    }
}