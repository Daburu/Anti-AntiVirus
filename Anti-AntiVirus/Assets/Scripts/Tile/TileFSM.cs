using UnityEngine;
using System.Collections;

public class TileFSM : MonoBehaviour
{
    public enum States
    { 
        Initialise,
        Play,
        Count
    }

    //Neighbouring Tiles
    public TileFSM up;
    public TileFSM down;
    public TileFSM left;
    public TileFSM right;

    public static GameManager gameManager;

    //Finite State Machine
    private ITile[] stateList;
    private ITile currentState;
    private States StateEnum;

    #region MonoBehaviour
    void Awake()
    {
        if (gameManager == null)
        {
            Transform parent = transform.parent.parent;
            gameManager = parent.GetComponent<GameManager>();
        }

        InitialiseFSM();
    }
    
    void Start () 
    {
	}
	
	void Update () 
    {
        currentState.Execute();
    }
    #endregion

    #region Finite State Machine
    public void ChangeState(States state)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = stateList[(int)state];

        if (currentState != null)
            currentState.Enter();
    }

    public void InitialiseFSM()
    {
        stateList = new ITile[(int)States.Count];

        stateList[(int)States.Initialise] = new TileInitialise(this);
        stateList[(int)States.Play] = new TilePlay(this);

        ChangeState(States.Initialise);
    }
    #endregion 
}
