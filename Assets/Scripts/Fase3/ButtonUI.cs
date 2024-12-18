using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] string actualLevel = "Fase 3";
    public void tryAgainButton()
    {
        SceneManager.LoadScene(actualLevel);
    }
}
