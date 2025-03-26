using UnityEngine;

public class Zebra_Herd_Script : MonoBehaviour
{
    public GameObject zebra;
    public GameObject lion_pack;
    public float move_dist;
    public float move_chance;
    public float lerp_amount;
    public int run_speed = 40;
    public int min;
    public int max;
    public int spawn_range;

    public Vector2 destination;

    public enum State { Chill, Fleeing, Mating }
    public State state;

    void Awake()
    {
        SpawnZebras();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lion_pack = GameObject.FindWithTag("Pack");

        destination = new Vector2(this.transform.position.x + Random.Range(-move_dist, move_dist), this.transform.position.y + Random.Range(-move_dist, move_dist));
        destination = Vector2.Lerp(destination, Vector2.zero, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float rand_val = Random.value;

        switch (state)
        {
            case State.Chill:

                if (rand_val > move_chance)
                {
                    destination = new Vector2(this.transform.position.x + Random.Range(-move_dist, move_dist), this.transform.position.y + Random.Range(-move_dist, move_dist));
                    destination = Vector2.Lerp(destination, Vector2.zero, 0.1f);
                }

                this.transform.position = Vector2.Lerp(this.transform.position, destination, lerp_amount);

                break;

            case State.Fleeing:

                if (rand_val > 1 - ((1-move_chance) * 4))
                {
                    destination = Vector2.MoveTowards(transform.position, lion_pack.transform.position, -run_speed);
                    destination = Vector2.Lerp(destination, Vector2.zero, 0.1f);
                }

                if (rand_val > move_chance)
                {
                    destination = Vector2.Perpendicular(Vector2.MoveTowards(transform.position, lion_pack.transform.position, -run_speed));
                }

                if (rand_val < 1 - move_chance)
                {
                    destination = Vector2.Perpendicular(Vector2.MoveTowards(transform.position, lion_pack.transform.position, run_speed));
                }

                this.transform.position = Vector2.Lerp(this.transform.position, destination, lerp_amount);


                break;
        }
    }

    public void SpawnZebras()
    {
        int count = Random.Range(min, max);

        for(int i = 0; i < count; i++)
        {
            Vector2 spawn_pos = new Vector2(this.transform.position.x + Random.Range(-spawn_range, spawn_range),
                this.transform.position.y + Random.Range(-spawn_range, spawn_range));

            GameObject zeb = Instantiate(zebra, spawn_pos, Quaternion.identity);
        }
    }
}
