public class PruebasArbol
{
    // Ejecuta las pruebas comunes para BST o AVL
    public static void Ejecutar(Func<IRatingTree> crearArbol)
    {
        Console.WriteLine("\n===== PRUEBAS COMUNES =====");

        PruebaArbolVacio(crearArbol());
        PruebaInsercion(crearArbol());
        PruebaDuplicados(crearArbol());
        PruebaBusqueda(crearArbol());
        PruebaRecorridos(crearArbol());
        PruebaEliminacionHoja(crearArbol());
        PruebaEliminacionUnHijo(crearArbol());
        PruebaEliminacionDosHijos(crearArbol());
        PruebaEliminarRaiz(crearArbol());
    }

    // =========================
    // ARBOL VACIO
    // =========================

    private static void PruebaArbolVacio(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Árbol vacío ---");

        Verificar(arbol.Count == 0, "Count = 0");
        Verificar(arbol.FindById(10) == null, "Búsqueda devuelve null");
        Verificar(!arbol.RemoveById(10), "Eliminar devuelve false");

        Verificar(arbol.InOrder().Length == 0, "InOrder vacío");
        Verificar(arbol.PreOrder().Length == 0, "PreOrder vacío");
        Verificar(arbol.PostOrder().Length == 0, "PostOrder vacío");
    }

    // =========================
    // INSERCION
    // =========================

    private static void PruebaInsercion(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Inserción ---");

        arbol.Insert(CrearRegistro(50));
        arbol.Insert(CrearRegistro(30));
        arbol.Insert(CrearRegistro(70));

        Verificar(arbol.Count == 3, "Count = 3");
        Verificar(arbol.FindById(50) != null, "Encontró 50");
        Verificar(arbol.FindById(30) != null, "Encontró 30");
        Verificar(arbol.FindById(70) != null, "Encontró 70");

        VerificarOrdenYCantidad(arbol);
    }

    // =========================
    // DUPLICADOS
    // =========================

    private static void PruebaDuplicados(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Duplicados ---");

        RatingRecord original = CrearRegistro(50);

        Verificar(arbol.Insert(original), "Insertó registro original");

        bool resultado = arbol.Insert(CrearRegistro(50));

        Verificar(!resultado, "Rechazó duplicado");
        Verificar(arbol.Count == 1, "Count sigue en 1");
        Verificar(
            arbol.FindById(50) == original,
            "Conservó el registro original"
        );
    }

    // =========================
    // BUSQUEDA
    // =========================

    private static void PruebaBusqueda(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Búsqueda ---");

        arbol.Insert(CrearRegistro(50));
        arbol.Insert(CrearRegistro(30));
        arbol.Insert(CrearRegistro(70));

        Verificar(arbol.FindById(30) != null, "Encontró clave existente");
        Verificar(arbol.FindById(70) != null, "Encontró otra clave existente");
        Verificar(arbol.FindById(99) == null, "No encontró clave inexistente");
    }

    // =========================
    // RECORRIDOS
    // =========================

    private static void PruebaRecorridos(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Recorridos ---");

        arbol.Insert(CrearRegistro(50));
        arbol.Insert(CrearRegistro(30));
        arbol.Insert(CrearRegistro(70));
        arbol.Insert(CrearRegistro(20));
        arbol.Insert(CrearRegistro(40));
        arbol.Insert(CrearRegistro(60));
        arbol.Insert(CrearRegistro(80));

        VerificarRecorrido(
            arbol.InOrder(),
            new int[] { 20, 30, 40, 50, 60, 70, 80 },
            "InOrder"
        );

        VerificarRecorrido(
            arbol.PreOrder(),
            new int[] { 50, 30, 20, 40, 70, 60, 80 },
            "PreOrder"
        );

        VerificarRecorrido(
            arbol.PostOrder(),
            new int[] { 20, 40, 30, 60, 80, 70, 50 },
            "PostOrder"
        );
    }

    // =========================
    // ELIMINAR HOJA
    // =========================

    private static void PruebaEliminacionHoja(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Eliminar hoja ---");

        arbol.Insert(CrearRegistro(50));
        arbol.Insert(CrearRegistro(30));
        arbol.Insert(CrearRegistro(70));

        Verificar(arbol.RemoveById(30), "Eliminó hoja");
        Verificar(arbol.FindById(30) == null, "La hoja ya no existe");
        Verificar(arbol.Count == 2, "Count = 2");

        VerificarOrdenYCantidad(arbol);
    }

    // =========================
    // ELIMINAR NODO CON UN HIJO
    // =========================

    private static void PruebaEliminacionUnHijo(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Eliminar nodo con un hijo ---");

        arbol.Insert(CrearRegistro(50));
        arbol.Insert(CrearRegistro(30));
        arbol.Insert(CrearRegistro(20));

        Verificar(arbol.RemoveById(30), "Eliminó nodo con un hijo");
        Verificar(arbol.FindById(30) == null, "El nodo ya no existe");
        Verificar(arbol.FindById(20) != null, "Conservó el hijo");
        Verificar(arbol.Count == 2, "Count = 2");

        VerificarOrdenYCantidad(arbol);
    }

    // =========================
    // ELIMINAR NODO CON DOS HIJOS
    // =========================

    private static void PruebaEliminacionDosHijos(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Eliminar nodo con dos hijos ---");

        arbol.Insert(CrearRegistro(50));
        arbol.Insert(CrearRegistro(30));
        arbol.Insert(CrearRegistro(70));
        arbol.Insert(CrearRegistro(60));
        arbol.Insert(CrearRegistro(80));

        Verificar(arbol.RemoveById(70), "Eliminó nodo con dos hijos");
        Verificar(arbol.FindById(70) == null, "El nodo ya no existe");
        Verificar(arbol.FindById(60) != null, "Conservó 60");
        Verificar(arbol.FindById(80) != null, "Conservó 80");
        Verificar(arbol.Count == 4, "Count = 4");

        VerificarOrdenYCantidad(arbol);
    }

    // =========================
    // ELIMINAR RAIZ
    // =========================

    private static void PruebaEliminarRaiz(IRatingTree arbol)
    {
        Console.WriteLine("\n--- Eliminar raíz ---");

        arbol.Insert(CrearRegistro(50));

        Verificar(arbol.RemoveById(50), "Eliminó la única raíz");
        Verificar(arbol.Count == 0, "Count = 0");
        Verificar(arbol.FindById(50) == null, "La raíz ya no existe");
        Verificar(arbol.InOrder().Length == 0, "El árbol quedó vacío");
    }

    // =========================
    // VERIFICAR RECORRIDOS
    // =========================

    private static void VerificarRecorrido(
        RatingRecord[] registros,
        int[] esperado,
        string nombre)
    {
        bool correcto = registros.Length == esperado.Length;

        if (correcto)
        {
            for (int i = 0; i < esperado.Length; i++)
            {
                if (registros[i].RecordId != esperado[i])
                {
                    correcto = false;
                    break;
                }
            }
        }

        Verificar(correcto, nombre + " correcto");
    }

    // =========================
    // VERIFICAR ORDEN Y CANTIDAD
    // =========================

    private static void VerificarOrdenYCantidad(IRatingTree arbol)
    {
        RatingRecord[] registros = arbol.InOrder();

        bool ordenado = true;

        for (int i = 1; i < registros.Length; i++)
        {
            if (registros[i - 1].RecordId >= registros[i].RecordId)
            {
                ordenado = false;
                break;
            }
        }

        Verificar(ordenado, "Orden de búsqueda correcto");
        Verificar(registros.Length == arbol.Count, "Cantidad de nodos correcta");
    }

    // =========================
    // MOSTRAR RESULTADOS
    // =========================

    private static void Verificar(bool condicion, string mensaje)
    {
        if (condicion)
        {
            Console.WriteLine("OK: " + mensaje);
        }
        else
        {
            Console.WriteLine("ERROR: " + mensaje);
        }
    }

  

    private static RatingRecord CrearRegistro(int id)
    {
        return new RatingRecord(
            id,
            1,
            1,
            5,
            1000
        );
    }
}