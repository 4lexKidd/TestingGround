using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Clunk
{
    class PlayerEntity : Entity
    {
        // Create a rectangle image.
        Image image = Image.CreateRectangle(32, 32, Color.White);
         
        
        public PlayerEntity(float x, float y ) : base(x, y)
        {

            var axisMove = Axis.CreateWASD();
            
            // Add the rectangle graphic to the Entity.
            AddGraphic(image);

            // Center the image's origin.
            image.CenterOrigin();

            AddComponents(
                axisMove,
                new TopDownMovement(axisMove, 4)
                
                );
            
        }

        public override void Update()
        {
            base.Update();
            

        }

    }
}
