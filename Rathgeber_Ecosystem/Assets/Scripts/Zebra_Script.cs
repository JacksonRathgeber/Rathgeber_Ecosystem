using UnityEngine;

public class Zebra_Script : MonoBehaviour
{
    public GameObject herd;
    public GameObject lion_pack;
    public float move_dist;
    public float move_chance;
    public float follow_lerp = 0.75f;
    public float move_force = 1f;

    public Rigidbody2D rb;
    public Vector3 destination;

    public enum State {Chill, Fleeing, Mating}
    public State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        herd = GameObject.FindWithTag("Herd");
        lion_pack = GameObject.FindWithTag("Pack");

        move_dist = herd.GetComponent<Zebra_Herd_Script>().move_dist;
        move_chance = herd.GetComponent<Zebra_Herd_Script>().move_chance;

        float move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                herd.transform.position.x - this.transform.position.x, follow_lerp);
        float move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
            herd.transform.position.y - this.transform.position.y, follow_lerp);

        destination = new Vector3(move_x, move_y, 0);

        rb = GetComponent<Rigidbody2D>();

        state = State.Fleeing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check_For_Lions();
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
                        herd.transform.position.x - this.transform.position.x, follow_lerp);
                    move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        herd.transform.position.y - this.transform.position.y, follow_lerp);

                    //destination = new Vector3(this.transform.position.x + move_x, this.transform.position.y + move_y, 0);
                    destination = new Vector3(move_x, move_y, 0);
                }

                if (herd.GetComponent<Zebra_Herd_Script>().state == Zebra_Herd_Script.State.Fleeing)
                {
                    state = State.Fleeing;
                }

                break;

            case State.Fleeing:

                if (rand_val > 1 - ((1 - move_chance) * 4))
                {
                    move_x = Mathf.Lerp(Random.Range(-move_dist*2, move_dist*2),
                        herd.transform.position.x - this.transform.position.x, 1 - ((1 - follow_lerp) / 2));
                    move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        herd.transform.position.y - this.transform.position.y, 1 - ((1 - follow_lerp) / 2));
                }

                //destination = new Vector3(this.transform.position.x + move_x, this.transform.position.y + move_y, 0);
                destination = new Vector3(move_x, move_y, 0);
                destination = Vector3.Lerp(destination, Vector3.MoveTowards(destination, lion_pack.transform.position, move_force), 0.1f);

                break;
        }        
        rb.AddForce(new Vector2(destination.x, destination.y) * move_force);
        rb.rotation = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;//Quaternion.LookRotation(rb.linearVelocity);
    }

    public void Check_For_Lions()
    {   
        Debug.DrawRay(this.transform.position, transform.up * 20);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10, Vector2.up);

        
        if(hit && hit.collider.gameObject.tag == "Lion")
        {
            if (herd.GetComponent<Zebra_Herd_Script>().state == Zebra_Herd_Script.State.Chill)
            {
                herd.GetComponent<Zebra_Herd_Script>().state = Zebra_Herd_Script.State.Fleeing;
            }
        }
     
    }
}
