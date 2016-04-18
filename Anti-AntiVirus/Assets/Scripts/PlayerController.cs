using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    private NodeFSM NodeFSM;
    private PlayerInput.Direction direction = PlayerInput.Direction.Null;

    public float minChange = 5f;
    public float distanceCoefficient = 1f;

    private float positionDelta;
    private float distBetween;
    private Vector3 destination;
    private bool isMoving;

    #region Monobehaviour
    void Start ()
    {
        isMoving = false;
	}
	
	void Update ()
    {
        if(direction != PlayerInput.Direction.Null)
        {
            isMoving = true;

            distBetween = Vector3.Magnitude(destination - transform.position);
            positionDelta = (minChange + distanceCoefficient * distBetween) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, positionDelta);

            if (transform.position == destination)
            {
                UpdateNodeFSM();
            }
        }
        else
        {
            isMoving = false;
        }
	}
    #endregion

    public void SetDirection(PlayerInput.Direction newDirection)
    {
        direction = newDirection;

        switch (direction)
        {
            case PlayerInput.Direction.Up:
                if (NodeFSM.up)
                {
                    destination = NodeFSM.up.transform.position;
                }
                break;
            case PlayerInput.Direction.Down:
                if (NodeFSM.down)
                {
                    destination = NodeFSM.down.transform.position;
                }
                break;
            case PlayerInput.Direction.Left:
                if (NodeFSM.left)
                {
                    destination = NodeFSM.left.transform.position;
                }
                break;
            case PlayerInput.Direction.Right:
                if (NodeFSM.right)
                {
                    destination = NodeFSM.right.transform.position;
                }
                break;
        }
    }

    public void SetNodeFSM(NodeFSM newTileFSM)
    {
        NodeFSM = newTileFSM;
    }

    private void UpdateNodeFSM()
    {
        switch (direction)
        {
            case PlayerInput.Direction.Up:
                if (NodeFSM.up)
                {
                    NodeFSM = NodeFSM.up;
                }
                break;
            case PlayerInput.Direction.Down:
                if (NodeFSM.down)
                {
                    NodeFSM = NodeFSM.down;
                }
                break;
            case PlayerInput.Direction.Left:
                if (NodeFSM.left)
                {
                    NodeFSM = NodeFSM.left;
                }
                break;
            case PlayerInput.Direction.Right:
                if (NodeFSM.right)
                {
                    NodeFSM = NodeFSM.right;
                }
                break;
        }

        direction = PlayerInput.Direction.Null;
    }

    public bool IsMoving //Read Only
    { 
        get 
        {
            return isMoving;
        }
    }
}
