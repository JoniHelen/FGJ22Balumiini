using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Browse : MonoBehaviour
{
    [SerializeField] Image art;
    [SerializeField] VideoPlayer moviePlayer;

    [SerializeField] List<Sprite> artGallery;
    [SerializeField] List<VideoClip> videos;

    [SerializeField] Button PreviousImage;
    [SerializeField] Button NextImage;

    [SerializeField] Button PreviousVideo;
    [SerializeField] Button NextVideo;

    int currentImage = 0;
    int currentVideo = 0;


    private void Start()
    {
        artGallery = new List<Sprite>();
        Object[] objects = Resources.LoadAll("Images");
        foreach (Object _object in objects)
        {
            if (_object is Sprite)
            {
                Sprite img = (Sprite)_object;
                artGallery.Add(img);
            }
        }
    }

    public void BrowseImage(int i = 1)
    {
        moviePlayer.enabled = false;
        art.gameObject.SetActive(true);

        if (currentImage + i >= artGallery.Count) currentImage = 0;
        else if (currentImage + i < 0) currentImage = artGallery.Count - 1;
        else currentImage += i;

        art.sprite = artGallery[currentImage];
        art.preserveAspect = true;
    }

    public void BrowseVideo(int i = 1)
    {
        moviePlayer.enabled = true;
        art.gameObject.SetActive(false);

        if (currentVideo + i >= videos.Count) currentVideo = 0;
        else if (currentVideo + i < 0) currentVideo = videos.Count - 1;
        else currentVideo += i;

        moviePlayer.clip = videos[currentVideo];
        moviePlayer.Play();
    }

}
