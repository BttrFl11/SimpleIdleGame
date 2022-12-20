using System.Collections.Generic;
using UnityEngine;

namespace Composite
{
    public class Init : MonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _compositions;

        private void Awake()
        {
            foreach (var composite in _compositions)
            {
                composite.Compose();
            }
        }
    }
}