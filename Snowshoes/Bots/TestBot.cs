using D3;
using Snowshoes.Common;

namespace Snowshoes.Bots
{
    class TestBot : AbstractBotSherpa
    {
        protected override void Loop()
        {
            SnagIt.StashItems();
            Snowshoes.Stop();

        }
    }
}
