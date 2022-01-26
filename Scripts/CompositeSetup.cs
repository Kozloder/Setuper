using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Installers
{
    [ExecuteInEditMode]
    public class CompositeSetup : BaseSetup
    {
        internal List<BaseSetup> setups = new List<BaseSetup>();

#if UNITY_EDITOR

        private void OnEnable()
        {
            if (ReconnectParentSetup())
            {
                ChildSetupsReconnect();
            }
        }

        private void OnDisable() => DisconnectParentSetup();

        private void OnTransformParentChanged() => ReconnectParentSetup();

        internal void Add(BaseSetup setup)
        {
            if (!setups.Contains(setup))
                setups.Add(setup);
        }

        internal void Remove(BaseSetup setup)
        {
            setups.Remove(setup);
        }

        private bool ReconnectParentSetup()
        {
            if (!Application.isPlaying)
            {
                if (Only1CompositeSetupAllowed())
                {
                    Parent = FindCompositeParent();

                    return true;
                }
            }

            return false;
        }

        private void DisconnectParentSetup()
        {
            if (!Application.isPlaying)
            {
                if (Parent != null)
                {
                    for (int i = 0; i < setups.Count; i++)
                        setups[i].Parent = Parent;

                    Parent = null;
                }
            }
        }

        protected bool Only1CompositeSetupAllowed()
        {
            var composites = GetComponents<CompositeSetup>();

            if (composites.Length > 1)
            {
                if (setups.Count > 0)
                {
                    var composite = composites[0];

                    for (int i = 0; i < setups.Count; i++)
                        setups[i].Parent = composite;
                }

                Debug.Log("Only one composite per gameobject!");
                DestroyImmediate(this);

                return false;
            }

            return true;
        }

        private void ChildSetupsReconnect()
        {
            void Reconnect(Transform transform)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    var childSetups = child.transform.GetComponents<BaseSetup>();

                    foreach (var childSetup in childSetups)
                    {
                        var childSetupParent = childSetup.Parent;

                        if (childSetupParent == null || childSetupParent == Parent)
                        {
                            childSetup.Parent = this;
                        }                            
                    }

                    Reconnect(child);
                }
            }

            if (!Application.isPlaying)
            {
                Reconnect(transform);
            }
        }

#endif

        public override void Setup(SetupContext ctx)
        {
            for (int i = 0; i < setups.Count; i++)
                setups[i].Setup(ctx);
        }

        public override void Dispose()
        {
            for (int i = 0; i < setups.Count; i++)
                setups[i].Dispose();
        }
    }
}