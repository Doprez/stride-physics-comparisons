using Stride.Engine;

namespace PhysicsComparison
{
    class PhysicsComparisonApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
