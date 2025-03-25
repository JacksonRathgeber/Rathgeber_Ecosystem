using UnityEngine;

public class Zebra_Script : MonoBehaviour
{
    public GameObject herd;
    public float move_dist;
    public float move_chance;
    public float move_lerp;
    public float follow_lerp = 0.75f;

    public Rigidbody2D rb;
    public Vector3 destination;

    public enum State {Chill, Running, Mating}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        herd = GameObject.FindWithTag("Herd");
        destination = new Vector3(this.transform.position.x + Random.Range(-move_dist, move_dist), 
            this.transform.position.y + Random.Range(-move_dist, move_dist), 0);
        move_dist = herd.GetComponent<Zebra_Herd_Script>().move_dist;
        move_chance = herd.GetComponent<Zebra_Herd_Script>().move_chance;
        move_lerp = herd.GetComponent<Zebra_Herd_Script>().lerp_amount;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check_For_Lions();
        Move();
    }

    public void Move()
    {
        float rand_val = Random.value;
        float move_x = 0;
        float move_y = 0;

        if (rand_val > move_chance)
        {
            move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                herd.transform.position.x - this.transform.position.x, follow_lerp);
            move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                herd.transform.position.y - this.transform.position.y, follow_lerp);

            destination = new Vector3(this.transform.position.x + move_x,
                this.transform.position.y + move_y, 0);
        }

        //transform.position = Vector3.Lerp(this.transform.position, destination, move_lerp);
        rb.AddForce(new Vector2(move_x * 40, move_y * 40));
        rb.rotation = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;//Quaternion.LookRotation(rb.linearVelocity);
    }

    public void Check_For_Lions()
    {   
        Debug.DrawRay(this.transform.position, transform.up * 20);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
    }
}
