using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShAndFall : MonoBehaviour
{
    private bool _IsActive = false;
    private bool _shake = false;
    private bool _fall = false;
    private Vector3 _goDown = new Vector3(0f, -1f, 0f);
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_shake)
        {
            _timer += Time.deltaTime;
            if(_timer < 0.5f)
            {
                transform.Translate(_goDown * 10f * Time.deltaTime);
            }else if (_timer > 0.5f &&  _timer < 1f)
            {
                transform.Translate(_goDown * -10f * Time.deltaTime);
            }
            else
            {
                _shake = false;
                StartCoroutine(WaitForFall(1.5f));
            }
        }
        if (_fall)
        {
            transform.Translate(_goDown * 20f * Time.deltaTime);
        }
    }

    private IEnumerator WaitForFall(float time)
    {
        yield return new WaitForSeconds(time);
        if (_IsActive)
        {
            _fall = true;
        }

    }
    private IEnumerator WaitforShake(float time)
    {
        yield return new WaitForSeconds(time);
        if (_IsActive)
        {
            _shake = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            _IsActive = true;
            StartCoroutine(WaitforShake(0.5f));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            _IsActive = false;
        }
    }
}
