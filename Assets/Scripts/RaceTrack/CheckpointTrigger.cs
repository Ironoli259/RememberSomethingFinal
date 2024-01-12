using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public CheckpointManager checkpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached");
            checkpointManager.CheckpointReached();
            Destroy(transform.parent.gameObject);
        }
    }
}
