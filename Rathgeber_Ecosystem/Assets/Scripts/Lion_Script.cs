using UnityEngine;
using static Lion_Script;

public class Lion_Script : MonoBehaviour
{
    public GameObject pack;
    public GameObject zebra_herd;
    public float move_dist;
    public float move_chance;
    private float follow_lerp = 0.8f;
    private float move_force = 50f;
    private int max_speed = 20;

    public Rigidbody2D rb;
    public Vector3 destination;

    public enum State { Chill, Hunting, Mating }
    public State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pack = GameObject.FindWithTag("Pack");
        zebra_herd = GameObject.FindWithTag("Herd");

        move_dist = pack.GetComponent<Lion_Pack_Script>().move_dist;
        move_chance = pack.GetComponent<Lion_Pack_Script>().move_chance;

        float move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                pack.transform.position.x - this.transform.position.x, follow_lerp);
        float move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
            pack.transform.position.y - this.transform.position.y, follow_lerp);

        destination = new Vector3(move_x, move_y, 0);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check_For_Zebras();
        Move();
    }

    private void Move()
    {
        float rand_val = Random.value;
        float move_x = 0;
        float move_y = 0;

        switch (state) {
            
            case State.Chill:
                if (rand_val > move_chance)
                {
                    move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        pack.transform.position.x - this.transform.position.x, follow_lerp);
                    move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        pack.transform.position.y - this.transform.position.y, follow_lerp);

                    destination = new Vector3(move_x, move_y, 0);
                }

                if (pack.GetComponent<Lion_Pack_Script>().state == Lion_Pack_Script.State.Hunting)
                {
                    state = State.Hunting;
                }

                break;

            case State.Hunting:

                if (rand_val > 1 - ((1 - move_chance) * 4))
                {
                    move_x = Mathf.Lerp(Random.Range(-move_dist * 2, move_dist * 2),
                        pack.transform.position.x - this.transform.position.x, 1 - ((1 - follow_lerp) / 2));
                    move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                        pack.transform.position.y - this.transform.position.y, 1 - ((1 - follow_lerp) / 2));
                }

                //destination = new Vector3(this.transform.position.x + move_x, this.transform.position.y + move_y, 0);
                destination = new Vector3(move_x, move_y, 0);
                break;
        }

        //transform.position = Vector3.Lerp(this.transform.position, destination, move_lerp);
        rb.AddForce(new Vector2(destination.x, destination.y) * move_force);
        rb.rotation = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;//Quaternion.LookRotation(rb.linearVelocity);

        Debug.DrawRay(this.transform.position, this.transform.up * Vector3.Distance(this.transform.position, destination));

        destination = destination.normalized * Mathf.Min(destination.magnitude, max_speed);
    }

    public void Check_For_Zebras()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10, Vector2.up);


        if (hit && hit.collider.gameObject.tag == "Zebra")
        {
            if (pack.GetComponent<Lion_Pack_Script>().state == Lion_Pack_Script.State.Chill)
            {
                pack.GetComponent<Lion_Pack_Script>().state = Lion_Pack_Script.State.Hunting;
                Debug.Log("HUNTING TIME");
            }
        }
    }
}
