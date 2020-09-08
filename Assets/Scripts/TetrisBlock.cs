using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private float _tick = 1f; //coroutine speed
    private State _state;
    [SerializeField] private static int _maxX = 9;
    [SerializeField] private static int _maxY = 16;
    public Vector3 rotationPoint;
    private bool _placed = false;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    private SpawnTet _spawnTet;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        StartCoroutine("MoveGravitation");
        _spawnTet = FindObjectOfType<SpawnTet>();
    }

    private IEnumerator MoveGravitation()
    {
        while (_placed)
        {
            transform.position -= new Vector3(0, 1, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                this.enabled = false;
                _spawnTet.SpawnNewTetramino();
                _placed = true;

            }
            yield return new WaitForSeconds(_tick / _speed);
        }
    }

    private bool IsInsideBoundaries(int dir)
    {
        foreach (Transform children in transform)
        {
            if (dir == 1) // left move
            {
                if (children.position.x - 1 > _maxX || children.position.x - 1 < 0)
                    return false;
            }
            else if (dir == 2) // right move
            {
                if (children.position.x + 1 > _maxX || children.position.x + 1 < 0)
                    return false;
            }
        }
        return true;
    }

    void Update()
    {
        #region "This Is Bad"
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //rotate !
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }


        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                FindObjectOfType<SpawnTet>().SpawnNewTetramino();

            }
            previousTime = Time.time;
        }
        #endregion

    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
				_spawnTet.IncreaseScore();
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }




    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }

        return true;
    }
}