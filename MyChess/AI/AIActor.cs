namespace MyChess.AI
{
    public abstract class AiActor : Actor
    {
        public override bool ShowErrors => false;

        protected AiActor(PlayerColor color) : base(color)
        {

        }
    }
}