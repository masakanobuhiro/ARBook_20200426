
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackMultiple : MonoBehaviour {
    [Header("登録したいオブジェクトを追加します。オブジェクトの追加順と画像の登録順で紐付けします。")]

    //マーカーに認識させたいオブジェクトをインスペクターで登録します。
    public List<GameObject> ObjectsToPlace;

    private int refImageCount;
    private Dictionary<string, GameObject> allObjects;
    private ARTrackedImageManager arTrackedImageManager;
    private XRReferenceImageLibrary refLibrary;

    void Awake() {
        //ARTrackedImageManagerを取得します
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable() {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable() {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void Start() {
        refLibrary = arTrackedImageManager.referenceLibrary;
        refImageCount = refLibrary.count;
        LoadObjectDictionary();
    }

    void LoadObjectDictionary() {
        //ARTrackedImageManagerに登録されている画像情報と表示させたいオブジェクトを関連付けします
        allObjects = new Dictionary<string, GameObject>();
        for (int i = 0; i < refImageCount; i++) {
            allObjects.Add(refLibrary[i].name, ObjectsToPlace[i]);
            //最後にオブジェクトを非表示します。
            ObjectsToPlace[i].SetActive(false);
        }
    }

    void ActivateTrackedObject(string _imageName) {
        allObjects[_imageName].SetActive(true);
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs _args) {
        foreach (var addedImage in _args.added) {
            //初検知したらオブジェクト表示をONにします。
            ActivateTrackedObject(addedImage.referenceImage.name);
        }

        foreach (var updated in _args.updated) {
            allObjects[updated.referenceImage.name].transform.position = updated.transform.position;
            allObjects[updated.referenceImage.name].transform.rotation = updated.transform.rotation;
            ///練習問題
            ///ここにマーカーが外れたら非表示する処理を実装してみましょう。
        }
    }
}