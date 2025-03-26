using UnityEngine;

public class Lion_Script : MonoBehaviour
{
    public GameObject pack;
    public float move_dist;
    public float move_chance;
    public float follow_lerp = 0.75f;
    private float move_force = 50f;

    public Rigidbody2D rb;
    public Vector3 destination;

    public enum State { Chill, Hunting, Mating }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pack = GameObject.FindWithTag("Pack");

        move_dist = pack.GetComponent<Lion_Pack_Script>().move_dist;
        move_chance = pack.GetComponent<Lion_Pack_Script>().move_chance;

        float move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                pack.transform.position.x - this.transform.position.x, follow_lerp);
        float move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
            pack.transform.position.y - this.transform.position.y, follow_lerp);

        destination = new Vector3(this.transform.position.x + move_x,
            this.transform.position.y + move_y, 0);

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

        if (rand_val > move_chance)
        {
            move_x = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                pack.transform.position.x - this.transform.position.x, follow_lerp);
            move_y = Mathf.Lerp(Random.Range(-move_dist, move_dist),
                pack.transform.position.y - this.transform.position.y, follow_lerp);

            destination = new Vector3(this.transform.position.x + move_x,
                this.transform.position.y + move_y, 0);
        }

        //transform.position = Vector3.Lerp(this.transform.position, destination, move_lerp);
        rb.AddForce(new Vector2(move_x, move_y) * move_force);
        rb.rotation = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;//Quaternion.LookRotation(rb.linearVelocity);
    }

    public void Check_For_Zebras()
    {
        Debug.DrawRay(this.transform.position, transform.up * 20);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10, Vector2.up);


        if (hit && hit.collider.gameObject.tag == "Zebra")
        {
            //Debug.Log("A Lion spotted a zebra!");
        }
    }
}
