using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [Header("Set binding for mouse click")]
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private float _mouseDragPhysicsSpeed = 10f;
    [SerializeField] private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _mouseDragSpeed = 0.1f;

    private Camera _mainCamera;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext obj)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && (hit.collider.gameObject.CompareTag("Draggable")) || (hit.collider.gameObject.layer == LayerMask.NameToLayer("Draggable")) || (hit.collider.gameObject.GetComponent<IDrag>() != null))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

        // RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        // {
        //     if (hit2D.collider != null && (hit2D.collider.gameObject.CompareTag("Draggable")) || (hit2D.collider.gameObject.layer == LayerMask.NameToLayer("Draggable")) || (hit2D.collider.gameObject.GetComponent<IDrag>() != null))
        //     {
        //         StartCoroutine(DragUpdate(hit2D.collider.gameObject));
        //     }
        // }


    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag();
        float initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * _mouseDragPhysicsSpeed;
                yield return _waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref _velocity, _mouseDragSpeed);
                yield return null;
            }

        }

        iDragComponent?.onEndDrag();
    }
}
