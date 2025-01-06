using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private int playerLives = 3;

    [SerializeField]
    private float deathDelay = 2;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        int sessionCount = FindObjectsOfType<GameSession>().Length;
        if (sessionCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        this.livesText.text = this.playerLives.ToString();
        this.scoreText.text = this.score.ToString();
    }

    public void ProcessPlayerDeath()
    {

        this.StartCoroutine(ProcessPlayerDeathInternal());
    }

    private IEnumerator ProcessPlayerDeathInternal()
    {
        this.playerLives -= 1;
        yield return new WaitForSecondsRealtime(this.deathDelay);

        if (this.playerLives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FindObjectOfType<ScenePersist>().ResetScenePersistence();
            SceneManager.LoadScene(0);
            Destroy(this.gameObject);
        }
    }

    public void IncrementScore()
    {
        this.score++;
    }

    public int GetScore() => this.score;
}
