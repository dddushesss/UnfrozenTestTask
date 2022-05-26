using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private List<SpawnerView> spawners;
        [SerializeField] private GameObject spawnerPrefab;
        [SerializeField] private Transform parrant;

        public List<SpawnerView> Spawners => spawners;

#if UNITY_EDITOR


        [EditorButton]
        private void AddSpawner(Team teamName)
        {
            var spawner = Instantiate(spawnerPrefab, parrant).GetOrAddComponent<SpawnerView>();
            spawner.SetUp(teamName);
            spawners.Add(spawner);
        }
#endif
    }
}