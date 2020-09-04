using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    public GameObject highlighter;
    public List<GameObject> troopList;
    public bool isPlacing = false;

    Vector3 point = Vector3.zero; // position on the grid
    public int selectedTroopIndex;

    void Start()
    {
        
    }

    public Vector3 GetCurrentMouseGridPosition()
    {
        return point;
    }

    void Update()
    {
        if (!isPlacing)
        {
            if (highlighter.activeSelf) highlighter.SetActive(false);
            return;
        }

        Plane plane = new Plane(Vector3.up, -1.1f);

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray, out dist))
        {
            point = ray.GetPoint(dist);
            point = new Vector3(Mathf.Round(point.x), 1, Mathf.Round(point.z));
        }

        if(point != Vector3.zero)
        {
            if(!highlighter.activeSelf) highlighter.SetActive(true);
            highlighter.transform.position = point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = Instantiate(troopList[selectedTroopIndex]);
            obj.transform.position = new Vector3(point.x, 1.5f, point.z);
        }
    }
}
