using System.Collections.Generic;
using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listePieges = new List<GameObject>();
    [SerializeField] private float _intensity = 500;
    
    [Header("Direction du vecteur de chute")]
    [SerializeField] private float _directionX = 0;
    [SerializeField] private float _directionY = -1;
    [SerializeField] private float _directionZ = 0;

    private List<Rigidbody> _listeRb = new List<Rigidbody>();
    private Vector3 _direction;
    private bool _isTrigger = false;

    private void Start()
    {
        foreach(var piege in _listePieges)
        {
            _listeRb.Add(piege.GetComponent<Rigidbody>());
            piege.GetComponent<Rigidbody>().useGravity = false;
        }
        _direction = new Vector3(_directionX, _directionY, _directionZ);

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player" && !_isTrigger)
        {
            foreach(var rb in _listeRb)
            {
                rb.gameObject.SetActive(true);
                rb.useGravity = true;
                rb.AddForce(_direction * _intensity);
            }
            _isTrigger = true;
        }
    }
}
