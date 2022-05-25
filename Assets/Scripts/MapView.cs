using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct Spawner
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private string teamName;
    
    public RectTransform RectTransform => rectTransform;

    public string TeamName => teamName;

    public Spawner(RectTransform rectTransform, string teamName)
    {
        this.rectTransform = rectTransform;
        this.teamName = teamName;
    }
}
public class MapView : MonoBehaviour
{
    [SerializeField] private List<Spawner> spawners;
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private Transform parrant;

    public List<Transform> SpawnPoses => spawners.Select(spawn => spawn.RectTransform.transform).ToList();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        spawners.Where(spawner => spawner.RectTransform != null)
            .ToList()
            .ForEach(spawner =>
            {
                var rect = spawner.RectTransform.rect;
                var position = spawner.RectTransform.position;
                Gizmos.DrawIcon(position, "Spawner.png");
                Gizmos.DrawLine(new Vector3(position.x - rect.width / 2, position.y - rect.height / 2),
                    new Vector3(position.x + rect.width / 2, position.y - rect.height / 2));

                Gizmos.DrawLine(new Vector3(position.x - rect.width / 2, position.y - rect.height / 2),
                    new Vector3(position.x - rect.width / 2, position.y + rect.height / 2));

                Gizmos.DrawLine(new Vector3(position.x - rect.width / 2, position.y + rect.height / 2),
                    new Vector3(position.x + rect.width / 2, position.y + rect.height / 2));

                Gizmos.DrawLine(new Vector3(position.x + rect.width / 2, position.y + rect.height / 2),
                    new Vector3(position.x + rect.width / 2, position.y - rect.height / 2));
            });
    }

    [EditorButton]
    private void AddSpawner(string teamName)
    {
        var spawner = Instantiate(spawnerPrefab, parrant).GetOrAddComponent<RectTransform>();
        spawners.Add(new Spawner(spawner, teamName));
    }
#endif
}