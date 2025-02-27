using UnityEngine;

public class GameManager : MonoBehaviour
{
    //**** Singleton *****
    public static GameManager Instance;

    private int _score;
    public int Score => _score;  //Accesseur de l'attribut _score

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        _score = 0;
    }

    public void UpdateScore()
    {
        _score++;
        Debug.Log("Hit(s) : " + _score);
    }
}
