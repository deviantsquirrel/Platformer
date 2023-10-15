using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotate : MonoBehaviour
{
    private bool _IsActive = false;
    private bool _Rotate = false;
    private Renderer _objectRenderer;
    private Color _initialColor;
    [SerializeField] float _rotationSpeed = 90f;
    private float _totalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _objectRenderer = gameObject.GetComponent<Renderer>();
        _initialColor = _objectRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Rotate) {
            _totalRotation += _rotationSpeed * Time.deltaTime;
            Vector3 newRotation = transform.eulerAngles + new Vector3(0f,0f, _rotationSpeed * Time.deltaTime);
            transform.eulerAngles = newRotation;
            if(_totalRotation >360f)
            {
                _totalRotation = 0f;
                if(!_IsActive) { _Rotate = false; }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            _IsActive = true;
            _Rotate = true;
            _objectRenderer.material.color = Color.green;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            _IsActive = false;
            _objectRenderer.material.color = _initialColor;
        }
    }
}
