using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapParameters", menuName = "ScriptableObjects/SceneConstants")]
public class MapParameters : ScriptableObject {
    
    public float mapWidthInSquares;
    public float mapHeightInSquares;
    
    public float originalTextureWidth;
    public float originalTextureHeight;

    public float displayedImageWidth;
    public float displayedImageHeight;
    
    public float textureOffsetX;
    public float textureOffsetY;
}
