using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int _score;
    public int Score => _score;  //Accesseur de l'attribut _score

    private float _tempsDepart;
    public float TempsDepart => _tempsDepart;

    private float _tempFinal;
    public float TempFinal => _tempFinal;

    //**** Singleton *****
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        _score = 0;
        _tempsDepart = Time.time;
    }

    //============== Méthodes publiques ===================
    
    public void UpdateScore()
    {
        _score++;
        UIManager.Instance.UpdateScore(_score);
    }

    // Méthode appellé à la fin de chaque niveau elle ajoute des les liste le temps et les collisions pour ce niveau
    public void SetNiveau(float p_temps)
    {
        _tempFinal = p_temps;  
    }


}
