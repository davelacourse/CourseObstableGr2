using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class UIDebutFin : UI
{
    [Header("Scène Début")]
    [SerializeField] GameObject _panelPrincipal = default;
    [SerializeField] GameObject _panelInstrction = default;
    [SerializeField] GameObject _boutonDemarrer = default;
    [SerializeField] GameObject _boutonRetourInstructions = default;

    [Header("Scène Fin")]
    [SerializeField] TMP_Text _txtTemps = default(TMP_Text);
    [SerializeField] TMP_Text _txtCollisions = default(TMP_Text);
    [SerializeField] TMP_Text _txtPointage = default(TMP_Text);

    private bool _intructionsOn = false;

    private void Start()
    {
        if ((GameManager.Instance != null && SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1))
        {
            AffichageResultats();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)  // Si scène de départ
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonDemarrer);
        }
        DestructionGameManager();
    }

    private static void DestructionGameManager()
    {
        if (GameManager.Instance != null && SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>(); //FindObjectOfType<GameManager>();
            Destroy(gameManager);
        }
    }

    private void AffichageResultats()
    {
        _txtTemps.text = "Temps : " + (GameManager.Instance.TempFinal - GameManager.Instance.TempsDepart).ToString("f2") + " sec.";
        _txtCollisions.text = "Collisions : " + GameManager.Instance.Score;
        float total = (GameManager.Instance.TempFinal - GameManager.Instance.TempsDepart) + GameManager.Instance.Score;
        _txtPointage.text = "Pointage final : " + total.ToString("f2") + " sec.";
    }

    public void ToggleInstructions()
    {
        bool toggle = _panelPrincipal.activeSelf;
        _panelPrincipal.SetActive(!toggle);
        _panelInstrction.SetActive(toggle);

        if (!_intructionsOn)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonRetourInstructions);
            _intructionsOn = true;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonDemarrer);
            _intructionsOn = false;
        }
    }
}
