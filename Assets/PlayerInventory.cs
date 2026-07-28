using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public static class PlayerInventory
{
    [Header("IsHolding?")]
    [SerializeField] private static bool holdingVHSTape;
    [SerializeField] private static VideoClip mostRecentVideo;

    public static bool HasVHSTape()
    {
        Debug.Log("hasVHSTapeCalled");
        return (holdingVHSTape);
    }

    public static void HasVHSTape(bool hasVHSTape, VideoClip video)
    {
        Debug.Log("hasVHSTapeset");
        holdingVHSTape = hasVHSTape;
        mostRecentVideo = video;
    }

    public static VideoClip GetVideoClip()
    {
        Debug.Log("returned: " + mostRecentVideo.name);
        return mostRecentVideo;
    }

}
