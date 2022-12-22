using UnityEngine;

public class Box : MonoBehaviour
{
    public bool OverGround { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        OverGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        OverGround = false;
    }
}
