using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonsButtons : MonoBehaviour
{
    PlacementScript ps;

    void Start()
    {
        ps = Camera.main.GetComponent<PlacementScript>();
    }

    void Update()
    {

    }

    public void btn_Exit()
    {
        Application.Quit(0);
    }

    public void btn_Play()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
    }

    public void btn_Click()
    {
        if (ps.viewDistanceHighlighterScript.safe && ps.statBlock.GetPlacedTroops() < ps.statBlock.GetMaxTroops())
        { 
            GameObject obj = Instantiate(ps.troopList[ps.selectedTroopIndex]);
            TroopScript ts = obj.GetComponent<TroopScript>();

            ps.statBlock.AdjustCash(-ts.GetCost());
            ps.statBlock.IncrementPlacedTroops(1);

            obj.transform.position = new Vector3(ps.GetPoint().x, ts.yPosSpawn, ps.GetPoint().z);

            ps.Placed();

            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
    }
}
