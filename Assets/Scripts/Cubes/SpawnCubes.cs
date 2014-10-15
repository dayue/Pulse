using UnityEngine;
using System.Collections;

public class SpawnCubes : MonoBehaviour {

    public GameObject iPrefab;
    public int iSize = 10;
    public int iPrefabXSize = 1;
    public int iPrefabYSize = 1;

    private GameObject mEnvironment;

    void Awake()
    {
        mEnvironment = GameObject.FindGameObjectWithTag("Environment");
    }

	// Use this for initialization
	void Start ()
    {
        if (iPrefab != null && mEnvironment != null)
        {
            int l_numCubes = GetNumberToSpawn(iSize);
            int l_width = (int)Mathf.Sqrt((float)l_numCubes);

            for (int i = 0; i < l_width; i++)
            {
                for (int j = 0; j < l_width; j++)
                {

                    Vector3 t_position = new Vector3((i * iPrefabXSize - (l_width * iPrefabXSize - 1) / 2), 0f, (j * iPrefabYSize - (l_width * iPrefabYSize - 1) / 2));
                    GameObject t_cube = (GameObject)GameObject.Instantiate(iPrefab, t_position + transform.position, Quaternion.identity);
                    t_cube.transform.parent = transform;
                    t_cube.name = iPrefab.name;
                }
            }
        }
	}

    int GetNumberToSpawn(int iLayer)
    {
        int newLayerSize = iLayer * 2 - 1;
        int numToSpawn = newLayerSize * newLayerSize;
        return numToSpawn;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
