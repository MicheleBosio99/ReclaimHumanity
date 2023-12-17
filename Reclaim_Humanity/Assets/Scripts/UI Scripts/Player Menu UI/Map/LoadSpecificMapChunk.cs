using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpecificMapChunk : MonoBehaviour {
    
    [SerializeField] private Texture2D mapTexture;
    [SerializeField] private Image mapImageUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPositionPin;
    private RectTransform pinRectTransform;

    private const float mapWidthInSquares = 128.0f;
    private const float mapHeightInSquares = 128.0f;
    
    private const float originalTextureWidth = 1084.0f;
    private const float originalTextureHeight = 1092.0f;

    private const float displayedImageWidth = 564.0f;
    private const float displayedImageHeight = 282.0f;
    
    private const float textureOffsetX = 542.0f;
    private const float textureOffsetY = 40.0f;

    private void Awake() { pinRectTransform = playerPositionPin.GetComponent<RectTransform>(); }

    private void OnEnable() { ApplyTextureToUI(player.transform.position); }

    private void ApplyTextureToUI(Vector2 playerPosition) {
        var mapTextureRatioX = originalTextureWidth / mapWidthInSquares;
        var mapTextureRatioY = originalTextureHeight / mapHeightInSquares;
        
        var texturePlayerPositionX = playerPosition.x * mapTextureRatioX + textureOffsetX;
        var texturePlayerPositionY = playerPosition.y * mapTextureRatioY + textureOffsetY;
        
        var lbPositionVector = new Vector2(texturePlayerPositionX - displayedImageWidth / 2.0f,
            texturePlayerPositionY - displayedImageHeight / 2.0f);
        
        // lbPositionVector = ChangePinPosition(lbPositionVector);

        var chunkDim = new Vector2(displayedImageWidth, displayedImageHeight);
        
        var chunk = new Rect(lbPositionVector, chunkDim);
        // Debug.Log($"Chunk {chunk}");
        
        mapImageUI.sprite = Sprite.Create(mapTexture, chunk, new Vector2(0.5f, 0.5f));
    }

    private Vector2 ChangePinPosition(Vector2 lbPositionVector) {
        var vectorPositionPin = new Vector2();
        
        if (lbPositionVector.x < 0.0f) { vectorPositionPin.x = lbPositionVector.x; lbPositionVector.x = 0.0f; }
        else if (lbPositionVector.x + displayedImageWidth >= originalTextureWidth) {
            vectorPositionPin.x = lbPositionVector.x + displayedImageWidth - originalTextureWidth;
            lbPositionVector.x = originalTextureWidth - displayedImageWidth;
        }
        
        if (lbPositionVector.y < 0.0f) { vectorPositionPin.y = lbPositionVector.y; lbPositionVector.y = 0.0f; }
        else if(lbPositionVector.y + displayedImageHeight >= originalTextureHeight) {
            vectorPositionPin.y = lbPositionVector.y + displayedImageHeight - originalTextureHeight;
            lbPositionVector.y = originalTextureHeight - displayedImageHeight;
        }
        
        pinRectTransform.anchoredPosition = vectorPositionPin;
        Debug.Log(vectorPositionPin);
        
        return lbPositionVector;
    }
}







