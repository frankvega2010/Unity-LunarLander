using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject levelBounds;
    public GameObject windZonePrefab;
    public GameObject[] windZones;
    public int points;
    public int cameraBoundsLimit;
    public Vector2[] pointsPositions;
    public Vector2[] levelBoundsPositions;
    public float minRangeX; //  1.5
    public float maxRangeX; //  6.0
    public float minRangeY; //  0.0
    public float maxRangeY; // 16.0
    
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private EdgeCollider2D levelBoundsCollider;
    private int randomChance;
    private float randomPositionX;
    private bool switchWinds;
    
    // Start is called before the first frame update
    void Start()
    {
        switchWinds = true;
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        levelBoundsCollider = levelBounds.GetComponent<EdgeCollider2D>();

        lineRenderer.positionCount = points;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if(i != 0)
            {
                randomChance = Random.Range(1,11);
                randomPositionX = lineRenderer.GetPosition(i - 1).x + Random.Range(minRangeX, maxRangeX);
                switch (randomChance)
                {
                    case 1:
                        lineRenderer.SetPosition(i, new Vector3(randomPositionX, lineRenderer.GetPosition(i - 1).y, 0));
                        break;
                    case 4:
                        lineRenderer.SetPosition(i, new Vector3(randomPositionX, lineRenderer.GetPosition(i - 1).y, 0));
                        break;
                    case 5:
                        lineRenderer.SetPosition(i, new Vector3(randomPositionX, lineRenderer.GetPosition(i - 1).y, 0));
                        break;
                    case 7:
                        lineRenderer.SetPosition(i, new Vector3(randomPositionX, lineRenderer.GetPosition(i - 1).y, 0));
                        break;
                    default:
                        lineRenderer.SetPosition(i, new Vector3(randomPositionX, Random.Range(minRangeY, maxRangeY), 0));
                        break;
                }
            }
            else
            {
                lineRenderer.SetPosition(i, new Vector3(0, Random.Range(minRangeY, maxRangeY), 0));
            }

            
        }

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            pointsPositions[i] = new Vector2(lineRenderer.GetPosition(i).x, lineRenderer.GetPosition(i).y);
        }

        levelBoundsPositions[0] = new Vector2(pointsPositions[cameraBoundsLimit].x, minRangeY);
        levelBoundsPositions[1] = new Vector2(pointsPositions[points- cameraBoundsLimit].x, minRangeY);
        levelBoundsPositions[2] = new Vector2(pointsPositions[points- cameraBoundsLimit].x, minRangeY + 24);
        levelBoundsPositions[3] = new Vector2(pointsPositions[cameraBoundsLimit].x, minRangeY + 24);
        levelBoundsPositions[4] = new Vector2(pointsPositions[cameraBoundsLimit].x, minRangeY);

        edgeCollider.points = pointsPositions;
        levelBoundsCollider.points = levelBoundsPositions;

        for (int i = 0; i < windZones.Length; i++)
        {
            GameObject windZoneInstance = Instantiate(windZonePrefab);
            windZones[i] = windZoneInstance;
            windZones[i].transform.position = new Vector3(Random.Range(levelBoundsPositions[0].x, levelBoundsPositions[1].x) + i * 15, windZones[i].transform.position.y, windZones[i].transform.position.z);
            windZones[i].SetActive(true);
        }

        UIDisplayCheatScreen.OnCheatSwitchWindZones += CheatSwitchWindZones;
    }

    private void CheatSwitchWindZones()
    {
        switchWinds = !switchWinds;

        for (int i = 0; i < windZones.Length; i++)
        {
            windZones[i].SetActive(switchWinds);
        }
    }

    private void OnDestroy()
    {
        UIDisplayCheatScreen.OnCheatSwitchWindZones -= CheatSwitchWindZones;
    }
}
