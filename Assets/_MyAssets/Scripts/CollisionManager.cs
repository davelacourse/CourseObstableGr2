using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private Material _material = default(Material);

    private bool _isHit = false;  // Booléen qui vérifie si l'item a été touché
    
    private void OnCollisionEnter(Collision collision)
    {
        // Si la collision de produit avec le joueur
        if (collision.gameObject.tag == "Player")
        {
            if (!_isHit && this.gameObject.tag != "End")
            {
                GetComponent<MeshRenderer>().material = _material;
                GameManager.Instance.UpdateScore();
                _isHit = true;
            }
            else if (this.gameObject.tag == "End" && !_isHit)
            {
                int noScene = SceneManager.GetActiveScene().buildIndex;
                // Teste si l'on est sur la dernière scène
                if (noScene == SceneManager.sceneCountInBuildSettings - 1)
                {

                    GameManager.Instance.FinPartie();
                    collision.gameObject.SetActive(false); // Déastive le joueur sur la scène
                }
                else
                {
                    GameManager.Instance.SetNiveau1(Time.time);
                    SceneManager.LoadScene(noScene + 1);
                }
                
            }
        }
    }
}
