using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; }

    private float _playerHealth;
    private bool _finished = false;
    [SerializeField] Image _HealthBar;
    [SerializeField] TextMeshProUGUI _endText;
    [SerializeField] TextMeshProUGUI _timer;
    [SerializeField] GameObject _restart;
    [SerializeField] GameObject _player;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerHealth = 1f;
        _restart.SetActive(false);
        _endText.text = "";
        _timer.text = "";
    }

    public void _getHurt(float hurt)
    {
        _playerHealth -= hurt;
        _HealthBar.fillAmount = _playerHealth;
        Debug.Log(_playerHealth);
        if(_playerHealth <= 0.05f) {
            float tt = _player.GetComponent<playerController>().GetTime();
            GameOver(false, tt);
            _player.GetComponent<playerController>().Die();
        }

    }

    public void RestartSceene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver(bool won, float tt)
    {
        if (!_finished)
        {
            _restart.SetActive(true);
            if (won)
            {
                _endText.text = "Победа!";
                _timer.text = "Время: " + Math.Round(tt, 2).ToString();
            }
            else
            {
                _endText.text = "Поражение!";
            }
            _finished = true;
        }
    }
}
