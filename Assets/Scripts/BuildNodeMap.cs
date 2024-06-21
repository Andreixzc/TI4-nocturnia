using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNodeMap : MonoBehaviour
{
    // map dimentions
    // (-17, 7  )-(19, 7  )
    //     |         |
    // (-17, -12) (19, -12)

    // x = 36
    // y = 19

    private int[] width = { 17, 160 };
    private int[] height = { 1, 119 };

    public IDictionary<Vector3, bool> walkablePositions = new Dictionary<Vector3, bool>();
    public IDictionary<Vector3, GameObject> nodeReference = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3, string> obstacles = new Dictionary<Vector3, string>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeNodeNetwork();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void InitializeNodeNetwork()
    {
        obstacles = getObstacles();

        for (int i = width[0]; i < width[1]; i++)
        {
            for (int j = height[0]; j < height[1]; j++)
            {
                Vector3 newPosition = new Vector3(i + 0.5f, j + 0.5f);

                if (obstacles.TryGetValue(newPosition, out _))
                {
                    walkablePositions.Add(new KeyValuePair<Vector3, bool>(newPosition, false));
                }
                else
                {
                    walkablePositions.Add(new KeyValuePair<Vector3, bool>(newPosition, true));
                }

                nodeReference.Add(newPosition, null);
            }
        }
    }
    Dictionary<Vector3, string> getObstacles()
    {
        Dictionary<Vector3, string> res = new Dictionary<Vector3, string>
        {
                        //Fences
            { new Vector3(67.5f, 58.5f), null },
            { new Vector3(67.5f, 57.5f), null },
            { new Vector3(67.5f, 56.5f), null },
            { new Vector3(67.5f, 55.5f), null },
            { new Vector3(67.5f, 54.5f), null },
            { new Vector3(67.5f, 53.5f), null },
            { new Vector3(67.5f, 52.5f), null },
            { new Vector3(67.5f, 51.5f), null },
            { new Vector3(67.5f, 50.5f), null },
            { new Vector3(67.5f, 49.5f), null },
            { new Vector3(67.5f, 48.5f), null },
            { new Vector3(67.5f, 47.5f), null },
            { new Vector3(67.5f, 46.5f), null },
            { new Vector3(67.5f, 45.5f), null },
            { new Vector3(67.5f, 44.5f), null },
            { new Vector3(67.5f, 43.5f), null },
            { new Vector3(67.5f, 42.5f), null },
            { new Vector3(67.5f, 41.5f), null },
            { new Vector3(67.5f, 40.5f), null },
            { new Vector3(67.5f, 39.5f), null },
            { new Vector3(67.5f, 38.5f), null },
            { new Vector3(67.5f, 37.5f), null },
            { new Vector3(67.5f, 36.5f), null },
            { new Vector3(67.5f, 35.5f), null },

            { new Vector3(68.5f, 35.5f), null },
            { new Vector3(68.5f, 34.5f), null },
            { new Vector3(68.5f, 33.5f), null },
            { new Vector3(68.5f, 32.5f), null },
            { new Vector3(68.5f, 31.5f), null },
            { new Vector3(68.5f, 30.5f), null },
            { new Vector3(68.5f, 29.5f), null },
            { new Vector3(68.5f, 28.5f), null },
            { new Vector3(68.5f, 27.5f), null },
            { new Vector3(68.5f, 26.5f), null },

            { new Vector3(49.5f, 35.5f), null },
            { new Vector3(49.5f, 34.5f), null },
            { new Vector3(49.5f, 33.5f), null },
            { new Vector3(49.5f, 32.5f), null },
            { new Vector3(49.5f, 31.5f), null },
            { new Vector3(49.5f, 30.5f), null },
            { new Vector3(49.5f, 29.5f), null },
            { new Vector3(49.5f, 28.5f), null },
            { new Vector3(49.5f, 27.5f), null },



            { new Vector3(50.5f, 58.5f), null },
            { new Vector3(50.5f, 57.5f), null },
            { new Vector3(50.5f, 56.5f), null },
            { new Vector3(50.5f, 55.5f), null },
            { new Vector3(50.5f, 54.5f), null },
            { new Vector3(50.5f, 53.5f), null },
            { new Vector3(50.5f, 52.5f), null },
            { new Vector3(50.5f, 51.5f), null },
            { new Vector3(50.5f, 50.5f), null },
            { new Vector3(50.5f, 49.5f), null },
            { new Vector3(50.5f, 48.5f), null },
            { new Vector3(50.5f, 47.5f), null },
            { new Vector3(50.5f, 46.5f), null },
            { new Vector3(50.5f, 45.5f), null },
            { new Vector3(50.5f, 44.5f), null },
            { new Vector3(50.5f, 43.5f), null },
            { new Vector3(50.5f, 42.5f), null },
            { new Vector3(50.5f, 41.5f), null },
            { new Vector3(50.5f, 40.5f), null },
            { new Vector3(50.5f, 39.5f), null },
            { new Vector3(50.5f, 38.5f), null },

            { new Vector3(49.5f, 26.5f), null },
            { new Vector3(50.5f, 26.5f), null },
            { new Vector3(51.5f, 26.5f), null },
            { new Vector3(52.5f, 26.5f), null },
            { new Vector3(53.5f, 26.5f), null },
            { new Vector3(54.5f, 26.5f), null },
            { new Vector3(55.5f, 26.5f), null },
            { new Vector3(56.5f, 26.5f), null },
            { new Vector3(57.5f, 26.5f), null },
            { new Vector3(58.5f, 26.5f), null },
            { new Vector3(59.5f, 26.5f), null },
            { new Vector3(60.5f, 26.5f), null },
            { new Vector3(61.5f, 26.5f), null },
            { new Vector3(62.5f, 26.5f), null },
            { new Vector3(63.5f, 26.5f), null },
            { new Vector3(64.5f, 26.5f), null },
            { new Vector3(65.5f, 26.5f), null },
            { new Vector3(66.5f, 26.5f), null },
            { new Vector3(67.5f, 26.5f), null },

          

            // TREE

            { new Vector3(53.5f, 34.5f), null },
            { new Vector3(53.5f, 33.5f), null },
            { new Vector3(53.5f, 32.5f), null },

            { new Vector3(54.5f, 34.5f), null },
            { new Vector3(54.5f, 33.5f), null },
            { new Vector3(54.5f, 32.5f), null },

            { new Vector3(55.5f, 34.5f), null },
            { new Vector3(55.5f, 33.5f), null },
            { new Vector3(55.5f, 32.5f), null },

            { new Vector3(64.5f, 34.5f), null },
            { new Vector3(64.5f, 33.5f), null },
            { new Vector3(64.5f, 32.5f), null },

            { new Vector3(65.5f, 34.5f), null },
            { new Vector3(65.5f, 33.5f), null },
            { new Vector3(65.5f, 32.5f), null },

            { new Vector3(66.5f, 34.5f), null },
            { new Vector3(66.5f, 33.5f), null },
            { new Vector3(66.5f, 32.5f), null },
        
            // bed

            { new Vector3(63.5f, 32.5f), null },

        };

        return res;
    }
}