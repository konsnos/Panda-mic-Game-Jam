using UnityEngine;

namespace LineUp
{
    [CreateAssetMenu(fileName = "AudioClipsList", menuName = "ScriptableObjects/AudioClipsListScriptableObject", order = 1)]
    public class AudioClipsListScriptableObject : ScriptableObject
    {
        ///<summary>0 - correct, 1 - wrong</summary>
        [Tooltip("0 - correct, 1 - wrong")]
        public AudioClip[] clips;
    }
}