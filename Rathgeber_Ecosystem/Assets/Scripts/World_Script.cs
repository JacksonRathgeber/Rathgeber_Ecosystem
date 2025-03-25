using UnityEngine;

public class World_Script : MonoBehaviour
{
    public static World_Script reference;
    public Camera ortho_cam;
    public GameObject lion;
    public GameObject zebra;
    public GameObject fly;


    void Awake()
    {
        reference = this;

    }

    /*
    TO DO LIST:
    
    ZEBRAS:
    - Lion detection with layer masking
    - Fleeing (herd + individuals) and re-calm condition
    - Time-based lifespan + death

    LIONS:
    - Zebra detection with layer masking
    - Chasing (pack + individuals) and re-calm condition
    - What happens when lion catches zebra?
    - Hunger-based lifespan + death

    FLIES:
    - Lion + zebra detection with layer masking
    - Following animals + re-calm condition
    - Sucking animal blood (reduce lifespan?)
    - Swarm-size-based lifespan + death (?)
    
    TRANSFERABLE:
    - Crank numbers up
    - Sex update
    - Respawn if group wipe
    - Lifespan framework
    - Eyesight + detection
    - Constrain herds to bounding box
    */
}
