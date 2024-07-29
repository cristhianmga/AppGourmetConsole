using AppGourmetConsole.Models;
using System.Runtime.Intrinsics.Arm;

public class GourmetGameService : IGourmetGameService
{
    private readonly List<Prato> _pratos;
    private readonly List<Categoria> _categorias;
    private readonly List<GourmetDish> _dishes;
    private readonly GourmetDish _finalDishe;
    List<string> pratos = new List<string>();
    List<Categoria> categorias = new List<Categoria>();
    string answer = "não";
    string answerCategoria = "não";
    string answerSubCategoria = "não";
    string categoriaUpperSelecionada = string.Empty;
    string categoriaSubSelecionada = string.Empty;
    string newDishCategory = string.Empty;

    public GourmetGameService()
    {
        _pratos = new List<Prato>()
        {
            new Prato("Lasanha"),
            new Prato("Bolo de chocolate")
        };
        _categorias = new List<Categoria>()
        {
            new Categoria("Massa"),
            new Categoria("Doce"),
            new Categoria("Topo")
        };
        var categoriaTopo = _categorias.Where(c => c.Nome == "Topo").FirstOrDefault();
        _dishes = new List<GourmetDish>
        {
            new GourmetDish(_pratos.Where(x => x.Nome == "Lasanha").FirstOrDefault(),categoriaTopo,_categorias.Where(x => x.Nome == "Massa").FirstOrDefault()),
            new GourmetDish(_pratos.Where(x => x.Nome == "Bolo de chocolate").FirstOrDefault(),categoriaTopo,_categorias.Where(x => x.Nome == "Doce").FirstOrDefault())
        };
    }

    public void PlayGame()
    {
        Console.WriteLine("\nPense em um prato que você gosta.(Pressione Enter quando terminar de pensar)");
        Console.ReadLine();

        GourmetDish boloChocolate = _dishes.Where(x => x.Categoria.Nome.ToLower() == "doce" && x.UpperCategoria.Nome.ToLower() == "topo").FirstOrDefault();

        categorias = _dishes.Where(x => x.Categoria.Nome.ToLower() != "doce" && x.UpperCategoria.Nome.ToLower() == "topo").Select(x => x.Categoria).ToList();

        foreach (var categoria in categorias)
        {
            Console.WriteLine($"O prato que você pensou é {categoria.Nome}?(Sim/Não)");
            answerCategoria = Console.ReadLine()?.ToLower();
            
            if(answerCategoria == "sim")
            {
                if(_dishes.Any(x => x.UpperCategoria.Nome == categoria.Nome))
                {
                    VerificaSubCategoria(categoria.Nome);
                    while (_dishes.Any(x => x.UpperCategoria.Nome == categoriaSubSelecionada))
                    {
                        VerificaSubCategoria(categoriaSubSelecionada);
                    }
                    VerificaPrato(categoriaSubSelecionada);
                }
                else
                {
                    categoriaUpperSelecionada = categoria.Nome;
                    VerificaPrato(categoria.Nome);
                }
            }

            if (answer == "sim")
            {
                ResetVariaveis();
                Console.WriteLine("\nAcertei de novo!");
                return;
            }

        }
        if(answerCategoria == "não")
        {
            Console.WriteLine($"\nO prato que você pensou é {boloChocolate.Prato.Nome}?(Sim/Não)");
            var answerBolo = Console.ReadLine()?.ToLower();
            if (answerBolo == "sim")
            {
                ResetVariaveis();
                Console.WriteLine("\nAcertei de novo!");
                return;
            }
        }
        Console.WriteLine("\nQual prato você pensou?");
        var newDishName = Console.ReadLine()?.ToLower();

        if(answerCategoria == "sim" && answerSubCategoria == "sim")
        {
            var categoriaUpper = _categorias.Where(x => x.Nome == categoriaSubSelecionada).FirstOrDefault();
            var dish = _dishes.Where(x => x.Categoria == categoriaUpper).FirstOrDefault();
            Console.WriteLine($"\n{newDishName} é ______ mas {dish.Prato.Nome} não.");
            newDishCategory = Console.ReadLine()?.ToLower();

            AddNewGourmetDish(categoriaUpper, newDishCategory,newDishName);
        }else if(answerCategoria == "sim" && answerSubCategoria == "não")
        {
            var categoriaUpper = _categorias.Where(x => x.Nome == categoriaUpperSelecionada).FirstOrDefault();
            var dish = _dishes.Where(x => x.Categoria == categoriaUpper).FirstOrDefault();
            Console.WriteLine($"\n{newDishName} é ______ mas {dish.Prato.Nome} não.");
            newDishCategory = Console.ReadLine()?.ToLower();

            AddNewGourmetDish(categoriaUpper, newDishCategory, newDishName);

        }
        else
        {
            Console.WriteLine($"\n{newDishName} é ______ mas bolo de chocolate não.");
            newDishCategory = Console.ReadLine()?.ToLower();

            AddNewGourmetDish(null, newDishCategory, newDishName);
        }
        ResetVariaveis();
        return;
    }

    private void AddNewGourmetDish(Categoria categoriaUpper, string categoriaNome, string pratoNome)
    {
        var novoprato = new Prato(pratoNome);
        var novaCategoria = new Categoria(categoriaNome);
        if (categoriaUpper == null)
        {
            var categoriaTopo = _categorias.FirstOrDefault(x => x.Nome.ToLower() == "topo");
            _dishes.Add(new GourmetDish(novoprato, categoriaTopo, novaCategoria));
        }
        else
        {
            var categoriaTopo = _categorias.FirstOrDefault(x => x.Nome == categoriaUpper.Nome);
            _dishes.Add(new GourmetDish(novoprato, categoriaTopo, novaCategoria));
        }
        _pratos.Add(novoprato);
        _categorias.Add(novaCategoria);
    }


    private void VerificaSubCategoria(string categoriaUpper)
    {

        foreach (var dishPorCategoria in _dishes.Where(x => x.UpperCategoria.Nome == categoriaUpper))
        {
            Console.WriteLine($"O prato que você pensou é {dishPorCategoria.Categoria.Nome}?(Sim/Não)");
            answerSubCategoria = Console.ReadLine()?.ToLower();
            if (answerSubCategoria == "sim")
            {
                categoriaSubSelecionada = dishPorCategoria.Categoria.Nome;
            }
        }
    }
    private void VerificaPrato(string categoria)
    {
        foreach (var pratosPorCategoria in _dishes.Where(x => x.Categoria.Nome == categoria))
        {
            Console.WriteLine($"\nO prato que você pensou é {pratosPorCategoria.Prato.Nome}?(Sim/Não)");
            answer = Console.ReadLine()?.ToLower();
            if (answer == "sim")
            {
                break;
            }
        }

    }
    private void ResetVariaveis()
    {
        answer = "não";
        answerCategoria = "não";
        answerSubCategoria = "não";
        categoriaUpperSelecionada = string.Empty;
        categoriaSubSelecionada = string.Empty;
        newDishCategory = string.Empty;
    }
}
