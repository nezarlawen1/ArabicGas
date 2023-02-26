using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPreventer : MonoBehaviour
{
    [SerializeField] private Transform _respawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetPositionAndRotation(_respawnPos.position, _respawnPos.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_respawnPos.position, 1);
    }
}
