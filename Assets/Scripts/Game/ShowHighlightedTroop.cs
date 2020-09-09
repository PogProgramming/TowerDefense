using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighlightedTroop : MonoBehaviour
{
    PlacementScript ps;
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlacementScript>();
    }

    private GameObject displayTroop;
    bool instantiated = false;
    void Update()
    {
        if (instantiated == false)
        {
            displayTroop = Instantiate(ps.troopList[ps.selectedTroopIndex]);
            displayTroop.GetComponent<TroopScript>().SetIsDisplayTroop(true);
            instantiated = true;

            Material troopMaterial = displayTroop.GetComponent<MeshRenderer>().material;
            displayTroop.GetComponent<MeshRenderer>().material = GetAdjustedMaterial(troopMaterial);
        }
        else
        {
            displayTroop.transform.position = ps.GetPoint();
        }
    }

    private void OnDisable()
    {
        if (displayTroop != null) Object.Destroy(displayTroop);
        instantiated = false;
    }

    Material GetAdjustedMaterial(Material current)
    {
        Material mat = current;
        mat.SetFloat("_Mode", 3);
        mat.SetColor("_Color", new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f));
        mat.renderQueue = 3000;
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        return mat;
    }
}
