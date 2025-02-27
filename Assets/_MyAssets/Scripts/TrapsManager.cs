using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    [SerializeField] private GameObject _piege = default(GameObject);
    [SerializeField] private float _intensity = 500;
    
    [Header("Direction du vecteur de chute")]
    [SerializeField] private float _directionX = 0;
    [SerializeField] private float _directionY = -1;
    [SerializeField] private float _directionZ = 0;

    private Rigidbody _rb;
    private Vector3 _direction;

    private void Start()
    {
        _rb = _piege.GetComponent<Rigidbody>();
        _rb.useGravity= false;
        _direction = new Vector3(_directionX, _directionY, _directionZ);

    }

    private void OnTriggerEnter(Collider other)
    {
        _rb.useGravity = true;
        _rb.AddForce(_direction * _intensity);
    }
}
