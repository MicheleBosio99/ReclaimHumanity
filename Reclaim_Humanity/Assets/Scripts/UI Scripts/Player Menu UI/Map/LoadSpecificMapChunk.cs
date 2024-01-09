using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpecificMapChunk : MonoBehaviour {
    
    [SerializeField] private Texture2D mapTexture;
    [SerializeField] private Image mapImageUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPositionPin;
    private RectTransform pinRectTransform;
    
    [SerializeField] private MapParameters mapParameter;
    

    private void Awake() { pinRectTransform = playerPositionPin.GetComponent<RectTransform>(); }

    private void OnEnable() { ApplyTextureToUI(player.transform.position); }

    private void ApplyTextureToUI(Vector2 playerPosition) {
        var mapTextureRatioX = mapParameter.originalTextureWidth / mapParameter.mapWidthInSquares;
        var mapTextureRatioY = mapParameter.originalTextureHeight / mapParameter.mapHeightInSquares;
        
        var texturePlayerPositionX = playerPosition.x * mapTextureRatioX + mapParameter.textureOffsetX;
        var texturePlayerPositionY = playerPosition.y * mapTextureRatioY + mapParameter.textureOffsetY;

        var lbPositionVector = new Vector2(texturePlayerPositionX - mapParameter.displayedImageWidth / 2.0f,
            texturePlayerPositionY - mapParameter.displayedImageHeight / 2.0f);
        
        lbPositionVector = ChangePinPosition(lbPositionVector);

        var chunkDim = new Vector2(mapParameter.displayedImageWidth, mapParameter.displayedImageHeight);
        
        var chunk = new Rect(lbPositionVector, chunkDim);
        
        mapImageUI.sprite = Sprite.Create(mapTexture, chunk, new Vector2(0.5f, 0.5f));
    }

    private Vector2 ChangePinPosition(Vector2 lbPositionVector) {
        var pinPosition = new Vector2(0.0f, 0.0f);
        
        if(lbPositionVector.x < 0.0f) { pinPosition.x = lbPositionVector.x; lbPositionVector.x = 0.0f; }
        else if(lbPositionVector.x + mapParameter.displayedImageWidth > mapParameter.originalTextureWidth) {
            pinPosition.x = lbPositionVector.x + mapParameter.displayedImageWidth - mapParameter.originalTextureWidth;
            lbPositionVector.x = mapParameter.originalTextureWidth - mapParameter.displayedImageWidth;
        }
        
        if(lbPositionVector.y < 0.0f) { pinPosition.y = lbPositionVector.y; lbPositionVector.y = 0.0f; }
        else if(lbPositionVector.y + mapParameter.displayedImageHeight > mapParameter.originalTextureHeight) {
            pinPosition.y = lbPositionVector.y + mapParameter.displayedImageHeight - mapParameter.originalTextureHeight;
            lbPositionVector.y = mapParameter.originalTextureHeight - mapParameter.displayedImageHeight;
        }
        
        pinPosition.x *= 1.5f;
        pinPosition.y *= 1.5f;
        
        pinRectTransform.anchoredPosition = pinPosition;
        
        return lbPositionVector;
    }
}

class NameParamBinding {
    public string sceneName;
    public MapParameters mapParameters;
}