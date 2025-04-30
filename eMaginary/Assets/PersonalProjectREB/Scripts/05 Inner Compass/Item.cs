using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public RadarEvent dropped;
    public RadarEvent pickUp;
    public Image icon;
    int numItems = 0;

    void Start()
    {
        dropped.Occured(this.gameObject);// runs the Egg Event
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ken")
        {
            pickUp.Occured(this.gameObject);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 5);
            Debug.Log("Picked up an object " + numItems++);
        }
       
    }


}
