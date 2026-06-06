using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ResetScreen : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<Vector3> transforms = new List<Vector3>();



    public void AddToList(GameObject obj, Vector3 pos)
    {
        for(int i = 0; i < objects.Count; i++) {
            if(objects[i] == obj)
            {
                transforms[i] = pos;
                return;
            }
        }
        objects.Add(obj);
        transforms.Add(pos);
    }

    public void ReloadScreen()
    {
        for(int i = 0; i < objects.Count; i++) {
            objects[i].transform.position = transforms[i];
        }
    }
}
