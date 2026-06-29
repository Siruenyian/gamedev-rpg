using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public void TeleportTo(GameObject target)
    {
        Debug.Log("husaaa");
        if (destination == null)
        {
            Debug.LogWarning("Teleport destination is not assigned.");
            return;
        }

        target.transform.position = destination.position;
    }
}