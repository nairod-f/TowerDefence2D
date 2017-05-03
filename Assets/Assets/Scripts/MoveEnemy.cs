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
                // TODO: Rotate into move direction
            }
            else
            {
                // desroy and trigger sound effect
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                // TODO: deduct health
            }
        }
    }
}
