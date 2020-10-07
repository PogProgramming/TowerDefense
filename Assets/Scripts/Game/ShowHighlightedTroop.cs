using UnityEngine;

public class ShowHighlightedTroop : MonoBehaviour
{
    PlacementScript ps;

    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlacementScript>();
    }

    public Material tranMat;
    private GameObject displayTroop;
    bool instantiated = false;
    void Update()
    {
        if (instantiated == false)
        {
            displayTroop = Instantiate(ps.troopList[ps.selectedTroopIndex]);
            displayTroop.GetComponent<TroopScript>().SetIsDisplayTroop(true);
            instantiated = true;

            MeshRenderer troopMesh;
            if (displayTroop.GetComponent<MeshRenderer>() == null)
                troopMesh = displayTroop.GetComponentInChildren<MeshRenderer>();
            else
                troopMesh = displayTroop.GetComponent<MeshRenderer>();

            troopMesh.material = tranMat;
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
