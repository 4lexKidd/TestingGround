using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Clunk
{
    class TopDownMovement : Component
    {
        Axis axis;

        float moveSpeed;

        public TopDownMovement(Axis movementAxis,float speed)
        {
            axis = movementAxis;
            moveSpeed = speed;
        }

        public override void Update()
        {
            base.Update();

            Entity.AddPosition(axis, moveSpeed);
        }

    }
}
