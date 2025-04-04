using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private Material _material = default(Material);

    private bool _isHit = false;  // Bool�en qui v�rifie si l'item a �t� touch�
    
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
                GameManager.Instance.SetNiveau(Time.time);
                
                int noScene = SceneManager.GetActiveScene().buildIndex;
                // Teste si l'on est sur la derni�re sc�ne
                if (noScene == SceneManager.sceneCountInBuildSettings - 1)
                {
                    GameManager.Instance.FinPartie();
                    collision.gameObject.SetActive(false); // D�astive le joueur sur la sc�ne
                }
                else
                {
                    SceneManager.LoadScene(noScene + 1);
                }
            }
        }
    }
}
