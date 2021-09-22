using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour {
    private const float WrapAroundOffset = 0.1f;
    
    [SerializeField] private float forceMagnitude = 500;
    [SerializeField] private float maxVelocity = 6;

    private Vector3 movementDirection;
    
    private Camera mainCamera;
    private Rigidbody rigidbody;

    private void OnEnable() {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable() {
        EnhancedTouchSupport.Disable();
    }

    void Start() {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        ProcessInput();
        WrapAroundScreen();

    }

    private void ProcessInput() {
        var activeTouches = Touch.activeTouches;
        if (activeTouches.Count <= 0) {
            movementDirection = Vector3.zero;
            return;
        }
        var touch = activeTouches[0];
        var touchWorldPosition = mainCamera.ScreenToWorldPoint(touch.screenPosition);
        touchWorldPosition.y = 0;

        movementDirection = transform.position - touchWorldPosition;
        movementDirection.y = 0;
        movementDirection.Normalize();
    }

    private void WrapAroundScreen() {
        var newPosition = transform.position;

        var viewportPosition = mainCamera.WorldToViewportPoint(newPosition);
        var viewportX = viewportPosition.x;
        var viewportY = viewportPosition.y;
        if (viewportX > 0 && viewportX < 1 && viewportY > 0 && viewportY < 1) return;

        if (viewportX >= 1) {
            newPosition.x = -newPosition.x + WrapAroundOffset;
        } else if (viewportX <= 0) {
            newPosition.x = -newPosition.x - WrapAroundOffset;
        }

        if (viewportY >= 1) {
            newPosition.z = -newPosition.z + WrapAroundOffset;
        } else if (viewportY <= 0) {
            newPosition.z = -newPosition.z - WrapAroundOffset;
        }

        transform.SetPositionAndRotation(newPosition, Quaternion.identity);
    }

    private void FixedUpdate() {
        if (movementDirection == Vector3.zero) return;
        rigidbody.AddForce(movementDirection * forceMagnitude);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
    }
}
