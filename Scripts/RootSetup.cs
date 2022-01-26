using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Installers
{
    [DefaultExecutionOrder(-10)]
    public sealed class RootSetup : CompositeSetup
    {
        private void Start()
        {
            //if (!Application.isPlaying)
            //{
                Setup(new SetupContext());
                Dispose();
            //}
        }

        public override void Setup(SetupContext ctx)
        {
            for (int i = 0; i < setups.Count; i++)
            {
                var setup = setups[i];
                setup.Setup(ctx);
            }
        }

        public override void Dispose()
        {
            for (int i = 0; i < setups.Count; i++)
            {
                var setup = setups[i];
                setup.Dispose();
            }
        }
    }
}