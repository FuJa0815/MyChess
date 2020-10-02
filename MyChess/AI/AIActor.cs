using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChess.OutputClasses;

namespace MyChess.AI
{
    public abstract class AIActor : Actor
    {
        public override bool ShowErrors => false;

        protected AIActor(PlayerColor color) : base(color)
        {

        }
    }
}