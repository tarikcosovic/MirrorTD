using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance = null;

    [SerializeField]
    GameObject[] turretPrefabs = {null};
    [SerializeField]
    Vector3 spawnOffset = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More then one BuildManager in scene..");
            return;
        }
        instance = this;
    }

    public void BuildTurret(int index)
    {
        GameObject selectedPosition = SelectionManager.selection;
        if(selectedPosition == null)
        {
            Debug.Log("Please select a position..");
            return;
        }

        GameObject go = Instantiate(turretPrefabs[index], selectedPosition.transform.position + spawnOffset, selectedPosition.transform.rotation);
        SelectionManager.selection = null;

        selectedPosition.GetComponent<Node>().turret = go;
        selectedPosition.GetComponent<Renderer>().material.color = Color.white;
    }
}
