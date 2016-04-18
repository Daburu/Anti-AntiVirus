using UnityEngine;
using System.Collections;

public class NodeInitialise : INode
{
    private static int totalTiles = 0;
    private static int finishedCount = 0;

    public NodeInitialise(NodeFSM otherFSM)
    {
        fsm = otherFSM;
    }

    public override void Enter()
    {
        if(totalTiles == 0)
        {
            totalTiles = fsm.transform.parent.childCount;
        }

        //Finds neighbouring tiles in all four directions.
        RaycastHit2D hit;

        hit = Physics2D.Raycast(fsm.transform.position, Vector2.up, 1f);
        if (hit.collider != null)
            fsm.up = hit.collider.GetComponent<NodeFSM>();

        hit = Physics2D.Raycast(fsm.transform.position, Vector2.right, 1f);
        if (hit.collider != null)
            fsm.right = hit.collider.GetComponent<NodeFSM>();

        hit = Physics2D.Raycast(fsm.transform.position, Vector2.down, 1f);
        if (hit.collider != null)
            fsm.down = hit.collider.GetComponent<NodeFSM>();

        hit = Physics2D.Raycast(fsm.transform.position, Vector2.left, 1f);
        if (hit.collider != null)
            fsm.left = hit.collider.GetComponent<NodeFSM>();

        finishedCount++;
        if (finishedCount == totalTiles)
        {
            NodeFSM.gameManager.FinishedSetup();
        }
    }

    public override void Execute()
    {
        if (NodeFSM.gameManager.finishedSetup)
        {
            fsm.ChangeState(NodeFSM.States.Play);
        }
    }

    public override void Exit()
    {

    }

}
