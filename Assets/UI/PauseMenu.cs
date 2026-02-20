using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Image[] elements;
    private void Awake()
    {
        foreach (Image image in elements)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        foreach (Image image in elements)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void Pause()
    {
        foreach (Image image in elements)
        {
            image.gameObject.SetActive(true);
        }
    }
}
