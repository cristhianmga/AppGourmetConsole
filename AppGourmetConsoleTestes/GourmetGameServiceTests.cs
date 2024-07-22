

namespace GourmetGame.Tests
{
    [TestFixture]
    public class GourmetGameServiceTests
    {
        private IGourmetGameService _gameService;
        
        [SetUp]
        public void  GourmetGameServiceSetup()
        {
            _gameService = new GourmetGameService();
        }


        [Test]
        public void TestAdivinharPrato()
        {
            var input = new StringReader("sim\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            _gameService.PlayGame();

            var consoleOutput = output.ToString().Trim().Split('\n');
            Assert.Contains("Acertei de novo!", consoleOutput);
        }

        [Test]
        public void TestAdicionarNovoPrato()
        {
            var input = new StringReader("\nn\nn\nCarne\nSalgado\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            _gameService.PlayGame();

            var consoleOutput = output.ToString().Trim().Split('\n');
            Assert.Contains("Pense em um prato que você gosta.(Pressione Enter quando terminar de pensar)", consoleOutput);
        }
    }
}
