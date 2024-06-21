using System.Collections.Generic;
using UnityEngine;

public class PathfindingBFS : MonoBehaviour
{

    private int[] width = { 17, 160 };
    private int[] height = { 1, 119 };

    private Rigidbody2D rb;

    public IDictionary<Vector3, bool> walkablePositions = new Dictionary<Vector3, bool>();
    public IDictionary<Vector3, string> obstacles;
    IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();
    public IDictionary<Vector3, GameObject> nodeReference = new Dictionary<Vector3, GameObject>();

    IList<Vector3> path;
    bool moveCube = false, lastDirection;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        // obstacles = nodeMap.obstacles;
        // walkablePositions = nodeMap.walkablePositions;

        rb = GetComponent<Rigidbody2D>();
       
        InitializeNodeNetwork();

        path = FindShortestPath();

        moveCube = true;
        lastDirection = false; // false if not flipped; else ...
    }

    // Update is called once per frame
    void Update()
    {

        if (path != null)
        {
            if (moveCube)
            {
                float speed = 5f;
                float step = Time.deltaTime * speed;

                // set animation

                Vector3 targetPos = path[i];
                Vector3 direction = (targetPos - transform.position).normalized;

                bool flipped = direction.x < 0f;
                if (direction.x == 0f)
                    transform.rotation = Quaternion.Euler(new Vector3(0f, lastDirection ? 180f : 0f, 0f));
                else
                    transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

                lastDirection = flipped;

                // move

                transform.position = Vector3.MoveTowards(transform.position, path[i], step);

                Vector3 normalizedPositionLeft = new Vector3(Mathf.Floor(transform.position.x) + 0.5f,
                                            Mathf.Floor(transform.position.y) + 0.5f);
                Vector3 normalizedPositionRight = new Vector3(Mathf.Ceil(transform.position.x) + 0.5f,
                                            Mathf.Ceil(transform.position.y) + 0.5f);

                if (i >= 0 && (normalizedPositionLeft.Equals(path[i]) || normalizedPositionRight.Equals(path[i])))
                    i--;
                if (i < 0)
                {
                    moveCube = false;
                    nodeParents.Clear();
                    path = FindShortestPath();
                    i = path != null ? path.Count - 1 : -1;
                }
            }
        }
        else
        {
            nodeParents.Clear();
            path = FindShortestPath();
            i = path != null ? path.Count - 1 : -1;
        }
    }

    Vector3 FindShortestPathBFS(Vector3 startPosition, Vector3 goalPosition)
    {

        uint nodeVisitCount = 0;

        Queue<Vector3> queue = new Queue<Vector3>();
        HashSet<Vector3> exploredNodes = new HashSet<Vector3>();
        queue.Enqueue(startPosition);

        while (queue.Count != 0)
        {
            Vector3 currentNode = queue.Dequeue();
            nodeVisitCount++;
            if (currentNode == goalPosition)
            {
                moveCube = true;
                return currentNode;
            }

            IList<Vector3> nodes = GetWalkableNodes(currentNode);

            foreach (Vector3 node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {
                    //Mark the node as explored
                    exploredNodes.Add(node);

                    //Store a reference to the previous node
                    nodeParents.Add(node, currentNode);

                    //Add this to the queue of nodes to examine
                    queue.Enqueue(node);
                }
            }
        }
        moveCube = false;
        return startPosition;
    }

    IList<Vector3> GetWalkableNodes(Vector3 curr)
    {

        IList<Vector3> walkableNodes = new List<Vector3>();

        IList<Vector3> possibleNodes = new List<Vector3>() {
            new Vector3 (curr.x + 1, curr.y),
            new Vector3 (curr.x - 1, curr.y),
            new Vector3 (curr.x, curr.y + 1),
            new Vector3 (curr.x, curr.y - 1),
        };

        foreach (Vector3 node in possibleNodes)
        {
            if (CanMove(node))
            {
                walkableNodes.Add(node);
            }
        }

        return walkableNodes;
    }

    bool CanMove(Vector3 nextPosition)
    {
        return (walkablePositions.ContainsKey(nextPosition) && walkablePositions[nextPosition]);
    }

    IList<Vector3> FindShortestPath()
    {

        IList<Vector3> res = new List<Vector3>();
        Vector3 player;
        Vector3 tmp = GameObject.Find("Player").transform.localPosition;
 
        Vector3 thisPosNormalized = new Vector3((float)System.Math.Floor(transform.localPosition.x) + 0.5f,
                                    (float)System.Math.Floor(transform.localPosition.y) + 0.5f);
        Vector3 playerPos = new Vector3((float)System.Math.Floor(tmp.x) + 0.5f, (float)System.Math.Floor(tmp.y) + 0.5f);

        player = FindShortestPathBFS(thisPosNormalized, playerPos);

        if (player == thisPosNormalized || !nodeParents.ContainsKey(new Vector3(player.x, player.y)))
        {
            return null;
        }

        Vector3 curr = player;
        while (curr != thisPosNormalized)
        {
            res.Add(curr);
            curr = nodeParents[curr];
        }
        return res;
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
           
            //stone
            { new Vector3(63.5f, 39.5f), null },
            { new Vector3(63.5f, 37.5f), null },
            { new Vector3(64.5f, 39.5f), null },
            { new Vector3(64.5f, 38.5f), null },
            { new Vector3(64.5f, 37.5f), null },

           

        };

        return res;
    }
}