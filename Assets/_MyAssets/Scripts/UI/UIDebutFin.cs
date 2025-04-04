using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDebutFin : MonoBehaviour
{
    [SerializeField] GameObject _panelPrincipal = default;
    [SerializeField] GameObject _panelInstrction = default;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>(); //FindObjectOfType<GameManager>();
            Destroy(gameManager);
        }
    }

    public void DebutJeu()
    {
        int noScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(noScene + 1);
    }

    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ToggleInstructions()
    {
        bool toggle = _panelPrincipal.activeSelf;
        _panelPrincipal.SetActive(!toggle);
        _panelInstrction.SetActive(toggle);
    }
}
