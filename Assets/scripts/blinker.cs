using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinker : MonoBehaviour
{
    private bool _IsActive = false;
    private int _pahase = 0;
    private float _timer;
    private Renderer _objectRenderer;
    private Color _initialColor;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
        _objectRenderer = gameObject.GetComponent<Renderer>();
        _initialColor = _objectRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsActive)
        {
            _timer += Time.deltaTime;
        }
        if(_timer >= 1f && _pahase == 0)
        {
            StartCoroutine(TurnRedForHalfSecond());
            GManager.Instance._getHurt(0.2f);
            //couse damage
            _pahase = 1;
        }else if (_timer > 5f)
        {
            _timer = 0f;
            _pahase = 0;
            _objectRenderer.material.color = new Color(255f / 255f, 165f / 255f, 0f);
        }
    }
    private IEnumerator TurnRedForHalfSecond()
    {
        _objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _objectRenderer.material.color = _initialColor;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            _IsActive = true;
            _objectRenderer.material.color = new Color(255f / 255f, 165f / 255f, 0f);
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
