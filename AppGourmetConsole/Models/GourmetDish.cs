using AppGourmetConsole.Models;

public class GourmetDish
{
    public Prato Prato { get; set; }
    public Categoria Categoria { get; set; }
    public Categoria UpperCategoria { get; set; }

    public GourmetDish(Prato prato, Categoria categoriaUpper, Categoria categoria)
    {
        Prato = prato;
        Categoria = categoria;
        UpperCategoria = categoriaUpper;
    }
}
