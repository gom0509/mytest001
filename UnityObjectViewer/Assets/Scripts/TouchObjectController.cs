using UnityEngine;

/// <summary>
/// Provides touch-based rotation, zoom and pan controls for an object on Android.
/// Attach this component to the root object that should respond to gestures.
/// </summary>
public class TouchObjectController : MonoBehaviour
{
    [Header("Rotation")]
    [Tooltip("Multiplier applied to drag distance when rotating the object.")]
    public float rotationSpeed = 0.4f;

    [Header("Zoom")]
    [Tooltip("Speed at which the object scales when pinching.")]
    public float zoomSpeed = 0.005f;
    [Tooltip("Minimum scale multiplier relative to the starting scale.")]
    public float minScaleMultiplier = 0.2f;
    [Tooltip("Maximum scale multiplier relative to the starting scale.")]
    public float maxScaleMultiplier = 3f;

    [Header("Pan")]
    [Tooltip("Speed at which the object moves when panning with two fingers.")]
    public float panSpeed = 0.0025f;

    private Vector3 _initialScale;
    private Camera _mainCamera;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            HandleRotation(Input.GetTouch(0));
        }
        else if (Input.touchCount >= 2)
        {
            HandlePinchAndPan(Input.GetTouch(0), Input.GetTouch(1));
        }
    }

    private void HandleRotation(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
        {
            return;
        }

        Vector2 delta = touch.deltaPosition;
        float rotationX = -delta.y * rotationSpeed;
        float rotationY = delta.x * rotationSpeed;

        Transform referenceTransform = GetReferenceTransform();
        Vector3 xAxis = referenceTransform != null ? referenceTransform.right : Vector3.right;

        transform.Rotate(xAxis, rotationX, Space.World);
        transform.Rotate(Vector3.up, rotationY, Space.World);
    }

    private void HandlePinchAndPan(Touch touch0, Touch touch1)
    {
        // Zoom
        Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
        Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;
        float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;
        float difference = currentMagnitude - prevMagnitude;

        float scaleFactor = 1f + (difference * zoomSpeed);
        float currentMultiplier = transform.localScale.x / Mathf.Max(_initialScale.x, Mathf.Epsilon);
        float targetMultiplier = currentMultiplier * scaleFactor;
        float clampedMultiplier = Mathf.Clamp(targetMultiplier, minScaleMultiplier, maxScaleMultiplier);
        transform.localScale = _initialScale * clampedMultiplier;

        // Pan
        Vector2 averageDelta = (touch0.deltaPosition + touch1.deltaPosition) * 0.5f;
        Transform referenceTransform = GetReferenceTransform();
        Vector3 right = referenceTransform != null ? referenceTransform.right : Vector3.right;
        Vector3 up = referenceTransform != null ? referenceTransform.up : Vector3.up;
        Vector3 translation = (-right * averageDelta.x - up * averageDelta.y) * panSpeed;
        transform.Translate(translation, Space.World);
    }

    private Transform GetReferenceTransform()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        return _mainCamera != null ? _mainCamera.transform : null;
    }
}
