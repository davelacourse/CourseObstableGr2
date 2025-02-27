using UnityEngine;

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
                Debug.Log("Fin partie !!! Hit(s)=" + GameManager.Instance.Score);
                collision.gameObject.SetActive(false); // Déastive le joueur sur la scène
            }
        }
    }
}
