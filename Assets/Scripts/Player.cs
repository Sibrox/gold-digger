using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int row, col;
    public double score, stunDuration, caosDuration;
    public bool isMoving, stopped, stunned, confused;

    public Map map;

    private Dictionary<string, int> playerItems;

    public float x
    {
        get { return col; }  // get method
    }

    public float y
    {
        get { return (-row); }  // get method
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (!stopped)
        {
            this.transform.position = new Vector3(x + 0.5f, y + 0.5f, -10);

            if (Input.GetKeyDown("a"))
            {
                BreakLeft();
            }
            if (Input.GetKeyDown("d"))
            {
                BreakRight();
            }
            if (Input.GetKeyDown("s"))
            {
                BreakDown();
            }
            if (Input.GetKeyDown("w"))
            {
                BreakUp();
            }
        }

        CheckStunnedState();
        CheckConfusedState();
    }

    public int TotalBlockDigged()
    {
        return playerItems.Count;
    }

    public string TotalBlockDiggedDebug()
    {
        return playerItems.ToString();
    }

    public void BreakLeft()
    {
        var offset = confused ? 1 : -1;

        var broken = map.Break(row, col + offset);
        if (broken)
        {
            isMoving = true;
            col += offset;
        }
    }

    public void BreakRight()
    {
        var offset = confused ? -1 : 1;

        var broken = map.Break(row, col + offset);
        if (broken)
        {
            isMoving = true;
            col += offset;
        }
    }

    public void BreakDown()
    {
        var offset = confused ? -1 : 1;

        // this is needed because caos block invert status
        // but we need to go down in this case.
        var status_before = confused;
        var broken = map.Break(row + offset, col);
        if (broken && status_before == false)
        {
            isMoving = true;
            row += offset;
        }
    }

    public void BreakUp()
    {
        var offset = confused ? 1 : -1;

        // this is needed because caos block invert status
        // but player can't go down in this case
        var status_before = confused;
        var broken = map.Break(row + offset, col);
        if (broken && status_before)
        {
            isMoving = true;
            row += offset;
        }
    }

    public void AddItemDigged(string item, double value)
    {
        int currentValue = playerItems.GetValueOrDefault(item);
        playerItems[item] = currentValue + 1;

        score += value;
    }

    public void Stop(bool stopped)
    {
        this.stopped = stopped;
    }

    public void Stun(double stunDuration)
    {
        stopped = true;
        stunned = true;
        this.stunDuration = stunDuration;
    }

    public void Confuse(double caosDuration)
    {
        confused = true;
        this.caosDuration = caosDuration;
    }

    private void CheckConfusedState()
    {
        if (confused == false) return;

        if (caosDuration <= 0)
        {
            confused = false;
            return;
        }

        caosDuration -= Time.deltaTime;
    }

    private void CheckStunnedState()
    {
        if (stunned == false) return;

        if (stunDuration <= 0)
        {
            stopped = stunned = false;
            return;
        }

        stunDuration -= Time.deltaTime;
    }

    public void Init()
    {
        row = 0;
        col = map.GetSize() / 2 - 1;

        score = stunDuration = caosDuration = 0;
        stopped = isMoving = stunned = confused = false;

        playerItems = new Dictionary<string, int>();
    }
}
