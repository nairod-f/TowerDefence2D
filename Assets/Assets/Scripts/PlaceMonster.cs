using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour {

    public GameObject monsterPrefab;
    private GameObject monster;

    private bool canPlaceMonster()
    {
        return monster == null;
    }
    //player can add a monster to an empty spot (checking wether it is occupied or not)
    private void OnMouseUp()
    {
        if (canPlaceMonster())
        {
            //creating monsters by instantiating a given prefab (monster in this case)
            monster = (GameObject)
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        }
        else if (canUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().increaseLevel();
            //adding audio to the monster prefac when created
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
    private bool canUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.getNextLevel();
            if (nextLevel != null)
            {
                return true;
            }
        }
        return false;
    }
}

