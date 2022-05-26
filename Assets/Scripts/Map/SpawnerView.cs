using UnityEngine;

namespace Map
{
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Team teamName;
        [SerializeField] private bool isInFront;
        
        public Team Team => teamName;
        public Vector3 SpawnPos => rectTransform.position - new Vector3(0, rectTransform.rect.height/2, 0);
        public Vector2 Size => rectTransform.rect.size;

        public bool IsInFront => isInFront;

        public void SetUp(Team teamName)
        {
            this.rectTransform = GetComponent<RectTransform>();
            this.teamName = teamName;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(rectTransform is null)
                this.rectTransform = GetComponent<RectTransform>();
            var rect = rectTransform.rect;
            var position = rectTransform.position;
        
            Gizmos.DrawIcon(position, "Spawner.png");
            Gizmos.DrawLine(new Vector3(position.x - rect.width / 2, position.y - rect.height / 2),
                new Vector3(position.x + rect.width / 2, position.y - rect.height / 2));

            Gizmos.DrawLine(new Vector3(position.x - rect.width / 2, position.y - rect.height / 2),
                new Vector3(position.x - rect.width / 2, position.y + rect.height / 2));

            Gizmos.DrawLine(new Vector3(position.x - rect.width / 2, position.y + rect.height / 2),
                new Vector3(position.x + rect.width / 2, position.y + rect.height / 2));

            Gizmos.DrawLine(new Vector3(position.x + rect.width / 2, position.y + rect.height / 2),
                new Vector3(position.x + rect.width / 2, position.y - rect.height / 2));
        }
#endif
    }
}