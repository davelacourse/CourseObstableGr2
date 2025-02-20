using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 1000f; // vitesse de rotation du joueur

    private void Update()
    {
        PlayerMouvements();
    }

    private void PlayerMouvements()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(dirX, 0f, dirZ);

        // Normalise mon vecteur à une valeur maximale de 1
        direction = direction.normalized;

        transform.Translate(direction * Time.deltaTime * _playerSpeed, Space.World);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
