public class GourmetGameService : IGourmetGameService
{
    private readonly List<GourmetDish> _dishes;
    private readonly GourmetDish _finalDishe;

    public GourmetGameService()
    {
        _dishes = new List<GourmetDish>
        {
            new GourmetDish("Lasanha","Massa")
        };

        _finalDishe = new GourmetDish("Bolo de chocolate","Doce");
    }

    public void PlayGame()
    {
        Console.WriteLine("\nPense em um prato que você gosta.(Pressione Enter quando terminar de pensar)");
        Console.ReadLine();

        foreach (var dish in _dishes)
        {
            Console.WriteLine($"O prato que você pensou é {dish.Category}?");
            var answer = Console.ReadLine()?.ToLower();
            
            if(answer == "sim")
            {
                Console.WriteLine($"\nO prato que você pensou é {dish.Name}?");
                answer = Console.ReadLine()?.ToLower();
            }

            if (answer == "sim")
            {
                Console.WriteLine("\nAcertei de novo!");
                return;
            }
        }

        Console.WriteLine($"\nO prato que você pensou é {_finalDishe.Name}?");
        var answerBolo = Console.ReadLine();
        if (answerBolo == "sim")
        {
            Console.WriteLine("\nAcertei de novo!");
            return;
        }
        Console.WriteLine("\nQual prato você pensou?");
        var newDishName = Console.ReadLine();

        Console.WriteLine($"\n{newDishName} é ______ mas bolo de chocolate não.");
        var newDishCategory = Console.ReadLine();

        _dishes.Add(new GourmetDish(newDishName,newDishCategory));
    }
}
