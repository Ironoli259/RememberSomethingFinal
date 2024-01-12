using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float throwForce = 25.0f;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float mouseSensitivity = 1.2f;
    private GameObject currentBall = null;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Repeat(yRotation, 360f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("BallRack") && currentBall == null)
                {
                    currentBall = Instantiate(ballPrefab, spawnPosition.position, spawnPosition.rotation);
                    currentBall.GetComponent<Rigidbody>().isKinematic = true;
                    currentBall.transform.SetParent(Camera.main.transform);
                    currentBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
                else if (currentBall != null)
                {
                    currentBall.transform.SetParent(null);
                    Rigidbody rb = currentBall.GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.None;
                    rb.isKinematic = false;
                    Vector3 shootingDirection = Vector3.Lerp(Camera.main.transform.forward, Camera.main.transform.up, 0.2f).normalized;
                    rb.AddForce(shootingDirection * throwForce, ForceMode.Impulse);
                    StartCoroutine(DestroyBallAfterDelay(currentBall, 5.0f));
                    currentBall = null;
                }
            }
        }
    }

    private IEnumerator DestroyBallAfterDelay(GameObject ball, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(ball);
    }
}