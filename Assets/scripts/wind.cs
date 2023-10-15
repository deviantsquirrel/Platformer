using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    [SerializeField] float _strength;
    private Vector3 _direction = new Vector3(1f, 0f,0f);
    private bool _IsActive = false;
    private float _timer = 0f;

    public void Update()
    {
        Debug.Log(_direction.x+ " " +_direction.y + " "+ _direction.z);
        if (_IsActive)
        {
            _timer += Time.deltaTime;
            if (_timer > 2f )
            {
                float randomX = Random.Range(-10f, 10f);
                float randomZ = Random.Range(-10f, 10f);
                _direction = new Vector3(randomX, 0f, randomZ);
                _timer = 0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _IsActive = true;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerController player = other.GetComponent<playerController>();

            if (player != null)
            {
                player.ApplyWindForce(_direction.normalized * _strength);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _IsActive = false;
    }

}
