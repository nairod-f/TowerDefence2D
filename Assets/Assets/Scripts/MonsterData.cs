using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterLevel
{
    public int cost;
    public GameObject visualization;
}
public class MonsterData : MonoBehaviour
{
    //the list gives the user access to the generic data structures of the script
    public List<MonsterLevel> levels;
    private MonsterLevel currentLevel;

    public MonsterLevel CurrentLevel
    {

        //defining the property of "current level"
        // defenition of a getter and setter method (costum behaviour) 
        //and by supplying only a getter, a setter or both, you can control whether a property is read-only, write-only or read/write

        get
        //In the getter, you return the value of currentLevel.
        {
            return currentLevel;
        }
        set
        //In the setter, you assign the new value to currentLevel. 
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }          
        }
    }
    void OnEnable()
    {
        CurrentLevel = levels[0];
    }
}