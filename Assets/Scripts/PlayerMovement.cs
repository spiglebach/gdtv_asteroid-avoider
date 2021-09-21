using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour {
    private Camera mainCamera;

    private void OnEnable() {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable() {
        EnhancedTouchSupport.Disable();
    }

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        var activeTouches = Touch.activeTouches;
        if (activeTouches.Count <= 0) return;
        var touch = activeTouches[0];
        var touchWorldPosition = mainCamera.ScreenToWorldPoint(touch.screenPosition);
        touchWorldPosition.y = 0;
        transform.SetPositionAndRotation(touchWorldPosition, Quaternion.identity);
    }
}
