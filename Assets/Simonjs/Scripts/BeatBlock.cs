using UnityEngine;

public class BeatBlock : MonoBehaviour
{
    private Vector3 origin;
    public Vector3 destination;
    public float destinationBeat;
    public float travelTime = 5;
    private void Start()
    {
        origin = transform.position;
        
        
    }
    private void LateUpdate()
    {
        float secPerBeat = BeatManager.instance.secPerBeat;
        float currentTime = BeatManager.instance.songCurrentBeat * secPerBeat;

        float hitTime = destinationBeat * secPerBeat;
        float startTime = hitTime - travelTime;
        transform.position = Vector3.Lerp(origin, destination, (currentTime-startTime)/travelTime);
        if ((currentTime - startTime) / travelTime >= 1) 
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            Destroy(gameObject, 0.25f);

        }
        

    }
}
