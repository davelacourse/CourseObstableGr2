using UnityEngine;

public class GameManager : MonoBehaviour
{
    //**** Singleton *****
    public static GameManager Instance;

    private int _score;
    public int Score => _score;  //Accesseur de l'attribut _score
    private float _tempsDepart;

    private float _tempsNiveau1;
    private int _collisionsNiveau1;

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
    }

    public void SetNiveau1(float temps)
    {
        _tempsNiveau1 = temps - _tempsDepart;
        _collisionsNiveau1 = _score;
    }

    public void FinPartie()
    {
        float pointageNiveau1 = _tempsNiveau1 + _collisionsNiveau1;
        
        float tempsNiveau2 = Time.time - _tempsNiveau1;
        int collisionsNiveau2 = _score - _collisionsNiveau1;
        float pointageNiveau2 = tempsNiveau2 + collisionsNiveau2;

        float pointageFinal = pointageNiveau1 + pointageNiveau2;

        Debug.Log(" ******  Fin partie ******");
        Debug.Log("Collisions totales niv1 : " + _collisionsNiveau1);
        Debug.Log("Temps total niv 1: " + _tempsNiveau1.ToString("f2") + " secondes.");
        Debug.Log("Pointage final niv1 : " + pointageNiveau1.ToString("f2") + " secondes.");
        Debug.Log("***************************");
        Debug.Log("Collisions totales niv2 : " + collisionsNiveau2);
        Debug.Log("Temps total niv 2: " + tempsNiveau2.ToString("f2") + " secondes.");
        Debug.Log("Pointage final niv2 : " + pointageNiveau2.ToString("f2") + " secondes.");
        Debug.Log("***************************");
        Debug.Log("Pointage Final : " + pointageFinal.ToString("f2") + " secondes.");
    }
}
