using System.Linq;
using System.Threading;
using D3;

namespace Snowshoes.Bots
{
    class TestBot : AbstractBotSherpa
    {
        protected override void Loop()
        {

            var cellar = GetData(() => Unit.Get().FirstOrDefault(u => u.Name.Contains("Dank Cellar")));
            if (cellar == default(Unit))
            {
                Snowshoes.Print("No");
            } else
            {
                Snowshoes.Print("Yes");
            }
            Thread.Sleep(500);
        }
    }
}
