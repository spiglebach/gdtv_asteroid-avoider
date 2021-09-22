using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour {
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
        var activeTouches = Touch.activeTouches;
        if (activeTouches.Count <= 0) {
            movementDirection = Vector3.zero;
            return;
        }
        var touch = activeTouches[0];
        var touchWorldPosition = mainCamera.ScreenToWorldPoint(touch.screenPosition);
        touchWorldPosition.y = 0;

        movementDirection = transform.position - touchWorldPosition;
        movementDirection.Normalize();
    }

    private void FixedUpdate() {
        if (movementDirection == Vector3.zero) return;
        rigidbody.AddForce(movementDirection * forceMagnitude);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
    }
}
