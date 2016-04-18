using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public GameObject playerGameObj;
    public GameObject startingTile;

    public bool finishedSetup = false;
    private GameObject player;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    public void FinishedSetup()
    {
        finishedSetup = true;
        InitialiseGame();
    }

    public void InitialiseGame()
    {
        Vector3 spawnPoint = startingTile.transform.position;
        spawnPoint.z = 0;
        player = (GameObject)Instantiate(playerGameObj, spawnPoint, transform.rotation);
        player.GetComponent<PlayerController>().SetNodeFSM(startingTile.GetComponent<NodeFSM>());
    }
}
