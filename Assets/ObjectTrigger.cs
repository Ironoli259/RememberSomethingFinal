using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objTriggered;
    [SerializeField] private GameObject light;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetActive(true);
            objTriggered.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetActive(false);
            objTriggered.SetActive(false);
        }
    }
}
