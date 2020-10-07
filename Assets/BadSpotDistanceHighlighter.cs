using UnityEngine;

public class BadSpotDistanceHighlighter : MonoBehaviour
{
    public bool safe = true;

    public UpgradeInterface ui;

    public Material badMaterial;
    public Material goodMaterial;

    public LayerMask groundLayer;
    public LayerMask pathLayer;
    public LayerMask troopLayer;

    void Start()
    {
    }

    void Update()
    {
        safe = true;
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.5f, pathLayer);
        if (cols.Length > 0)
            safe = false;

        cols = Physics.OverlapSphere(transform.position, 0.25f, troopLayer);
        if (cols.Length > 1)
            safe = false;

        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += 5;
        RaycastHit hit;
        if (!Physics.Raycast(rayCastOrigin, Vector3.down, out hit, 10f, groundLayer))
        {
            safe = false;
        }

        if (safe)
        {
            if (gameObject.GetComponent<MeshRenderer>().material != goodMaterial)
                gameObject.GetComponent<MeshRenderer>().material = goodMaterial;
        }
        else
        {
            if (gameObject.GetComponent<MeshRenderer>().material != badMaterial)
                gameObject.GetComponent<MeshRenderer>().material = badMaterial;
        }
    }
}
