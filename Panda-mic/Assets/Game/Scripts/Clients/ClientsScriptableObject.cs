using UnityEngine;

namespace Game
{
    /// <summary>
    /// Creates a scriptable object with data for all clients.
    /// </summary>
    [CreateAssetMenu(fileName = "ClientsData", menuName = "ScriptableObjects/ClientsScriptableObject", order = 1)]
    public class ClientsScriptableObject : ScriptableObject
    {
        ///<summary>Array of available clients.</summary>
        public ClientData[] clients;
    }

    [System.Serializable]
    public class ClientData
    {
        public Sprite person;
        public Sprite mask;
        public Sprite gloves;
        public Sprite id;
    }
}