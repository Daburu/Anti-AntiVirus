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
        player = (GameObject)Instantiate(playerGameObj, startingTile.transform.position, transform.rotation);
        player.GetComponent<PlayerController>().SetTileFSM(startingTile.GetComponent<TileFSM>());
    }
}
