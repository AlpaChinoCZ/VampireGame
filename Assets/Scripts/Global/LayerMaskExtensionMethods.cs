using UnityEngine;

namespace VG
{
    public static class LayerMaskExtensionMethods
    {
        /// <summary>
        /// Extension method to check if a layer is in a layermask
        /// </summary>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
