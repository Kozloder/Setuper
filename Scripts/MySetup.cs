using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Installers
{
    public abstract class MySetup : BaseSetup, ISetup
    {

#if UNITY_EDITOR

        private void OnEnable() => ReconnectParentSetup();

        private void OnDisable() => DissconnectParentSetup();

        private void OnTransformParentChanged() => ReconnectParentSetup();

        private void ReconnectParentSetup()
        {
            if (!Application.isPlaying)
            {
                Parent = FindCompositeParent();
            }
        }

        private void DissconnectParentSetup()
        {
            if (!Application.isPlaying)
            {
                Parent = null;
            }
        }

#endif

    }
}