using UnityEngine;

/// <summary>
/// General Ammo pickup component
/// </summary>
public class Ammo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<CGun>().CurAmmo++;
            Destroy(this.gameObject);
        }
    }
}