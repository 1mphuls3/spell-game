using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class DoorExit : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene(0);
    }
}
