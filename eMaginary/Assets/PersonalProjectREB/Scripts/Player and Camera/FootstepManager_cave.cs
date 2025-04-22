using System.Collections.Generic;
using UnityEngine;

public class FootstepManager_cave: MonoBehaviour
{
    public List<AudioClip> caveSteps = new List<AudioClip>();

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        AudioClip clip = caveSteps[Random.Range(0, caveSteps.Count)];
        source.PlayOneShot(clip);
    }

 }
