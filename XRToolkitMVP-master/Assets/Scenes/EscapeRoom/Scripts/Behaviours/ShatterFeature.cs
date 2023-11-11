using UnityEngine;

public class ShatterFeature : BaseFeature
{
    [SerializeField]
    private GameObject fracturedObject = null;

    [SerializeField]
    private GameObject insideObject = null;

    [SerializeField]
    private float minSpeed = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        float speed = collision.relativeVelocity.magnitude;
        if(speed > minSpeed)
        {
            Fracture();
        }
    }

    public void Fracture()
    {
        if(fracturedObject == null)
        {
            return;
        }

        this.gameObject.SetActive(false);

        Instantiate(fracturedObject, this.transform.position, this.transform.rotation);

        if(insideObject != null)
        {
            Instantiate(insideObject, this.transform.position, this.transform.rotation);
        }
    }
}
