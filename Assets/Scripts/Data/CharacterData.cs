using Spine.Unity;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CharactersData", menuName = "Data/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private SkeletonDataAsset skeletonData;

        [SpineSkin(dataField: "skeletonData"), SerializeField]
        private string initSkin;

        [SpineAnimation(dataField: "skeletonData"), SerializeField]
        private string initAnimation;
        [SpineAnimation(dataField: "skeletonData"), SerializeField]
        private string attackAnimation;
        [SpineAnimation(dataField: "skeletonData"), SerializeField]
        private string getHitAnimation;
        
        [SerializeField, Space] private int dmg;
        [SerializeField, Space] private int hp;

        public int Dmg => dmg;

        public SkeletonDataAsset SkeletonData => skeletonData;

        public string InitSkin => initSkin;

        public string InitAnimation => initAnimation;

        public int Hp => hp;

        public string AttackAnimation => attackAnimation;

        public string GetHitAnimation => getHitAnimation;
    }
}