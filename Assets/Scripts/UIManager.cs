using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    TMP_Text _scoreText;
    [SerializeField]
    TMP_Text _gameOverText;
    [SerializeField]
    TMP_Text _restartText;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private Image _lifeImage;
    [SerializeField]
    private Image _thrusterImage;

    public static UIManager Instance
    {
        get { return _instance; }
    }


    // Using onAwake, because it's called before Start.
    void Awake() //awake
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        _gameOverText.gameObject.SetActive(false);
    }



    public void UpdateScore(int value)
    {
        _scoreText.text = $"Score \n {value}";
    }

    public void UpdateLives(int value)
    {
        if (value > 3)
            value = 3;
        if (value < 0)
            value = 0;
        _lifeImage.sprite = _lifeSprites[value];
    }

    internal void OnPlayerDeath()
    {
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(PostDeathCoroutine());
        GameManager.Instance.GameOver();
    }

    IEnumerator PostDeathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _restartText.gameObject.SetActive(true);
    }

    internal void UpdateThruster(float v)
    {
        _thrusterImage.fillAmount = v;
    }
}
