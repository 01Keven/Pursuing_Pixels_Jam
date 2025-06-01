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

    public static bool IsAnimationDone(Animator animation, int layerIndex, string animationName)
    {
        var state = animation.GetCurrentAnimatorStateInfo(layerIndex);
        return state.IsName(animationName) && state.normalizedTime >= 1.0f;
    }


}
