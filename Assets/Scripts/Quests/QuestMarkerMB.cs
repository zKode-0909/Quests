using UnityEngine;

public class QuestMarkerMB : MonoBehaviour
{
    [SerializeField] private Transform markerAnchor;
    [SerializeField] GameObject availablePrefab;
    [SerializeField] GameObject completedPrefab;
    [SerializeField] GameObject inProgressPrefab;


    private GameObject availableInstance;
    private GameObject completedInstance;
    private GameObject inProgressInstance;

    public void Awake()
    {
        availableInstance = Instantiate(availablePrefab);
        completedInstance = Instantiate(completedPrefab);
        inProgressInstance = Instantiate(inProgressPrefab);

        availableInstance.transform.SetParent(this.gameObject.transform, false); // worldPositionStays = false
        availableInstance.transform.localPosition = Vector3.zero;

        completedInstance.transform.SetParent(this.gameObject.transform, false);
        completedInstance.transform.localPosition = Vector3.zero;

        inProgressInstance.transform.SetParent(this.gameObject.transform, false);
        inProgressInstance.transform.localPosition = Vector3.zero;



        Debug.Log($"local pos: {this.gameObject.transform.localPosition}   world pos: {this.gameObject.transform.position}");

        availableInstance.SetActive(false);
        completedInstance.SetActive(false);
        inProgressInstance.SetActive(false);
    }

    static string GetPath(Transform t)
    {
        var path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }

    public void SetMarker(QuestDisplayIcon iconToShow) {
        availableInstance.SetActive(false);
        completedInstance.SetActive(false);
        inProgressInstance.SetActive(false);
        Debug.Log($"about to show the icon: {iconToShow}");

        switch (iconToShow) { 
            
            case QuestDisplayIcon.Available: availableInstance.SetActive(true); break;
            case QuestDisplayIcon.Completed: completedInstance.SetActive(true); break;
            case QuestDisplayIcon.InProgress: inProgressInstance.SetActive(true); break;
            case QuestDisplayIcon.None: default:  break;
        }
    }


}
