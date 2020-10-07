using UnityEngine;

public class ChangeUI : MonoBehaviour
{
    public GameObject ChangeFrom;
    public GameObject ChangeTo;

    public void ChangeScreen()
    {
        ChangeTo.SetActive(true);
        ChangeFrom.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("BButton"))
        {
            ChangeScreen();
        }    
    }
}
