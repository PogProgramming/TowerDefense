using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCheck : MonoBehaviour
{
    public FreeFlyCamera playerMovement;

    public GameObject mobileControls;
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            GetComponent<Canvas>().scaleFactor = 4;
            mobileControls.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
