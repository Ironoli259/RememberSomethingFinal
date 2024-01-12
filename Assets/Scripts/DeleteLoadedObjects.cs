using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLoadedObjects : MonoBehaviour
{
    private void Start()
    {
        Destroy(GameObject.Find("Timer"));
        Destroy(GameObject.Find("ArcadeManager"));
    }
}
