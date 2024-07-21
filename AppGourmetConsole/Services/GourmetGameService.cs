public class GourmetGameService : IGourmetGameService
{
    private readonly List<GourmetDish> _dishes;

    public GourmetGameService()
    {
        _dishes = new List<GourmetDish>
        {
            new GourmetDish("Lasanha", "Massa"),
            new GourmetDish("Bolo de Chocolate", "Sobremesa")
        };
    }

    public void PlayGame()
    {
        Console.WriteLine("Pense em um prato que você gosta.");

        foreach (var dish in _dishes)
        {
            Console.WriteLine($"O prato que você pensou é {dish.Name}?");
            var answer = Console.ReadLine()?.ToLower();

            if (answer == "sim")
            {
                Console.WriteLine("Acertei de novo!");
                return;
            }
        }

        Console.WriteLine("Qual prato você pensou?");
        var newDishName = Console.ReadLine();

        Console.WriteLine($"{newDishName} é massa ou sobremesa?");
        var newDishCategory = Console.ReadLine();

        _dishes.Add(new GourmetDish(newDishName, newDishCategory));
    }
}
