﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementScript : MonoBehaviour
{
    public Stats statBlock;

    public LayerMask TroopLayerMask;

    public GameObject highlighter;
    public GameObject gridHighlighter;
    public GameObject viewDistanceHighlighter;
    public BadSpotDistanceHighlighter viewDistanceHighlighterScript;

    public List<GameObject> troopList;
    public bool isPlacing = false;
    public UpgradeInterface upgradeUI;

    Vector3 point = Vector3.zero; // position on the grid
    public int selectedTroopIndex = -1;

    bool placed = true;

    public Vector3 GetPoint() { return new Vector3(point.x, troopList[selectedTroopIndex].GetComponent<TroopScript>().yPosSpawn, point.z); }
    public void Placed()
    {
        placed = true;
        highlighter.SetActive(false);
        gridHighlighter.SetActive(false);

        selectedTroopIndex = -1;

    }
    public void ResetPlaced() { placed = false; }

    public Vector3 GetCurrentMouseGridPosition()
    {
        return point;
    }

    void Start()
    {
        viewDistanceHighlighterScript = viewDistanceHighlighter.GetComponent<BadSpotDistanceHighlighter>();
    }

    void Update()
    {
        Plane plane = new Plane(Vector3.up, -1.1f);

        float dist;
        Ray ray;

        if (Camera.main.GetComponent<FreeFlyCamera>().controllerConnected)
        {
            Vector2 midScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            ray = Camera.main.ScreenPointToRay(midScreen);
        }
        else
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

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

            if (Mouse.current.leftButton.wasPressedThisFrame || Input.GetButtonDown("AButton"))
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, TroopLayerMask))
                {
                    if (!Camera.main.GetComponent<FreeFlyCamera>().isInViewMode || !Application.isMobilePlatform)
                    {
                        upgradeUI.SetTroop(hit.collider.gameObject);
                        upgradeUI.HidePurchaseButtons();
                        upgradeUI.OpenUpgradeOptions();
                    }
                }
            }


            return;
        }

        if (point != Vector3.zero)
        {
            highlighter.transform.position = point;
            viewDistanceHighlighter.transform.position = new Vector3(point.x, 1.1f, point.z);
        }

        if (!placed)
        {
            if (selectedTroopIndex != -1 && !highlighter.activeSelf && !viewDistanceHighlighter.activeSelf)
            {
                highlighter.SetActive(true);
                viewDistanceHighlighter.SetActive(true);
                gridHighlighter.SetActive(true);

                float distance = troopList[selectedTroopIndex].GetComponent<TroopScript>().GetViewRadius() * 2;
                viewDistanceHighlighter.transform.localScale = new Vector3(distance, 0.001f, distance);
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (viewDistanceHighlighterScript.safe && statBlock.GetPlacedTroops() < statBlock.GetMaxTroops() && !Camera.main.GetComponent<FreeFlyCamera>().controllerConnected)
                {
                    GameObject obj = Instantiate(troopList[selectedTroopIndex]);
                    TroopScript ts = obj.GetComponent<TroopScript>();

                    statBlock.AdjustCash(-ts.GetCost());
                    statBlock.IncrementPlacedTroops(1);

                    obj.transform.position = new Vector3(point.x, ts.yPosSpawn, point.z);

                    Placed();
                }
                else
                {
                    Placed();
                }
            }
        }
    }
}
