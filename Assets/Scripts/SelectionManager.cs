using UnityEngine;
using UnityEngine.EventSystems;
public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    Color startColor = Color.white;

    public static GameObject selection = null;
    private GameObject isOccupied = null;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && hit.transform != null)
            {
                if (hit.transform.CompareTag("node") && selection == null)
                {
                    //Only select this node if there is no existing turret - Use this for turret shop menu
                    if (hit.transform.GetComponent<Node>().turret == null)
                    {
                        selection = hit.transform.gameObject;
                        selection.GetComponent<Renderer>().material.color = Color.green;

                        CheckIfOccupied();
                    }
                    else //Select if there is an existing turret on the node  - Use this for upgrade/sell menu
                    {
                        isOccupied = hit.transform.gameObject;
                        hit.transform.GetComponent<Renderer>().material.color = Color.yellow;
                    }
                }
                else if (selection != null)
                {
                    selection.GetComponent<Renderer>().material.color = startColor;
                    selection = null;

                    CheckIfOccupied();
                }
                else CheckIfOccupied();
            }
        }
    }

    void CheckIfOccupied()
    {
        if (isOccupied)
        {
            isOccupied.GetComponent<Renderer>().material.color = startColor;
            isOccupied = null;
        }
    }
}
