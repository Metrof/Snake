using UnityEngine;

public class Apple : MonoBehaviour
{
    public delegate void AppleTeleportation();
    public event AppleTeleportation Teleportation;
    private void OnCollisionEnter(Collision collision)
    {
        IAppleEater appleEater = collision.gameObject.GetComponent<IAppleEater>();
        if (appleEater != null)
        {
            appleEater.EatApple();
            Teleportation?.Invoke();
        }
    }
}
