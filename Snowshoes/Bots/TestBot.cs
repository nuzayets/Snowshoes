using System.Linq;
using System.Threading;
using D3;
using Snowshoes.Common;

namespace Snowshoes.Bots
{
    class TestBot : AbstractBotSherpa
    {
        protected override void Loop()
        {

            MoveReallyFast(2966, 2825);
            MoveReallyFast(2941.5f, 2850.7f);
            Interact("Salvage", false);
            SnagIt.SalvageItems();

            MoveReallyFast(2940, 2813);
            MoveReallyFast(2895, 2782);
            Interact("Tashun the Miner", false);
            RepairAll();
            SnagIt.SellItems();
        }
    }
}
