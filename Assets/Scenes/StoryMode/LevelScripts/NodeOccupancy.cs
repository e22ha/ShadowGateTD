// NodeOccupancy.cs
using UnityEngine;

namespace Scenes.StoryMode.Scripts
{
    public class NodeOccupancy : MonoBehaviour
    {
        public float yOffset;

        public bool IsOccupied { get; private set; }

        public float GetYOffset()
        {
            return yOffset;
        }

        public void MarkOccupied()
        {
            IsOccupied = true;
        }
    }
}