using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool mute = false;
    public static bool isGameStarted;

    public static int currentLevelIndex;
    public static int numberOfPassedRings;
    public static int score = 0;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject GamePlayPanel;
    public GameObject startMenuPanel;
    public Slider gameProgressSlider;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;


    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        HighScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        isGameStarted =gameOver = levelCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        scoreText.text = score.ToString();
        //Update UI
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)//for Mobile
            if (Input.GetMouseButtonDown(0) && !isGameStarted)
            {
            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//for Mobile
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
            isGameStarted = true;
            GamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
            {
                if(score >PlayerPrefs.GetInt("HighScore",0))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }
                score = 0;
                SceneManager.LoadScene("Level");
            }
        }

        if (levelCompleted)
        {
            
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1")) 
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
