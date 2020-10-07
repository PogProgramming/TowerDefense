
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerInput : MonoBehaviour
{
    public UpgradeInterface ui;
    private EventSystem eventSystem = null;

    FreeFlyCamera cameraScript;
    public GameObject crosshair;

    public Button firstSelect;
    public Button secondSelect;
    void Start()
    {
        cameraScript = Camera.main.GetComponent<FreeFlyCamera>();

        if (cameraScript.controllerConnected)
        {
            crosshair.SetActive(true);
            eventSystem = EventSystem.current.GetComponent<EventSystem>();
        }
    }

    float timer = 0;
    bool cooldown = false;

    bool selecting = false;

    void Update()
    {
        if (!cameraScript.controllerConnected) return;

        if (Input.GetButtonDown("YButton"))
        {
            eventSystem.SetSelectedGameObject(firstSelect.gameObject);
            selecting = true;
        }
        if (Input.GetButtonDown("BButton"))
        {
            if(selecting != false)
            {
                eventSystem.SetSelectedGameObject(null);
                selecting = false;
            }

            if (ui.upgradePanel.activeSelf) ui.HideUpgradeAndOpenPurchase();
        }

        if (timer > 0.5f)
        {
            cooldown = false;
            timer = 0f;
        }

        if (cooldown) timer += Time.deltaTime;
        else
        {
            if (Input.GetAxis("DPad X") > 0.5f)
            {
                SelectButton(secondSelect.gameObject);
            }
            else if (Input.GetAxis("DPad X") < -0.5f)
            {
                SelectButton(firstSelect.gameObject);
            }
        }
    }

    void SelectButton(GameObject buttonObj)
    {
        eventSystem.SetSelectedGameObject(buttonObj);
    }
}
