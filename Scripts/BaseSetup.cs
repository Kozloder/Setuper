using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Installers
{
    public abstract class BaseSetup : MonoBehaviour
    {

#if UNITY_EDITOR

        private CompositeSetup parent;

        internal CompositeSetup Parent
        {
            set
            {
                this.parent?.Remove(this);
                this.parent = value;
                this.parent?.Add(this);
            }

            get => parent;
        }

        protected CompositeSetup FindCompositeParent()
        {
            CompositeSetup parent = null;

            void Find(Transform transform)
            {
                if (transform == null)
                    return;

                if (transform.TryGetComponent(out parent))
                    return;

                Find(transform.parent);
            }

            Find(transform.parent);

            return parent;
        }

#endif

        public abstract void Setup(SetupContext ctx);
        public abstract void Dispose();
    }
}