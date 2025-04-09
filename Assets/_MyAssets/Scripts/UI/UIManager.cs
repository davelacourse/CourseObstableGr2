using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIManager : UI
{
    [SerializeField] private TMP_Text _txtTemps = default(TMP_Text);
    [SerializeField] private TMP_Text _txtCollisions = default(TMP_Text);
    [SerializeField] private GameObject _panelPause = default(GameObject);
    [SerializeField] private GameObject _boutonContinuer = default(GameObject);


    private bool _enPause = false;
    private PlayerInputActions _playerInputActions;

    //**** Singleton *****
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _playerInputActions = new PlayerInputActions();  // Instancie un objet de type PlayerInputActions
        _playerInputActions.Player.Enable();  // Active l'action Map "Player"
        _playerInputActions.Player.Pause.performed += Event_Pause;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Pause.performed -= Event_Pause;
        _playerInputActions.Player.Disable();
    }

    private void Event_Pause(InputAction.CallbackContext obj)
    {
        TogglePause();
    }

    private void Start()
    {
        _enPause = false;
        Time.timeScale = 1.0f;
        _txtCollisions.text = "Collisions : " + GameManager.Instance.Score.ToString();
    }

    private void Update()
    {
        float temps = Time.time - GameManager.Instance.TempsDepart;
        _txtTemps.text = "Temps : " + temps.ToString("f2");
        
        /*
        // Utilisation Input Manager (old)
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
        */
        
    }

    public void UpdateScore(int p_pointage)
    {
        _txtCollisions.text = "Collisions : " + p_pointage.ToString();
    }

    public void TogglePause()
    {
        _panelPause.SetActive(!_panelPause.activeSelf);
        if (_enPause)
        {
            Time.timeScale = 1f;
            _enPause = false;
        }
        else
        {
            Time.timeScale = 0f;
            _enPause = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonContinuer);
            
        }
    }
}
