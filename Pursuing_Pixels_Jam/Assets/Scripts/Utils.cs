using UnityEngine;

public class Utils : MonoBehaviour
{
    // Singleton instance
    public static Utils Instance { get; private set; }


    // M�todo para pegar a posi��o do mouse no mundo
    // Method to get the mouse position in the world
    public static Vector3 GetMouseWorldPosition(Camera worldCamera, Vector3 screePosition) // Recebe a c�mera e a posi��o do mouse na tela // Receives the camera and the mouse position on the screen
    {
        // Converte a posi��o da tela para o mundo usando a c�mera fornecida e a posi��o do mouse/ 
        //Convert the screen position to world position using the provided camera and mouse position
        Vector3 vec = worldCamera.ScreenToWorldPoint(screePosition);

        // Ajusta a posi��o Z para 0, pois estamos trabalhando em 2D
        // Adjust the Z position to 0, as we are working in 2D
        vec.z = 0;

        // Retorna a posi��o ajustada do mouse no mundo
        // Return the adjusted mouse position in the world
        return vec;
    }

    // M�todo para verificar se uma anima��o est� conclu�da
    // Method to check if an animation is done
    public static bool IsAnimationDone(Animator animation, int layerIndex, string animationName) // Recebe o Animator, o �ndice da camada e o nome da anima��o // Receives the Animator, layer index, and animation name
    {
        // Obt�m o estado atual da anima��o na camada especificada
        // Get the current animation state in the specified layer
        var state = animation.GetCurrentAnimatorStateInfo(layerIndex);

        // Verifica se o nome da anima��o corresponde ao nome fornecido e se a normalizedTime � maior ou igual a 1.0f
        // Check if the animation name matches the provided name and if normalizedTime is greater than or equal to 1.0f
        return state.IsName(animationName) && state.normalizedTime >= 1.0f;
    }


}
