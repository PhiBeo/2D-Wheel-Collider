using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    private void Update()
    {
        if (followTarget == null) return;

        transform.position = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
    }
}
