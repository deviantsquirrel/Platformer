using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody _rd;
    private bool _Alive = true;
    private bool _TimeOn = false;
    private float _timer = 0f;


    [SerializeField] float _moveSpeed = 4;
    [SerializeField] float _rotationSpeed = 5;
    [SerializeField] float _gravity = -20f;
    [SerializeField] float _jumpSpeed = 8;

    CharacterController _characterController;
    Vector3 _moveVelocity;
    Vector3 _turnVelocity;

    void Start()
    {
        _rd = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_TimeOn)
        {
            _timer += Time.deltaTime;
        }
        if (_Alive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (_characterController.isGrounded)
            {
                _moveVelocity = transform.forward * _moveSpeed * verticalInput;
                _turnVelocity = transform.up * _rotationSpeed * horizontalInput;
                if (Input.GetButtonDown("Jump"))
                {
                    _moveVelocity.y = _jumpSpeed;
                }
            }
            _moveVelocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_moveVelocity * Time.deltaTime);
            transform.Rotate(_turnVelocity * Time.deltaTime);
        }
    }
    public void Die()
    {
        _Alive = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("line"))
        {
            _TimeOn = true;
        }
        if (other.gameObject.CompareTag("lineEnd"))
        {
            _TimeOn = false;
            GManager.Instance.GameOver(true, _timer);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("worldColider"))
        {
            _Alive = false;
            GManager.Instance.GameOver(false, _timer);
        }
    }
    public float GetTime()
    {
        return _timer;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("rain"))
        {
            GManager.Instance._getHurt(0.005f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("fire"))
        {
            GManager.Instance._getHurt(0.05f);
        }
    }
    public void ApplyWindForce(Vector3 force)
    {
        _characterController.Move(force * Time.deltaTime);
    }


}
