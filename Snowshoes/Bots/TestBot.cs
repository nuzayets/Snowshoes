namespace Snowshoes.Bots
{
    class TestBot : AbstractBotSherpa
    {
        protected override void Loop()
        {
            StartGame();

            GoTown();

        }
    }
}
