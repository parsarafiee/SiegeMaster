using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tantacle : MonoBehaviour
{
    public GameObject TheObjectToConnect;
    public int length;
    public LineRenderer LineRenderer;
    public Vector3[] segmentPoses;

    private Vector3[] segmentV;

    public Transform targetDir;

    public float targetDist;

    public float smoothSpeed;
    public float trailSpeed;

    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    bool firstUpdate = false;
    float timer;

    private void Start()
    {
        LineRenderer.GetComponent<LineRenderer>();
        LineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];

        firstUpdate = true;
        //LineRenderer.enabled = false;
    }

    public void Update()
    {
        //timer += Time.deltaTime;

        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(wiggleSpeed * Time.time) * wiggleMagnitude);
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed + 1 / trailSpeed);
        }

        LineRenderer.SetPositions(segmentPoses);
    }
}