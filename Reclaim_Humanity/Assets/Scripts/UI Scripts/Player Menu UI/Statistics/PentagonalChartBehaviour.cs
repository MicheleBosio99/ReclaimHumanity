using System;
using System.Collections.Generic;
using UnityEngine;

public class PentagonalChartBehaviour : MonoBehaviour {
    
    [SerializeField] private Material meshMaterial;
    [SerializeField] private GameObject radarMesh;
    private CanvasRenderer radarMeshCanvasRenderer;
    
    private const float pentagonSideLength = 50.0f;
    private const float maxStats = 20;
    private const int numberOfVertex = 5;
    
    [SerializeField] private Color centerColor;
    [SerializeField] private Color outerColor;
    [SerializeField] private const float alphaLevel = 0.9f;


    private void Awake() { radarMeshCanvasRenderer = radarMesh.GetComponent<CanvasRenderer>(); }

    private void OnEnable() { if(GameManager.party != null) { UpdateStatsVisual(); } }

    private void UpdateStatsVisual() {
        // For now is only Wollo

        var mesh = new Mesh();
        var wolloStats = new List<int>() {
            GameManager.party[0].Attack,
            GameManager.party[0].Defense,
            GameManager.party[0].SpecialAttack,
            GameManager.party[0].SpecialDefense,
            GameManager.party[0].Speed / 10
        };

        var vertices = new Vector3[numberOfVertex + 1];
        var uv = new Vector2[numberOfVertex + 1];
        var triangles = new int[3 * (numberOfVertex)];
        var colors = new Color[numberOfVertex + 1];

        vertices[0] = Vector3.zero;

        for (var i = 0; i < wolloStats.Count; i++) {
            vertices[i + 1] = ComputeCoordinate(i, wolloStats[i]);
        }

        for (var i = 1; i < numberOfVertex + 1; i++) {
            var nextIndex = (i == numberOfVertex) ? 1 : i + 1;

            triangles[(i - 1) * 3] = 0;
            triangles[(i - 1) * 3 + 1] = i;
            triangles[(i - 1) * 3 + 2] = nextIndex;
        }
        
        for (var i = 0; i < numberOfVertex + 1; i++) {
            uv[i] = new Vector2(vertices[i].x / pentagonSideLength, vertices[i].y / pentagonSideLength);
        }
        
        for (var i = 0; i < vertices.Length; i++) {
            var distance = Vector3.Distance(Vector2.zero, vertices[i]);
            var normalizedDistance = distance / pentagonSideLength;
            
            colors[i] = Color.Lerp(centerColor, outerColor, normalizedDistance);
            colors[i].a = alphaLevel;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.colors = colors;
        
        radarMeshCanvasRenderer.SetMesh(mesh);
        radarMeshCanvasRenderer.SetMaterial(meshMaterial, null);
    }

    private Vector3 ComputeCoordinate(int vertex, int stat) {
        var angle = (vertex * (360f / 5) + 90.0f) * Mathf.Deg2Rad;
        var length = (stat / maxStats) * pentagonSideLength;
        return new Vector3(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length, 0f);
    }
}
