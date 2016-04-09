using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refugee
{
    public class ColorControl : MonoBehaviour
    {
        public GameObject[] ColeredComponents;
        private List<Material> _materials;

        public void SetColor(Color color)
        {
            ensureMaterials();
            foreach (var material in _materials)
                material.color = color;
        }

        private void ensureMaterials()
        {
            if(_materials != null) return;
            _materials = new List<Material>();
            foreach (var colorComp in ColeredComponents)
                _materials.Add(colorComp.GetComponent<MeshRenderer>().material);
        }
    }
}
