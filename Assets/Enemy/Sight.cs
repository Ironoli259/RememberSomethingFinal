using System;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Material redMat;
    public Material greenMat;

    [SerializeField] private bool canSee;
    [SerializeField] private bool isDebug = false;

    private MeshFilter viewMeshFilter;
    private MeshRenderer viewMeshRenderer;
    private Mesh viewMesh;

    private void Start()
    {
        GameObject viewCone = new GameObject("ViewCone");
        viewCone.transform.parent = transform;
        viewCone.transform.localPosition = Vector3.zero;

        viewMeshFilter = viewCone.AddComponent<MeshFilter>();
        viewMeshRenderer = viewCone.AddComponent<MeshRenderer>();

        viewMeshRenderer.material = redMat;

        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private void Update()
    {
        bool targetInSight = IsTargetInSight(); // Call IsTargetInSight() once and store the result
        if (targetInSight)
            viewMeshRenderer.material = greenMat;
        else
            viewMeshRenderer.material = redMat;

        CreateFieldOfViewMesh();

    }

    public bool IsTargetInSight()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);


        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    canSee = true;
                    return true;
                }
            }
        }

        canSee = false;
        return false;
    }

    void OnDrawGizmos()
    {
        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.color = canSee ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewRadius * viewAngleA);
        Gizmos.DrawLine(transform.position, transform.position + viewRadius * viewAngleB);

        Gizmos.DrawLine(transform.position, transform.position + viewRadius * viewAngleA);
        Gizmos.DrawLine(transform.position + viewRadius * viewAngleA, transform.position + viewRadius * viewAngleB);
        Gizmos.DrawLine(transform.position + viewRadius * viewAngleB, transform.position);
    }

    Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void CreateFieldOfViewMesh()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * 2);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            Vector3 dir = DirFromAngle(angle, true);
            float distance = viewRadius;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
            {
                distance = hit.distance;
            }
            viewPoints.Add(dir * distance);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformDirection(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

}
