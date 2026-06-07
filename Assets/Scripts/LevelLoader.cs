using UnityEngine;
using System.Collections.Generic;
public class LevelLoader : MonoBehaviour
{

    public List<GameObject> levelObjects= new List<GameObject>();



    public void LoadLevel(int level)
    {
        levelObjects[level-1].SetActive(true);
    }

    public void UnloadLevel(int level)
    {
        levelObjects[level-1].SetActive(false);
    }

}
