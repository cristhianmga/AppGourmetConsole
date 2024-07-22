class Program
{
    static void Main(string[] args)
    {
        IGourmetGameService gourmetGameService = new GourmetGameService();

        while (true)
        {
            gourmetGameService.PlayGame();
        }
    }
}