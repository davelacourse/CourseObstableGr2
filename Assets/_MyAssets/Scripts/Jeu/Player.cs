using UnityEngine;

public class Player : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 1000f; // vitesse de rotation du joueur

    private Animator _animator;
    private PlayerInputActions _playerInputActions;
    private Rigidbody _rb;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();  // Instancie un objet de type PlayerInputActions
        _playerInputActions.Player.Enable();  // Active l'action Map "Player"
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        //S'assure d'arrête la souscription à l'action Player quand le joueur est détruit
        _playerInputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        PlayerMouvements();
    }

    private void PlayerMouvements()
    {
        // **Ancien Input Manager
        //float dirX = Input.GetAxis("Horizontal");
        //float dirZ = Input.GetAxis("Vertical");
        //Vector3 direction = new Vector3(dirX, 0f, dirZ);

        Vector2 direction2D = _playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(direction2D.x, 0f, direction2D.y);

        // Normalise mon vecteur à une valeur maximale de 1
        direction = direction.normalized;


        // Déplacement par téléportation du transform
        //transform.Translate(direction * Time.deltaTime * _playerSpeed, Space.World);

        // Donne une vitesse au corps physique dans la direction vecteur
        //_rb.linearVelocity = direction * Time.fixedDeltaTime * _playerSpeed;

        //Appliquer une force sur l'objet dans la direction du vecteur
        _rb.AddForce(direction * Time.fixedDeltaTime * _playerSpeed);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            _animator.SetBool(IS_WALKING, true);
        }
        else
        {
            _animator.SetBool(IS_WALKING, false);
        }
    }
}
