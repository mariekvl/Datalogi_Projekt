using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform PlayerEnzyme;
    public float speed = 5f;

    // late update is used because it runs after all update functions (in this case after player movement)
    void LateUpdate()
    {
        if (PlayerEnzyme == null) return;
        transform.position = new Vector3(PlayerEnzyme.position.x, PlayerEnzyme.position.y, transform.position.z);
    }
}

