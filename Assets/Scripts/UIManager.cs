using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    TMP_Text _scoreText;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private Image _lifeImage;


    public static UIManager Instance
    {
        get { return _instance; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

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


}
