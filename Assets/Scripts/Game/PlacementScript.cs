using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementScript : MonoBehaviour
{
    public Stats statBlock;

    public LayerMask TroopLayerMask;

    public GameObject highlighter;
    public GameObject viewDistanceHighlighter;

    public List<GameObject> troopList;
    public bool isPlacing = false;
    public UpgradeInterface upgradeUI;

    Vector3 point = Vector3.zero; // position on the grid
    public int selectedTroopIndex = -1;

    bool placed = true;

    public Vector3 GetPoint() { return new Vector3(point.x, 1.5f, point.z); }
    public void Placed()
    {
        placed = true;
        highlighter.SetActive(false);
        selectedTroopIndex = -1;
    }
    public void ResetPlaced() { placed = false; }

    public Vector3 GetCurrentMouseGridPosition()
    {
        return point;
    }

    void Update()
    {
        Plane plane = new Plane(Vector3.up, -1.1f);

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (plane.Raycast(ray, out dist))
        {
            point = ray.GetPoint(dist);
            point = new Vector3(Mathf.Round(point.x), 1, Mathf.Round(point.z));
        }

        if (selectedTroopIndex == -1)
        {
            if (highlighter.activeSelf)
            {
                highlighter.SetActive(false);
            }

            if (viewDistanceHighlighter.activeSelf)
            {
                viewDistanceHighlighter.SetActive(false);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, TroopLayerMask))
                {
                    upgradeUI.SetTroop(hit.collider.gameObject);
                    upgradeUI.HidePurchaseButtons();
                    upgradeUI.OpenUpgradeOptions();
                }
            }

            return;
        }

        if (point != Vector3.zero)
        {
            highlighter.transform.position = point;
            viewDistanceHighlighter.transform.position = point;
        }

        if (!placed)
        {
            if (selectedTroopIndex != -1 && !highlighter.activeSelf && !viewDistanceHighlighter.activeSelf)
            {
                if (!highlighter.activeSelf)
                {
                    highlighter.SetActive(true);
                    viewDistanceHighlighter.SetActive(true);

                    float distance = troopList[selectedTroopIndex].GetComponent<TroopScript>().GetViewRadius();
                    viewDistanceHighlighter.transform.localScale = new Vector3(distance, 0.001f, distance);
                }
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                GameObject obj = Instantiate(troopList[selectedTroopIndex]);
                obj.transform.position = new Vector3(point.x, 1.5f, point.z);

                statBlock.AdjustCash(-obj.GetComponent<TroopScript>().GetCost());

                Placed();
            }
        }
    }
}
