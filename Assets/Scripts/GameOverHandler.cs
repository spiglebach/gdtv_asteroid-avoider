using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button continueButton;
    private ScoreSystem _scoreSystem;

    private void Awake() {
        gameOverCanvas.SetActive(false);
        _scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    public void GameOver() {
        gameOverText.text = $"Your score is {_scoreSystem.Score.ToString()}";
        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void ContinueButton() {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
    }

    public void ContinueGame() {
        Time.timeScale = 1;
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        player.SetActive(true);
        player.GetComponent<PlayerHealth>().Continue();
    }
}
