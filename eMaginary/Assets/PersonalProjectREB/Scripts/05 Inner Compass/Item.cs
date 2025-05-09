using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public RadarEvent dropped;
    public RadarEvent pickUp;
    public Image icon;

    public Animator anim;
    public GameObject effect_PickUp;
    //public GameObject gifts_UI;
    //public GameObject icon_gift;

    void Start()
    {
        dropped.Occured(this.gameObject);// runs the Egg Event
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ken")
        {
            pickUp.Occured(this.gameObject);
            //this.GetComponent<MeshRenderer>().enabled = false;         

            //ShowUIPickUp();
            
            anim.SetTrigger("openLid");
            effect_PickUp.SetActive(true);

            //gifts_UI.SetActive(true);
            //icon_gift.SetActive(true);

            //this.GetComponent<BoxCollider>().enabled = false;

            //Destroy(this.gameObject);
            Debug.Log("Picked up an object ");
        }
       
    }

    public void ShowUIPickUp()
    {
        //presents_UI.SetActive(true);
        //icon_present.SetActive(true);
    }


}
