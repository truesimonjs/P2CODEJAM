using UnityEngine;

public class BeatManager : MonoBehaviour
{
    [SerializeField] float songBPM = 110;
    [SerializeField] float firstBeatOffset = 6;
    private float secPerBeat;
    private AudioSource audioSource;
    [SerializeField] private float songPosition;
    [SerializeField] private float songCurrentBeat;
    bool foundStart = false;

    float songStart;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        secPerBeat = 60 / songBPM;
        songStart = (float)AudioSettings.dspTime;
        audioSource.Play();
    }

    private void Update()
    {
        songPosition = (float)AudioSettings.dspTime - songStart - firstBeatOffset;
        songCurrentBeat = songPosition / secPerBeat;
        if (!foundStart && CurrentLoudness() > 0.01f)
        {
            Debug.Log((float)AudioSettings.dspTime - songStart);
            foundStart = true;
        }

    }

    public float CurrentLoudness()
    {
        const int sampleDataLength = 1024;
        float[] clipSampleData = new float[sampleDataLength];


        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
        float clipLoudness = 0f;
        foreach (var sample in clipSampleData)
        {
            clipLoudness += Mathf.Abs(sample);
        }
        clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        return clipLoudness;

    }
}
