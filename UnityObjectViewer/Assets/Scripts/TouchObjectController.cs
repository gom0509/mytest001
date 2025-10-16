using UnityEngine;

/// <summary>
/// 안드로이드에서 터치 제스처로 3D 오브젝트를 회전, 확대/축소, 이동할 수 있도록 제어합니다.
/// 제스처에 반응할 루트 오브젝트에 이 컴포넌트를 추가하세요.
/// </summary>
public class TouchObjectController : MonoBehaviour
{
    [Header("회전")]
    [Tooltip("한 손가락 드래그 시 회전 속도에 곱해지는 계수입니다.")]
    public float rotationSpeed = 0.4f;

    [Header("확대/축소")]
    [Tooltip("핀치 제스처 시 오브젝트 크기가 변하는 속도입니다.")]
    public float zoomSpeed = 0.005f;
    [Tooltip("시작 크기를 기준으로 허용되는 최소 배율입니다.")]
    public float minScaleMultiplier = 0.2f;
    [Tooltip("시작 크기를 기준으로 허용되는 최대 배율입니다.")]
    public float maxScaleMultiplier = 3f;

    [Header("이동")]
    [Tooltip("두 손가락으로 드래그할 때 오브젝트가 이동하는 속도입니다.")]
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
        // 확대/축소 처리
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

        // 이동 처리
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
