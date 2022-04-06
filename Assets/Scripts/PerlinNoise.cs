using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField, Range(128, 512), Tooltip("Texture width in pixels.")]
    private int width = 256;
    
    [SerializeField, Range(128, 512), Tooltip("Texture height in pixels.")]
    private int height = 256;

    [SerializeField, Range(1f, 100f), Tooltip("Texture height in pixels.")]
    private float scale = 20f;

    private float textureOffsetX = 100f;
    private float textureOffsetY = 100f;

    private void Awake() {
        textureOffsetX = Random.Range(0.0f, 99999f);
        textureOffsetY = Random.Range(0.0f, 99999f);
    }

    private void Start() {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture() {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    public Color CalculateColor (int x, int y){
        float xPerlinCoord = (float)x / width * scale + textureOffsetX;
        float yPerlinCoord = (float)y / height * scale + textureOffsetY;

        float colorSample = Mathf.PerlinNoise(xPerlinCoord, yPerlinCoord);
        return new Color( colorSample, colorSample, colorSample );
    }

}
