using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    public class Lookat: MonoBehaviour
    {
        public Vector3 LookatThis;

        public Lookat()
        {
        }

        public void Update()
        {
            transform.LookAt(LookatThis);
        }
    }
}
