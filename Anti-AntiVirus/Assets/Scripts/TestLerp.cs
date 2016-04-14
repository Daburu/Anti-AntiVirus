using UnityEngine;
using System.Collections;

public class TestLerp : MonoBehaviour 
{
    public Vector3 destination = new Vector3(0, -10f, 0);
    public float minChange = 30f;
    public float magnitudeCoefficient = 1f;

    private float positionDelta;
    private float distBetween;
    private bool moveBool = false;

    void Start()
    {

    }

	void Update () 
    {
        if(Input.GetKeyDown("space"))
        {
            moveBool = true;
        }

        if (moveBool)
        {
            distBetween = Vector3.Magnitude(destination - transform.position);
            positionDelta = (minChange + magnitudeCoefficient * distBetween) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, positionDelta);
        }
    }
}
