using UnityEngine;
using System.Collections;

public class NodeFSM : MonoBehaviour
{
    public enum States
    { 
        Initialise,
        Play,
        Count
    }

    //Neighbouring Tiles
    public NodeFSM up;
    public NodeFSM down;
    public NodeFSM left;
    public NodeFSM right;

    public static GameManager gameManager;

    //Finite State Machine
    private INode[] stateList;
    private INode currentState;
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
        stateList = new INode[(int)States.Count];

        stateList[(int)States.Initialise] = new NodeInitialise(this);
        stateList[(int)States.Play] = new NodePlay(this);

        ChangeState(States.Initialise);
    }
    #endregion 
}
