using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private float _spawnTime = 0.04f;

    private List<CubeColor> _cubes;

    private void Start()
    {
        _cubes = new();
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        for (int x = 19; x>= 0; x--)
        {
            for (int z = 0; z < 20; z++)
            {
                var cellWorldPos = _grid.GetCellCenterWorld(new Vector3Int(z-11, 0, x-11));
                //var cube = Instantiate(_prefabCube, cellWorldPos, Quaternion.identity);
                var cube = Instantiate(_prefabCube);
                cube.transform.position = cellWorldPos + new Vector3(0, 0.15f);
                _cubes.Add(cube.GetComponent<CubeColor>());
                yield return new WaitForSeconds(_spawnTime);
            }
        }
    }

    public void ChangeColor()
    {
        
    }

    private IEnumerator ChangeColorCoroutine()
    {
        yield break;
    }

    private void OnDrawGizmos()
    {
        var cellSize = _grid.cellSize;
        var cellGap = _grid.cellGap;
        var origin = _grid.transform.position +new Vector3(-11f,0,-11f);

        Gizmos.color = Color.red;

        for (int x = 0; x < 20; x++)
        {
            for (int z = 0; z < 20; z++)
            {
                var pos = origin + new Vector3(x * (cellSize.x + cellGap.x), 0, z * (cellSize.z + cellGap.z));

                Gizmos.DrawLine(pos, pos + new Vector3(cellSize.x, 0, 0));
                Gizmos.DrawLine(pos, pos + new Vector3(0, 0, cellSize.z));
                Gizmos.DrawLine(pos + new Vector3(cellSize.x, 0, 0), pos + new Vector3(cellSize.x, 0, cellSize.z));
                Gizmos.DrawLine(pos + new Vector3(0, 0, cellSize.z), pos + new Vector3(cellSize.x, 0, cellSize.z));

            }
        }
    }

}
