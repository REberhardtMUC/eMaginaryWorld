using UnityEngine;



public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] private GameObject effectDisappear;
    void Update()
    {
        if (transform.position.z > 44)
        {
            effectDisappear.SetActive(true);
            if (transform.position.z > 46)
            {
                Destroy(this.gameObject);
                if (transform.position.z > 48)
                {
                    effectDisappear.SetActive(false); 
                }
            }
        }
        
    }
}
