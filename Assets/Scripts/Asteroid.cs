using UnityEngine;

public class Asteroid : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
            playerHealth.Crash();
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
