using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    //public GameObject[] CPList;

    public List<GameObject> CPList = new List<GameObject>();
    public GameObject currentCheckpoint;
    private Transform checkpointLocation;
    private GameObject player;
    private GameObject startingPoint;
    private CharacterController charController;

    public Material activeMaterial;
    public Material inactiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charController = player.GetComponent<CharacterController>();
        startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
        currentCheckpoint = startingPoint;

        CPList.AddRange(GameObject.FindGameObjectsWithTag("CheckPoint"));

        player.transform.position = startingPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RespawnPlayer()
    {
        charController.enabled = false;
        checkpointLocation = currentCheckpoint.transform;
        player.transform.position = checkpointLocation.position;
        Invoke("ReactivateController", 0.5f);
    }

    public void UpdateCheckPoints()
    {
        Debug.Log("UpdateCheckPoints was called");
        foreach (GameObject l in CPList)
        {
            if (l.gameObject.GetComponent<CheckPoint>().isCheckpoint == false)
            {
                l.gameObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
            }
            else
            {
                l.gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
            }
        }
    }

    private void ReactivateController()
    {
        charController.enabled = true;
    }
}
