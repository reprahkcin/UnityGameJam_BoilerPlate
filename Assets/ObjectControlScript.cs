using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectControlScript : MonoBehaviour, IDrag
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void onStartDrag()
    {
        _rb.useGravity = false;
    }

    public void onEndDrag()
    {
        _rb.useGravity = true;
    }
}
