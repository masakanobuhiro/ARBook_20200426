using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognition : MonoBehaviour{

    public ARTrackedImageManager arTrackedImageManager;
    // Start is called before the first frame update
    public void OnEnable(){
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    // Update is called once per frame
    public void DisEnable(){
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;        
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args) {
        
        foreach (var trackdImage in args.updated) {
        
            if (trackdImage.trackingState == TrackingState.Tracking) {            
                trackdImage.gameObject.SetActive(true);           
            } else {            
                trackdImage.gameObject.SetActive(false);            
            }
        }
    
    }
}
