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
                if (noScene == SceneManager.sceneCountInBuildSettings - 2)
                {
                    GameManager.Instance.SetNiveau(Time.time);
                }
                SceneManager.LoadScene(noScene + 1);
            }
        }
    }
}
