using UnityEngine;

public class Zebra_Herd_Script : MonoBehaviour
{
    public GameObject zebra;
    public float move_dist;
    public float move_chance;
    public float lerp_amount;
    public int min;
    public int max;
    public int spawn_range;

    public Vector2 destination;

    void Awake()
    {
        SpawnZebras();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = new Vector2(this.transform.position.x + Random.Range(-move_dist, move_dist), this.transform.position.y + Random.Range(-move_dist, move_dist));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float rand_val = Random.value;

        if(rand_val > move_chance)
        {
            destination = new Vector2(this.transform.position.x + Random.Range(-move_dist, move_dist), this.transform.position.y + Random.Range(-move_dist, move_dist));
            //Debug.Log(rand_val);
        }

        this.transform.position = Vector2.Lerp(this.transform.position, destination, lerp_amount);
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
