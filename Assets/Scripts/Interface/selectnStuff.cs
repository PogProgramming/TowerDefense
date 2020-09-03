using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectnStuff : MonoBehaviour
{
    Button btn;
    GameObject eventSystem;
    void Start()
    {
        btn = transform.GetComponent<Button>();
        eventSystem = GameObject.Find("EventSystem");
    }

    bool highlighted = false;
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            highlighted = !highlighted;
            timer = 0;
        }

        if (highlighted)
        {
            btn.Select();
        }
        else
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
