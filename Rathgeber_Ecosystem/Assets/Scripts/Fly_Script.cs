using UnityEngine;

public class Fly_Script : MonoBehaviour
{
    public GameObject swarm;
    public float move_dist;
    public float move_chance;
    private float follow_lerp = 0.95f;
    private float move_force = 12;
    private int max_speed = 5;

    public Rigidbody2D rb;
    public Vector3 destination;

    public enum State { Chill, Chasing, Mating }
    public State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swarm = GameObject.FindWithTag("Swarm");

        move_dist = swarm.GetComponent<Fly_Swarm_Script>().move_dist;
        move_chance = swarm.GetComponent<Fly_Swarm_Script>().move_chance;

        float move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                swarm.transform.position.x - this.transform.position.x, follow_lerp);
        float move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
            swarm.transform.position.y - this.transform.position.y, follow_lerp);

        destination = new Vector3(move_x, move_y, 0);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float rand_val = Random.value;
        float move_x = 0;
        float move_y = 0;

        switch (state)
        {

            case State.Chill:
                if (rand_val > move_chance)
                {
                    move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        swarm.transform.position.x - this.transform.position.x, follow_lerp);
                    move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        swarm.transform.position.y - this.transform.position.y, follow_lerp);

                    destination = new Vector3(move_x, move_y, 0);

                }

                if (swarm.GetComponent<Fly_Swarm_Script>().state == Fly_Swarm_Script.State.Chasing)
                {
                    //state = State.Chasing;
                }

                break;

            case State.Chasing:
                
                /*
                if (rand_val > 1 - ((1 - move_chance) * 4))
                {
                    move_x = Mathf.Lerp(Random.Range(-move_dist * 2, move_dist * 2),
                        swarm.transform.position.x - this.transform.position.x, 1 - ((1 - follow_lerp) / 2));
                    move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        swarm.transform.position.y - this.transform.position.y, 1 - ((1 - follow_lerp) / 2));
                }

                //destination = new Vector3(this.transform.position.x + move_x, this.transform.position.y + move_y, 0);
                destination = new Vector3(move_x, move_y, 0);
                //destination = Vector3.Lerp(destination, targeted_zebra.transform.position, 0.5f);
                */
                break;
        }

        //transform.position = Vector3.Lerp(this.transform.position, destination, move_lerp);
        rb.AddForce(new Vector2(destination.x, destination.y) * move_force);
        rb.rotation = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;//Quaternion.LookRotation(rb.linearVelocity);

        //Debug.DrawRay(this.transform.position, this.transform.up * Vector3.Distance(this.transform.position, destination));

        destination = destination.normalized * Mathf.Min(destination.magnitude, max_speed);
    }
}
