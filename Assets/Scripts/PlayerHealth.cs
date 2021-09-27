using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private bool alive = true;

    public bool Alive => alive;

    public void Crash() {
        alive = false;
        gameObject.SetActive(false);
    }

    public void Continue() {
        alive = true;
    }
}
