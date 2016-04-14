using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    private TileFSM tileFSM;
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
                UpdateTileFSM();
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
                if (tileFSM.up)
                {
                    destination = tileFSM.up.transform.position;
                }
                break;
            case PlayerInput.Direction.Down:
                if (tileFSM.down)
                {
                    destination = tileFSM.down.transform.position;
                }
                break;
            case PlayerInput.Direction.Left:
                if (tileFSM.left)
                {
                    destination = tileFSM.left.transform.position;
                }
                break;
            case PlayerInput.Direction.Right:
                if (tileFSM.right)
                {
                    destination = tileFSM.right.transform.position;
                }
                break;
        }
    }

    public void SetTileFSM(TileFSM newTileFSM)
    {
        tileFSM = newTileFSM;
    }

    private void UpdateTileFSM()
    {
        switch (direction)
        {
            case PlayerInput.Direction.Up:
                if (tileFSM.up)
                {
                    tileFSM = tileFSM.up;
                }
                break;
            case PlayerInput.Direction.Down:
                if (tileFSM.down)
                {
                    tileFSM = tileFSM.down;
                }
                break;
            case PlayerInput.Direction.Left:
                if (tileFSM.left)
                {
                    tileFSM = tileFSM.left;
                }
                break;
            case PlayerInput.Direction.Right:
                if (tileFSM.right)
                {
                    tileFSM = tileFSM.right;
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
