using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    bool _isGameOver = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            //Reload the active scene.  Much more reuseable than LoadScene(0); especially if we want to tweak difficulty.
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
