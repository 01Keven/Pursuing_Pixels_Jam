using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils Instance { get; private set; }

    public static Vector3 GetMouseWorldPosition(Camera worldCamera, Vector3 screePosition)
    {
        Vector3 vec = worldCamera.ScreenToWorldPoint(screePosition);
        vec.z = 0;

        return vec;
    }


}
