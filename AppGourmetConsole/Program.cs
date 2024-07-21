class Program
{
    static void Main(string[] args)
    {
        IGourmetGameService gourmetGameService = new GourmetGameService();

        while (true)
        {
            gourmetGameService.PlayGame();

            Console.WriteLine("Quer jogar novamente? (sim/não)");
            var playAgain = Console.ReadLine()?.ToLower();

            if (playAgain != "sim")
            {
                break;
            }
        }
    }
}