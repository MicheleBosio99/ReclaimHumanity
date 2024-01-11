using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        Enemy enemy = (Enemy)target;
        Vector3 enPos = enemy.transform.position;
        
        Handles.color = Color.red;
        Handles.DrawSolidArc(enPos, Vector3.forward, enemy.FOVDirection(), enemy.angle, enemy.radius);
        Handles.DrawSolidArc(enPos, Vector3.forward, enemy.FOVDirection(), -enemy.angle, enemy.radius);

        Vector3 viewAngle1 = DirectionFromAngle(enemy.transform.eulerAngles.y, -enemy.angle);
        Vector3 viewAngle2 = DirectionFromAngle(enemy.transform.eulerAngles.y, enemy.angle);

        Handles.color = Color.yellow;
        Handles.DrawLine(enPos, enPos + viewAngle1 * enemy.radius);
        Handles.DrawLine(enPos, enPos + viewAngle2 * enemy.radius);

    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}