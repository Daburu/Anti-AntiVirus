using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
    public enum Direction
    { 
        Up,
        Down,
        Left,
        Right,
        Null
    }

    private PlayerController playerController;

    public float dotProductTolerance = 0.80f;
    public float swipeDuration = 0.5f;
    public float swipeDistanceScale = 0.15f;

    private float swipeDistance;
    private Vector2 firstTouch;
    private float firstTouchTime;
    private bool swipeAvailable = false;

    //DELETE FROM FINAL BUILD
    private string guiPhase = "Null";
    private float guiPosX = 0;
    private float guiPosY = 0;
    private Vector2 guiSwipeDir;
    private float guiSwipeMagnitude = 0;
    private Direction guiDirection = Direction.Null;
    //DELETE FROM FINAL BUILD

    #region Monobehaviour
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

	void Start () 
    {
        int screenheight = Screen.height;
        int screenWidth = Screen.width;

        if (screenheight >= screenWidth)
            swipeDistance = screenWidth * swipeDistanceScale;
        else
            swipeDistance = screenheight * swipeDistanceScale;

        firstTouch = new Vector2(0, 0);
        guiSwipeDir = new Vector2(0, 0);
	}
	
	void Update () 
    {
	    if(Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            { 
                case TouchPhase.Began:
                    swipeAvailable = true;
                    firstTouch = touch.position;
                    firstTouchTime = Time.time;

                    break;
                case TouchPhase.Ended:
                    firstTouch = new Vector2(0, 0);
                    swipeAvailable = false;

                    break;
                default:
                    UpdateGUIVars(touch);

                    //Conditions to prevent swipe detection.
                    //Note that firstTouch will still be recorded.
                    if(swipeAvailable == false) 
                        break;

                    if (playerController.IsMoving)
                        break;

                    if (playerController.IsMoving)
                        break;

                    Vector2 touchVector = touch.position - firstTouch;

                    if(touchVector.magnitude >= swipeDistance)
                    {
                        touchVector.Normalize();
                        Direction direction = CalculateSwipeDirection(touchVector);
                        playerController.SetDirection(direction);
                        swipeAvailable = false;

                        guiSwipeDir = touchVector; //DELETE FROM FINAL BUILD
                        guiDirection = direction;   // DELETE FROM FINAL BUILD
                    }

                    if(Time.time - firstTouchTime >= swipeDuration)
                    {
                        swipeAvailable = false;
                    }
                    break;
            }
        }
	}

    void OnGUI()//DELETE FROM FINAL BUILD
    {
        string message = "";
        message += "Phase: " + guiPhase + "\n";
        message += "Pos X: " + guiPosX + "\n";
        message += "Pos Y: " + guiPosY + "\n";
        message += "-First Touch-" + "\n";
        message += "X: " + firstTouch.x + " | Y: " + firstTouch.y + "\n";
        message += "-Swipe Magnitude-" + "\n";
        message += guiSwipeMagnitude + "\n";
        message += "-Swipe Direction-" + "\n";
        message += "X: " + guiSwipeDir.x + "\n";
        message += "Y: " + guiSwipeDir.y + "\n";
        message += "Swipe Direction: " + guiDirection.ToString() + "\n";

        GUI.Label(new Rect(0, 0, 150, 250), message);

        string message2 = "";
        message2 += "Screen Height: " + Screen.height + "\n";
        message2 += "Screen Width: " + Screen.width + "\n";
        message2 += "Slide Mag: " + swipeDistance;

        GUI.Label(new Rect(120, 0, 150, 150), message2);
    }
    #endregion

    void UpdateGUIVars(Touch touch) //DELETE FROM FINAL BUILD
    {
        guiPhase = touch.phase.ToString();
        guiPosX = touch.position.x;
        guiPosY = touch.position.y;
        guiSwipeMagnitude = Vector3.Magnitude(touch.position - firstTouch);
    }

    Direction CalculateSwipeDirection(Vector2 swipe)
    {
        if (Vector2.Dot(swipe, Vector2.up) >= dotProductTolerance)
        {
            return Direction.Up;
        }

        if (Vector2.Dot(swipe, Vector2.down) >= dotProductTolerance)
        {
            return Direction.Down;
        }

        if (Vector2.Dot(swipe, Vector2.left) >= dotProductTolerance)
        {
            return Direction.Left;
        }

        if (Vector2.Dot(swipe, Vector2.right) >= dotProductTolerance)
        { 
            return Direction.Right;
        }

        return Direction.Null;
    }
}
