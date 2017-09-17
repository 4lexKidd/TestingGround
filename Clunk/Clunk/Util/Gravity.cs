using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Clunk;

namespace Clunk.Util
{
    class Gravity : Entity
    {

        //Fallspeed every Linked Entity is accelerated 
        private float fallSpeed;
        
        private enum direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT

        }

        List<Entity> entityList = new List<Entity>();
        public Gravity(float acceleration)
        {
            //Acceleration² for increasing speed
            fallSpeed = acceleration * acceleration / 1000;
            
        }

        public void linkToGravity(ref Entity entity)
        {
            entityList.Add(entity);
        }
        public override void Update()
        {

        }
    }
}
