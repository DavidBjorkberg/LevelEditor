using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    internal List<GameObject> Nodes;
    int targetNodeIndex = 1;
    int intendedDir = 1;
    Vector3 direction;
    [Range(1, 15)]
    public int Speed;
    [HideInInspector]
    public float MoveTime;
    public bool Circulate;
    [HideInInspector]
    public bool MoveOnGameStart;
    private void Awake()
    {
        foreach (Transform child in GetComponent<SetChildren>().Children)
        {
            child.SetParent(transform);
        }

    }
    void Start()
    {
        NodeInitialization();
        if (MoveOnGameStart)
        {
            StartCoroutine("Move");
        }
    }
   protected void NodeInitialization()
    {
        Nodes = GetComponent<NodeSpawner>().nodes;
        if (Nodes.Count > 0)
        {
            transform.position = Nodes[0].transform.position;
            SetDirection();
        }
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (Nodes.Count > 0)
            {
                transform.Translate(direction * Time.deltaTime * Speed);
                Vector3 temp = direction;
                SetDirection();
                if (Vector3.Dot(temp, direction) == -1 || Vector3.Distance(Nodes[targetNodeIndex].transform.position, transform.position) < 0.1f)
                {
                    if (Circulate)
                    {
                        GameObject CurTarget = Nodes[targetNodeIndex - 1];
                        Nodes.Remove(CurTarget);
                        Nodes.Add(CurTarget);
                        SetDirection();
                    }
                    else
                    {
                        targetNodeIndex = NextTarget();
                        SetDirection();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    int NextTarget()
    {
        int nextTarget;
        if (targetNodeIndex + intendedDir == Nodes.Count || targetNodeIndex + intendedDir < 0)
        {
            intendedDir = intendedDir * -1;
        }
        nextTarget = targetNodeIndex + intendedDir;
        return nextTarget;
    }
    void SetDirection()
    {
        direction = (Nodes[targetNodeIndex].transform.position - transform.position).normalized;
    }
    public void StartMoving(float Alivetime)
    {
        StopCoroutine("Move");
        StartCoroutine("Move");
        if (Alivetime != 0)
        {
            Invoke("StopMoving", Alivetime);

        }
    }
    public void StopMoving()
    {
        StopCoroutine("Move");
    }

}
