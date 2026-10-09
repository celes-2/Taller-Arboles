 class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========== BST ==========");

        PruebasArbol.Ejecutar(() => new BST());

        Console.WriteLine("\n========== AVL ==========");

        PruebasArbol.Ejecutar(() => new AVL());

        Console.WriteLine("\nPresione una tecla para salir...");
        Console.ReadKey();
    }
}