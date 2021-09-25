using UnityEngine;

public class Asteroid : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
            playerHealth.Crash();
        Destroy(gameObject);
        Destroy(other.gameObject);
        FindObjectOfType<GameOverHandler>().GameOver();
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
