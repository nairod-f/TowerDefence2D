using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour {
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;

    //a delegate, is a container for a function that can be passed around like a variable.


    //side note  -Use delegates when you want one game object to actively notify other game objects of changes.
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        if (enemyDelegate != null)
        {
            enemyDelegate(gameObject);
        }
    }
}
