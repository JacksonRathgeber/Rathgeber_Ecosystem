using UnityEngine;

public class World_Script : MonoBehaviour
{
    public static World_Script reference;
    public Camera ortho_cam;
    public GameObject lion;
    public GameObject zebra;
    public GameObject fly;

    public int octaves;
    public float persistence;
    public float lacunarity;
    public float scale;
    public int seed;

    public float[,] noiseMap;
    
    void Awake()
    {
        reference = this;
        float[,] noiseMap = CreateNoiseMap(octaves, persistence, lacunarity, scale);
        SpawnAnimals(noiseMap);
    }
    
    public float[,] CreateNoiseMap(int octaves, float persistence, float lacunarity, float scale)
    {
        int width = ortho_cam.pixelWidth;
        int height = ortho_cam.pixelHeight;

        float[,] noiseMap = new float[width, height];
        seed = Random.Range(-50000, 50000);
        Vector2[] octaveOffsets = new Vector2[octaves];
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / scale + octaveOffsets[i].x;
                    float sampleY = y / scale + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight,
                    maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }


    public void SpawnAnimals(float[,] noiseMap)
    {
        int width = ortho_cam.pixelWidth;
        int height = ortho_cam.pixelHeight;

        for (int y = height/20; y < height; y+=height/20)
        {
            for(int x = width/20; x < width; x+=width/20)
            {
                Vector3 current_world_point = new Vector3(x, y, 10);
                current_world_point = ortho_cam.ScreenToWorldPoint(current_world_point);

                float val = noiseMap[x, y];
                
                if (val < 0.2)
                {
                    Instantiate(lion, current_world_point, Quaternion.identity);
                }
                else if (val < 0.6 && val > 0.2)
                {

                }
                else if (val < 0.8)
                {
                    Instantiate(zebra, current_world_point, Quaternion.identity);
                }
                else if (val < 1)
                {
                    Instantiate(fly, current_world_point, Quaternion.identity);
                }
                
                //Debug.Log(val);

            }
        }
    }
}
