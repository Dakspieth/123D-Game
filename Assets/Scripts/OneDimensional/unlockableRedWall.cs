using UnityEngine;

public class unlockableRedWall : MonoBehaviour
{
    void OnEnable()
    {
        RedWallKey.unlockRedWall += Unlock;
    }

    void OnDestroy()
    {
        RedWallKey.unlockRedWall -= Unlock;  
    }

    void Unlock()
    {
        print("Wall Unlocked!");
        Destroy(gameObject);
    }
}
