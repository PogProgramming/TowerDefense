using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 1f;
    public float smoothing = 2.0f;
    private GameObject character;
    private GameObject character_eyes;

    public bool thirdperson = true;

    private Vector3 CameraPosition;
    private Vector3 fpTargetPosition;
    private Vector3 tpNormalCameraPositon;

    public bool playing = true;
    public bool orbiting = false;

    void Start()
    {
        GameObject Target = transform.parent.gameObject;
        Vector3 fpTargetPosition = Target.transform.localPosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        character = Target.transform.parent.gameObject;
        character_eyes = character.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        CameraPosition = transform.localPosition;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playing = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playing = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (!thirdperson && playing && !orbiting)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector2.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f && thirdperson) ZoomIn();

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) ZoomOut();

        if (Input.GetMouseButton(1))
        {
            orbiting = true;
            sensitivity *= 2;
        }

        else if (Input.GetMouseButtonUp(1))
        {
            orbiting = false;
            sensitivity /= 2;
        }

    }

    private bool Zooming;
    void ZoomOut()
    {
        if (!thirdperson)
        {
            Vector3 moveToOriginal = transform.localPosition;
            moveToOriginal.z -= 2f;
            transform.localPosition = moveToOriginal;
        }
        Zooming = true;
        var temp_z = CameraPosition.z - 0.2f;

        while (Zooming)
        {
            if (temp_z != transform.localPosition.z)
            {
                CameraPosition.z = Mathf.Clamp(CameraPosition.z, -5f, 0f);
                CameraPosition.z -= 2;
                transform.Translate(CameraPosition * Time.deltaTime * 10f);
            }
            else
            {
                Zooming = false;
                break;
            }
            Zooming = false;
            break;

        }
    }
    void ZoomIn()
    {
        Zooming = true;
        var temp_z = CameraPosition.z + 0.2f;
        while (Zooming)
        {
            if (temp_z != transform.localPosition.z)
            {
                CameraPosition.z = Mathf.Clamp(CameraPosition.z, -5f, 0f);
                CameraPosition.z += 2;
                transform.Translate(CameraPosition * -Time.deltaTime * 10f);
            }
            else
            {
                Zooming = false;
                break;
            }
            Zooming = false;
            break;
        }
    }
    void LateUpdate()
    {
        fpTargetPosition = Target.transform.localPosition;
        if (CameraPosition.z > -1.5f && thirdperson)
        {

            CameraFPSPositionSet(true);

        }
        if (CameraPosition.z < -1.5f)
        {
            CameraFPSPositionSet(false);
            CamControl();
        }
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        if (thirdperson && !orbiting)
        {
            transform.LookAt(Target);

            if (Input.GetKey(KeyCode.LeftControl))
            {
                Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            }
            else
            {
                if (playing)
                {
                    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
                    Player.rotation = Quaternion.Euler(0, mouseX, 0);
                }

            }
        }

        if (orbiting)
        {
            transform.LookAt(Target);

            if (Input.GetKey(KeyCode.LeftControl))
            {
                Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            }
            else
            {
                if (playing)
                {
                    Debug.Log("HELP");
                    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
                }
            }
        }
    }

    public void CameraFPSPositionSet(bool firstperson)
    {
        //enters into thirdperson
        if (firstperson)
        {
            thirdperson = false;
            character_eyes.SetActive(false);
            tpNormalCameraPositon.x = transform.localPosition.x;
            tpNormalCameraPositon.y = Mathf.Lerp(transform.localPosition.y, 0, 5f);
            tpNormalCameraPositon.z = Mathf.Lerp(transform.localPosition.z, 0, 5f);
            transform.localPosition = tpNormalCameraPositon;

            fpTargetPosition.x = Mathf.Lerp(Target.localPosition.x, 0, 5f);
            fpTargetPosition.y = Mathf.Lerp(transform.localPosition.y, 0.8f, 5f);
            fpTargetPosition.z = Mathf.Lerp(transform.localPosition.z, 0, 5f);
            Target.localPosition = fpTargetPosition;
        }
        //enters into firstperson
        if (!firstperson)
        {
            thirdperson = true;
            character_eyes.SetActive(true);
            fpTargetPosition.x = 0.49f;
            fpTargetPosition.y = 0.97f;
            fpTargetPosition.z = 0;
            Target.localPosition = fpTargetPosition;

        }



    }
}