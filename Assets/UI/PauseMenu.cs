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
        Time.timeScale = 1f;
        foreach (Image image in elements)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        foreach (Image image in elements)
        {
            image.gameObject.SetActive(true);
        }
    }

    public void Settings()
    {
    }
}
