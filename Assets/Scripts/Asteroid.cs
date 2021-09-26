using UnityEngine;

public class Asteroid : MonoBehaviour {
    private PlayerHealth _playerHealth;
    private ScoreSystem _scoreSystem;

    private void Start() {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
            playerHealth.Crash();
        Destroy(gameObject);
        Destroy(other.gameObject);
        FindObjectOfType<GameOverHandler>().GameOver();
    }

    private void OnBecameInvisible() {
        if (_playerHealth.Alive) {
            _scoreSystem.IncreaseScore();
        }
        Destroy(gameObject);
    }
}
