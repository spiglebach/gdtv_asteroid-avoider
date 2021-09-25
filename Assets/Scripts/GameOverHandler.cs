using UnityEngine;

public class GameOverHandler : MonoBehaviour {
    [SerializeField] private GameObject gameOverCanvas;

    private void Awake() {
        gameOverCanvas.SetActive(false);
    }

    public void GameOver() {
        gameOverCanvas.SetActive(true);
    }
}
