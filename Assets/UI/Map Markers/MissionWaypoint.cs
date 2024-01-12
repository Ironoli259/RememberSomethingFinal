using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public Image img;
    public ObjectiveChecker objectiveChecker;

    private float minX, minY, maxX, maxY;
    private Vector2 pos;


    private void Update()
    {
        if (objectiveChecker.Memory >= objectiveChecker.TargetObjectsCount)
            return;

        this.minX = this.img.GetPixelAdjustedRect().width / 2;
        this.maxX = Screen.width - this.minX;
        this.minX = this.img.GetPixelAdjustedRect().width / 2;
        this.maxX = Screen.width - this.minX;
        if (objectiveChecker.targetObjects.Count > objectiveChecker.Memory)
        {

            this.minX = this.img.GetPixelAdjustedRect().width / 2;
            this.maxX = Screen.width - this.minX;

            minY = this.img.GetPixelAdjustedRect().height / 2;
            maxY = Screen.width - this.minY;

            pos = Camera.main.WorldToScreenPoint(objectiveChecker.targetObjects[objectiveChecker.Memory].position);

            if (Vector3.Dot((objectiveChecker.targetObjects[objectiveChecker.Memory].position - transform.position), transform.forward) < 0)
            {
                //Target is behind the player
                if (pos.x < Screen.width / 2)
                    pos.x = maxX;
                else
                    pos.x = minX;
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            img.transform.position = pos;
        }
    }
}
