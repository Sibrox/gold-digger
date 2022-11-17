using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animation
{
    DOWNBREAK = 1,
    UPBREAK = 2,
    LEFTBREAK = 3,
    RIGHTBREAK = 4,
    MOVELEFT = 5,
    MOVERIGHT = 6,
}
public class Player : MonoBehaviour
{
    public int row, col;
    public double score, stunDuration, caosDuration;
    public bool isMoving, stopped, stunned, confused;
    public Animator animator;
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
        int nBlock = 0;
        foreach (var blockType in playerItems)
        {
            nBlock += blockType.Value;
        }
        return nBlock;
    }

    public string TotalBlockDiggedDebug()
    {
        return playerItems.ToString();
    }

    public void BreakLeft()
    {
        var offset = confused ? 1 : -1;
        var broken = map.Break(row, col + offset);

        //AnimationHandeler(broken, (int)Direction.LEFT);

        if (broken > 0)
        {
            isMoving = true;
            col += offset;
        }
    }

    public void BreakRight()
    {
        var offset = confused ? -1 : 1;
        var broken = map.Break(row, col + offset);

        //AnimationHandeler(broken, (int)Direction.RIGHT);

        if (broken > 0)
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
        //AnimationHandeler(broken,Direction.DOWN);
        if (broken > 0 && status_before == false)
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
        //AnimationHandeler(broken,Direction.UP);
        if (broken > 0 && status_before)
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

    private void AnimationHandeler(int action, int direction)
    {
        //0 Picconata verso direzione nessun movimento.
        //1 Picconata e poi movimento verso direzione.
        //2 Nessuna picconata, movimento verso direzione.

        //Tocca comprendere come gestire il non fare un movimento se già si sta muovendo.

        switch (direction)
        {
            case (int)Direction.LEFT:

                if (action == 0)
                {
                    animator.SetInteger("miningDirection", (int)Animation.LEFTBREAK);
                }
                else if (action == 1)
                {
                    animator.SetInteger("miningDirection", (int)Animation.LEFTBREAK);

                    while (animator.GetCurrentAnimatorStateInfo(0).IsName("player_break_left"))
                    {

                    }

                    animator.SetInteger("miningDirection", (int)Animation.MOVELEFT);
                }
                else
                {
                    animator.SetInteger("miningDirection", (int)Animation.MOVELEFT);
                }

                break;
            case (int)Direction.RIGHT:

                if (action == 0)
                {
                    animator.SetInteger("miningDirection", (int)Animation.RIGHTBREAK);
                }
                else if (action == 1)
                {
                    animator.SetInteger("miningDirection", (int)Animation.RIGHTBREAK);

                    while (animator.GetCurrentAnimatorStateInfo(0).IsName("player_break_right"))
                    {

                    }

                    animator.SetInteger("miningDirection", (int)Animation.MOVERIGHT);
                }
                else
                {
                    animator.SetInteger("miningDirection", (int)Animation.MOVERIGHT);
                }

                break;
            case (int)Direction.UP:

                if (action == 1)
                {
                    animator.SetInteger("miningDirection", (int)Animation.UPBREAK);
                }
                break;
            case (int)Direction.DOWN:

                animator.SetInteger("miningDirection", (int)Animation.DOWNBREAK);
                break;
        }
    }
}
