using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fire : MonoBehaviour
{
    private float _time = 0f;
    [SerializeField] private GameObject _fire;
    [SerializeField] private GameObject _fireCol;
    ParticleSystem _particleSystem;
    private bool _alive;
    private bool _dying;
    void Start()
    {
        _fire.SetActive(false);
        _fireCol.SetActive(false);
        _alive = false;
        _dying = false;
        _particleSystem = _fire.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > 2f && !_alive)
        {
            var emission = _particleSystem.emission;
            emission.rateOverTime = 10f;
            _alive = true;
            _dying = true;
            _fire.SetActive(true);
            _fireCol.SetActive(true);
        }
        else if (_time > 4.7f && _dying)
        {
            var emission = _particleSystem.emission;
            emission.rateOverTime = 0f;
            _dying = false;
        }
        else if(_time > 6f && _alive)
        {
            _alive = false;
            _fire.SetActive(false);
            _fireCol.SetActive(false);
            _time = 0f;
        }


    }
}
