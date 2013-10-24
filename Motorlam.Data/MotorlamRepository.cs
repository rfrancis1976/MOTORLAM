using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using inercya.ORMLite;

namespace Motorlam.Data
{
    public class MotorlamRepository : DataAccess
    {
        public MotorlamRepository() : base("Motorlam")
        {
        }
    }
}
