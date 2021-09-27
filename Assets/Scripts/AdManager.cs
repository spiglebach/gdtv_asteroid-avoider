using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener {
    [SerializeField] private bool testMode = true;
    
    public static AdManager Instance;

    private GameOverHandler _gameOverHandler;

#if UNITY_ANDROID
    private const string GameId = ""; // TODO fill
#elif UNITY_IOS
    private const string GameId = ""; // TODO fill
#endif

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.AddListener(this);
            Advertisement.Initialize(GameId, testMode);
        }
    }

    public void OnUnityAdsReady(string placementId) {
        Debug.Log("Unity Ad Ready");
    }

    public void OnUnityAdsDidError(string message) {
        Debug.LogError($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidStart(string placementId) {
        Debug.Log("Unity Ad Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        switch (showResult) {
            case ShowResult.Finished:
                Debug.Log("Unity Ad Finished");
                _gameOverHandler.ContinueGame();
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Unity Ad Failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Unity Ad Skipped");
                break;
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler) {
        _gameOverHandler = gameOverHandler;
        Advertisement.Show("Rewarded_Video");
    }
}
