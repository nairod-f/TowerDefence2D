using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {
    //ensures you cannot accidentally change the field in the inspector, but is still accessable from other scripts1
    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    // Use this for initialization
    void Start () {
        lastWaypointSwitchTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        // retrieve the waypoint segments from start to finish
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
        //calculating total time needed to finish path
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        //Check whether the enemy has reached the endPosition. - Yes = add waypoint - desroy and trigger sound effect
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                // add waypoint
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }
            else
            {
                // desroy and trigger sound effect
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                GameManagerBehaviour gameManager =
                GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
                gameManager.Health -= 1;
            }
        }
    }
    private void RotateIntoMoveDirection()
    {
        //calculate movement direction by subtracting the current waypoint’s position

        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);

        //Mathf.Atan2 to determine the angle toward which newDirection points
        //n radians, assuming zero points to the right. Multiplying the result by 180 / Mathf.PI 
        //converts the angle to degrees.

        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;

        //retrieves the child named Sprite and rotates it rotationAngle 

        GameObject sprite = (GameObject)
            gameObject.transform.FindChild("Sprite").gameObject;

        sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

    public float distanceToGoal()
    {
        //calculates the length of road not yet traveled by the enemy.

        float distance = 0;
        distance += Vector3.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }
}
