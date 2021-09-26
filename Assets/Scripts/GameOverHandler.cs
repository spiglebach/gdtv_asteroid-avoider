using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour {
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private Text gameOverText;
    private ScoreSystem _scoreSystem;

    private void Awake() {
        gameOverCanvas.SetActive(false);
        _scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    public void GameOver() {
        gameOverText.text = $"Your score is {_scoreSystem.Score.ToString()}";
        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
    }
}
