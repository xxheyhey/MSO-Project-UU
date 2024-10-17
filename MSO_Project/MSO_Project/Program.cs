namespace MSO_Project
{
    internal static class Program
    {
        private static void Main()
        {
            Game game = Game.Examples[1];
            game.Execute();
            game.CalculateMetrics();

            Game importedGame = Game.Import("../../../test.txt");
            importedGame.Execute();
            importedGame.CalculateMetrics();

        }
    }
}