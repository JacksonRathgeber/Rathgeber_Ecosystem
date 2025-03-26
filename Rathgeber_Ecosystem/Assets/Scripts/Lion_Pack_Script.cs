using UnityEngine;

public class Lion_Pack_Script : MonoBehaviour
{
    public GameObject lion;
    public float move_dist;
    public float move_chance;
    public float lerp_amount;
    public int min;
    public int max;
    public int spawn_range;

    public Vector2 destination;

    public enum State { Chill, Hunting, Mating }

    void Awake()
    {
        SpawnLions();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        if (rand_val > move_chance)
        {
            destination = new Vector2(this.transform.position.x + Random.Range(-move_dist, move_dist), this.transform.position.y + Random.Range(-move_dist, move_dist));
            destination = Vector2.Lerp(destination, Vector2.zero, 0.1f);
        }

        this.transform.position = Vector2.Lerp(this.transform.position, destination, lerp_amount);
    }

    public void SpawnLions()
    {
        int count = Random.Range(min, max);

        for (int i = 0; i < count; i++)
        {
            Vector2 spawn_pos = new Vector2(this.transform.position.x + Random.Range(-spawn_range, spawn_range),
                this.transform.position.y + Random.Range(-spawn_range, spawn_range));

            GameObject zeb = Instantiate(lion, spawn_pos, Quaternion.identity);
        }
    }
}
