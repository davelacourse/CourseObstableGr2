using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int _score;
    public int Score => _score;  //Accesseur de l'attribut _score

    private float _tempsDepart;
    public float TempsDepart => _tempsDepart;

    private List<float> _listeTempsNiveau = new List<float>();  // liste contenant le temps pour chaque niveau
    private List<int> _listeCollisionsNiveau = new List<int>(); // liste contenant les collisions pour chaque niveau
    

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
        //Si je suis au premier niveau les listes sont vides
        if (_listeTempsNiveau.Count == 0)
        {
            _listeTempsNiveau.Add(Time.time - TempsDepart); // Ajoute le temps depuis le départ du jeu
            _listeCollisionsNiveau.Add(_score); // Ajoute directement le collisions depuis le début
        }
        else
        {
            int nombreNiveau = _listeCollisionsNiveau.Count; // Compte combien de niveau ont été complété
            //Ajoute le temps pour le niveau en enlevant le temps du niveau précédent
            _listeTempsNiveau.Add(Time.time - _listeTempsNiveau[nombreNiveau-1]);
            //Ajoute le nombre de collision pour le niveau en enlevant les collision des niveaux précédent
            _listeCollisionsNiveau.Add(_score - _listeCollisionsNiveau.Sum());
        }
        
    }

    public void FinPartie()
    {
        float tempsTotal = 0;
        int collisionsTotales = 0;
        float pointageFinal = 0;

        Debug.Log(" ******  Fin partie ******");
        
        //Boucle qui affiche l'information poue chacun des niveaux !
        for (int i = 0; i < _listeTempsNiveau.Count; i++)
        {
            Debug.Log("Collisions totales niv" + (i+1) + ": " + _listeCollisionsNiveau[i]);
            Debug.Log("Temps total niv " + (i+1) + ": " + _listeTempsNiveau[i].ToString("f2") + " secondes.");
            Debug.Log("Pointage final niv" + (i+1) + " : " + 
                (_listeCollisionsNiveau[i] + _listeTempsNiveau[i]).ToString("f2") + " secondes.");
            Debug.Log("***************************");

            tempsTotal += _listeTempsNiveau[i];
            collisionsTotales += _listeCollisionsNiveau[i];
            pointageFinal += _listeCollisionsNiveau[i] + _listeTempsNiveau[i];

        }

        Debug.Log("Collisions Totales : " + collisionsTotales);
        Debug.Log("Temps Total : " + tempsTotal.ToString("f2") + " secondes.");
        Debug.Log("Pointage Final : " + pointageFinal.ToString("f2") + " secondes.");
    }
}
