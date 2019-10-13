using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetChild(0).GetComponent<CGun>().curAmmo++;
            Destroy(this.gameObject);
        }
    }
}