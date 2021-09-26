using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    [SerializeField] private Text scoreText;
    [SerializeField] private int scoreIncrement = 20;
    
    private int score;
    
    public int Score => score;
    
    void Start() {
        Display();
    }

    public void IncreaseScore() {
        score += scoreIncrement;
        Display();
    }

    private void Display() {
        scoreText.text = $"{score.ToString()}";
    }
}
