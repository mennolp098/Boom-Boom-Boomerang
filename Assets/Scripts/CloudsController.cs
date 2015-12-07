using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudsController : MonoBehaviour {
    private List<GameObject> _allClouds = new List<GameObject>();
    private Dictionary<GameObject, int> _cloudInLayer = new Dictionary<GameObject, int>();

    private Transform _borderTransform;

    public Sprite[] layerOneClouds = new Sprite[0];
    public Sprite[] layerTwoClouds = new Sprite[0];
    public Sprite[] layerThreeClouds = new Sprite[0];

    public int cloudsOnStage;

    public float layerOneSpeed;
    public float layerTwoSpeed;
    public float layerThreeSpeed;

    void Start()
    {
        _borderTransform = transform.GetChild(0);
        this.GetComponent<SpriteRenderer>().enabled = false;
        _borderTransform.GetComponent<SpriteRenderer>().enabled = false;

        for (int i = 0; i < cloudsOnStage; i++)
        {
            GameObject newCloud = new GameObject();
            newCloud.transform.parent = this.transform;

            int layer = RandomizeCloudLayer(newCloud);

            Vector3 pos = new Vector3(Random.Range(this.transform.position.x, _borderTransform.localPosition.x + 5), Random.Range(_borderTransform.localPosition.y - 1, _borderTransform.localPosition.y + 2), 0);
            newCloud.transform.localPosition = pos;

            _allClouds.Add(newCloud);
            _cloudInLayer.Add(newCloud, layer);
        }
    }

    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.y = 0;
        this.transform.position = pos;
        foreach (var cloud in _allClouds)
        {
            if (_cloudInLayer.ContainsKey(cloud))
            {
                if (_cloudInLayer[cloud] == 1)
                {
                    cloud.transform.localPosition += new Vector3(-1, 0, 0) * layerOneSpeed * Time.deltaTime;
                }
                else if (_cloudInLayer[cloud] == 2)
                {
                    cloud.transform.localPosition += new Vector3(-1, 0, 0) * layerTwoSpeed * Time.deltaTime;
                }
                else if (_cloudInLayer[cloud] == 3)
                {
                    cloud.transform.localPosition += new Vector3(-1, 0, 0) * layerThreeSpeed * Time.deltaTime;
                }
                if (cloud.transform.localPosition.x < -10)
                {
                    ReplaceCloud(cloud);
                }
            }
        }
    }

    void ReplaceCloud(GameObject cloud)
    {
        _cloudInLayer.Remove(cloud);
        int layer = RandomizeCloudLayer(cloud);
        cloud.transform.localPosition = new Vector3(Random.Range(_borderTransform.localPosition.x, _borderTransform.localPosition.x + 5), Random.Range(_borderTransform.localPosition.y - 1, _borderTransform.localPosition.y + 2), 0);
        _cloudInLayer.Add(cloud, layer);
    }

    int RandomizeCloudLayer(GameObject cloud)
    {
        SpriteRenderer cloudSpriteRndr;
        if (cloud.GetComponent<SpriteRenderer>() == null)
        {
            cloudSpriteRndr = cloud.AddComponent<SpriteRenderer>();
        } else
        {
            cloudSpriteRndr = cloud.GetComponent<SpriteRenderer>();
        }
        int layer = Random.Range(1, 4);
        switch (layer)
        {
            case 1:
                cloudSpriteRndr.sprite = layerOneClouds[(int)Random.Range(0, layerOneClouds.Length)];
                cloudSpriteRndr.sortingOrder = -4;
                break;
            case 2:
                cloudSpriteRndr.sprite = layerTwoClouds[(int)Random.Range(0, layerTwoClouds.Length)];
                cloudSpriteRndr.sortingOrder = -5;
                break;
            case 3:
                cloudSpriteRndr.sprite = layerThreeClouds[(int)Random.Range(0, layerThreeClouds.Length)];
                cloudSpriteRndr.sortingOrder = -9;
                break;
            default:
                cloudSpriteRndr.sprite = layerOneClouds[(int)Random.Range(0, layerOneClouds.Length)];
                cloudSpriteRndr.sortingOrder = -4;
                break;
        }
        return layer;
    }

    private int CheckCloudLayer(Sprite cloudSprite)
    {
        int layer = 0;
        foreach (var sprite in layerOneClouds)
        {
            if(sprite == cloudSprite)
            {
                layer = 1;
                break;
            }
        }
        if (layer == 0)
        {
            foreach (var sprite in layerTwoClouds)
            {
                if (sprite == cloudSprite)
                {
                    layer = 2;
                    break;
                }
            }

        }
        if (layer == 0)
        {
            foreach (var sprite in layerThreeClouds)
            {
                if (sprite == cloudSprite)
                {
                    layer = 3;
                    break;
                }
            }
        }
        return layer;
    }
}
