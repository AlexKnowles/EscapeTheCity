using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldBuildingPlacer : MonoBehaviour
{
    public GameObject[] Buildings;

    private GameObject _container;

    public void PlaceBuildings(WorldGenerator worldGenerator)
    {
        SetupContainer();

        var possibleSpawns = worldGenerator.AllTiles.Where(w => w.Value.Type == WorldTileType.Pavement).ToList();
        List<Vector2> usedCoords = new List<Vector2>();
        List<Vector3> buildingWorldSpawnPoints = new List<Vector3>();

        foreach (var possibleSpawn in possibleSpawns)
        {
            if(usedCoords.Any(a => a == possibleSpawn.Key)
                || usedCoords.Any(a => a == possibleSpawn.Key + Vector2.right)
                || usedCoords.Any(a => a == possibleSpawn.Key + Vector2.down)
                || usedCoords.Any(a => a == possibleSpawn.Key + Vector2.down + Vector2.right))
            {
                continue;
            }

            if(possibleSpawns.Any(a => a.Key == possibleSpawn.Key + Vector2.right)
                && possibleSpawns.Any(a => a.Key == possibleSpawn.Key + Vector2.down)
                && possibleSpawns.Any(a => a.Key == possibleSpawn.Key + Vector2.down + Vector2.right))
            {
                buildingWorldSpawnPoints.Add(new((possibleSpawn.Key.x * 2) + 1f, 0f, (possibleSpawn.Key.y * 2) + 1f));

                usedCoords.Add(possibleSpawn.Key);
                usedCoords.Add(possibleSpawn.Key + Vector2.right);
                usedCoords.Add(possibleSpawn.Key + Vector2.down);
                usedCoords.Add(possibleSpawn.Key + Vector2.down + Vector2.right);
            }
        }

        foreach(Vector3 spawn in buildingWorldSpawnPoints)
        {
            int randomIndex = Random.Range(0, Buildings.Length);

            GameObject building = Instantiate(Buildings[randomIndex], _container.transform);
            building.transform.position = spawn;
        }
    }

    private void SetupContainer()
    {
        if (_container is not null)
        {
            DestroyImmediate(_container);
            _container = null;
        }

        _container = new GameObject("buildings");
        _container.transform.parent = transform;
    }
}
