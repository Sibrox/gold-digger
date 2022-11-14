using UnityEngine;

public class GtaBlock : Block
{
    public int resistence = 5;
    private KeyCode[] pool, sequence;
    public int index = 0;
    public double score;
    public bool started;
    public int counter = 0;
    public double slowRate = 2.0;

    public override void Start()
    {

        base.Start();
        started = false;
        pool = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
        sequence = new KeyCode[resistence];

        for (var i = 0; i < resistence; i++)
        {
            sequence[i] = pool[Random.Range(0, 3)];
        }
    }

    void Update()
    {

        if (started == false) return;

        score -= Time.deltaTime / slowRate;

        if (Input.anyKeyDown) { counter++; } //prende l'input dell'onTap()

        if (Input.anyKeyDown && counter >= 2)
        {
            if (Input.GetKeyDown(sequence[index]))
            {
                resistence--;
                index++;
            }
            else
            {
                Interrupt();
            }
            if (resistence == 0)
            {
                Complete();
            }
        }
    }

    public override bool OnTap()
    {
        started = true;
        score = timer.remaingTime;

        player.Stop(true);
        player.AddItemDigged(type.ToString(), value);

        timer.Reset();
        timer.SetSlowRate(slowRate);

        Debug.Log(Sequence());

        return true;
    }

    private string Sequence()
    {
        string debugString = "";

        foreach (var action in sequence)
        {
            debugString += action.ToString() + " ";
        }

        return debugString;
    }

    private void Interrupt()
    {
        started = false;
        UnityEngine.Debug.Log("SONO STUNNATO!!");
        player.Stun(2.0);
    }

    private void Complete()
    {
        started = false;
        player.Stop(false);

        timer.SetSlowRate(1.0);
        timer.Reset();

        Debug.Log("L'ho smurfata ho fatto " + score + " punti");
        //reward
        //destroy
    }
}
