using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour {
    // keep track of all enemies within range
    public List<GameObject> enemiesInRange;

    // Use this for initialization
    void Start () {
        enemiesInRange = new List<GameObject>();
    }
    // remove enemy from enemiesinrange- monster walks in trigger  OnTrigger2D is called

    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // This makes sure that OnEnemyDestroy is called when the enemy is destroyed.
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }
    // you remove the enemy from the list and unregister your delegate.
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
