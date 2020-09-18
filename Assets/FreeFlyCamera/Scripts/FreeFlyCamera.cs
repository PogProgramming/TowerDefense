//===========================================================================//
//                       FreeFlyCamera (Version 6.9)                         //
//                       (cx ew) haha ;)                                     //
//===========================================================================//

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(Camera))]
public class FreeFlyCamera : MonoBehaviour
{
    #region UI

    [Space]

    [SerializeField]
    [Tooltip("The script is currently active")]
    private bool _active = true;

    [Space]

    [SerializeField]
    [Tooltip("Camera rotation by mouse movement is active")]
    private bool _enableRotation = true;

    [SerializeField]
    [Tooltip("Sensitivity of mouse rotation")]
    private float _mouseSense = 1.8f;

    [Space]

    [SerializeField]
    [Tooltip("Camera zooming in/out by 'Mouse Scroll Wheel' is active")]
    private bool _enableTranslation = true;

    [SerializeField]
    [Tooltip("Velocity of camera zooming in/out")]
    private float _translationSpeed = 55f;

    [Space]

    [SerializeField]
    [Tooltip("Camera movement by 'W','A','S','D','Q','E' keys is active")]
    private bool _enableMovement = true;

    [SerializeField]
    [Tooltip("Camera movement speed")]
    private float _movementSpeed = 10f;

    [SerializeField]
    [Tooltip("Speed of the quick camera movement when holding the 'Left Shift' key")]
    private float _boostedSpeed = 50f;

    //[SerializeField]
    //[Tooltip("Move up")]
    //private KeyCode _moveUp = KeyCode.E;

    //[SerializeField]
    //[Tooltip("Move down")]
    //private KeyCode _moveDown = KeyCode.Q;

    [Space]

    //[SerializeField]
    //[Tooltip("Acceleration at camera movement is active")]
    private bool _enableSpeedAcceleration = true;

    [SerializeField]
    [Tooltip("Rate which is applied during camera movement")]
    private float _speedAccelerationFactor = 1.5f;

    [Space]

    [SerializeField]
    [Tooltip("This keypress will move the camera to initialization position")]
    //private KeyCode _initPositonButton = KeyCode.R;

    #endregion UI

    private CursorLockMode _wantedMode;

    private float _currentIncrease = 1;
    private float _currentIncreaseMem = 0;

    private Vector3 _initPosition;
    private Vector3 _initRotation;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_boostedSpeed < _movementSpeed)
            _boostedSpeed = _movementSpeed;
    }
#endif

    public bool controllerConnected = false;

    private void Start()
    {
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;

        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    controllerConnected = true;
                }
            }
        }
    }

    private void OnEnable()
    {
        if (_active)
            _wantedMode = CursorLockMode.Locked;
    }

    // Apply requested cursor state
    private void SetCursorState()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = _wantedMode = CursorLockMode.None;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _wantedMode = CursorLockMode.Locked;
        }

        // Apply cursor state
        Cursor.lockState = _wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    }

    private void CalculateCurrentIncrease(bool moving)
    {
        _currentIncrease = Time.deltaTime;

        if (!_enableSpeedAcceleration || _enableSpeedAcceleration && !moving)
        {
            _currentIncreaseMem = 0;
            return;
        }

        _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
        _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
    }

    public float dragSpeed = 2;
    private Vector2 dragOrigin;

    public bool isInViewMode = true;

    private void Update()
    {
        if (!_active)
            return;

        SetCursorState();

        if (Application.isMobilePlatform && Cursor.visible)
            return;

        // Translation
        if (_enableTranslation)
        {
            transform.Translate(Vector3.forward * Mouse.current.scroll.ReadValue().y * Time.deltaTime * _translationSpeed);
        }

        // Movement
        if (_enableMovement)
        {
            Vector3 deltaPosition = Vector3.zero;
            float currentSpeed = _movementSpeed;

            if (controllerConnected)
            {
                transform.Translate(Vector3.forward * -Input.GetAxis("LJoystick Y") * 200f * Time.deltaTime);
                transform.Translate(Vector3.right * Input.GetAxis("LJoystick X") * 200f * Time.deltaTime);
            }
            else
            {
                if (Keyboard.current.leftShiftKey.isPressed)
                {
                    currentSpeed = _boostedSpeed;
                }

                if (Keyboard.current.wKey.isPressed)
                {
                    deltaPosition += transform.forward;
                }

                if (Keyboard.current.sKey.isPressed)
                {
                    deltaPosition -= transform.forward;
                }

                if (Keyboard.current.aKey.isPressed)
                {
                    deltaPosition -= transform.right;
                }

                if (Keyboard.current.dKey.isPressed)
                {
                    deltaPosition += transform.right;
                }

                //if (Input.GetKey(_moveUp))
                //    deltaPosition += transform.up;

                //if (Input.GetKey(_moveDown))
                //    deltaPosition -= transform.up;
            }

            // Calc acceleration
            CalculateCurrentIncrease(deltaPosition != Vector3.zero);

            transform.position += deltaPosition * currentSpeed * _currentIncrease;
        }

        //Rotation
        if (_enableRotation)
        {
            if (!Cursor.visible)
            {
                if (controllerConnected)
                {
                    transform.rotation *= Quaternion.AngleAxis(
                        -Input.GetAxis("RJoystick Y") * _mouseSense * 2,
                        Vector3.right
                    );

                    transform.rotation = Quaternion.Euler(
                        transform.eulerAngles.x,
                        transform.eulerAngles.y + Input.GetAxis("RJoystick X") * _mouseSense * 2,
                        transform.eulerAngles.z
                    );
                }
                else
                {
                    // Pitch
                    transform.rotation *= Quaternion.AngleAxis(
                        -Input.GetAxis("Mouse Y") * _mouseSense,
                        Vector3.right
                    );

                    // Paw
                    transform.rotation = Quaternion.Euler(
                        transform.eulerAngles.x,
                        transform.eulerAngles.y + Input.GetAxis("Mouse X") * _mouseSense,
                        transform.eulerAngles.z
                    );
                }
            }
            else
            if (!Application.isMobilePlatform && isInViewMode)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    dragOrigin = Mouse.current.position.ReadValue();
                    return;
                }

                if (!Mouse.current.leftButton.isPressed) return;

                Vector3 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue() - dragOrigin);
                Vector3 move = new Vector3(pos.x * 2f, 0, pos.y * 2f);

                transform.Translate(move, Space.World);
            }
        }

        //Return to init position
        //if ()
        //{
        //    transform.position = _initPosition;
        //    transform.eulerAngles = _initRotation;
        //}
    }
}
