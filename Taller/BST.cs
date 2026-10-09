public class Nodo
{
    public RatingRecord Value { get; set; }
    public Nodo Izquierda { get; set; }
    public Nodo Derecha { get; set; }
    public int Altura { get; set; }

    public Nodo(RatingRecord value)
    {
        Value = value;
        Izquierda = null;
        Derecha = null;
        Altura = 0;
    }
}


public class BST : IRatingTree
{
    private Nodo root;
    private int count;

    public BST()
    {
        root = null;
        count = 0;
    }

    public int Count
    {
        get { return count; }
    }


    // INSERTAR
    public bool Insert(RatingRecord value)
    {
        bool insertado = false;

        root = InsertarRec(root, value, ref insertado);

        if (insertado)
        {
            count++;
            return true;
        }

        return false;
    }

    private Nodo InsertarRec(Nodo nodo, RatingRecord value, ref bool insertado) 
    {
        if (nodo == null) //Si esta vacio el arbol, se crea un nuevo nodo con el valor a insertar
        {
            insertado = true;
            return new Nodo(value);
        }

        if (value.RecordId < nodo.Value.RecordId) //Si es menor se inserta a la izquierda
        {
            nodo.Izquierda = InsertarRec(nodo.Izquierda, value, ref insertado);
        }
        else if (value.RecordId > nodo.Value.RecordId) //Si es mayor se inserta a la derecha
        {
            nodo.Derecha = InsertarRec(nodo.Derecha, value, ref insertado);
        }
        else
        {
            // No se permiten duplicados
            insertado = false;
        }

        return nodo;
    }


    // BUSCAR
    public RatingRecord? FindById(int recordId) 
    {
        Nodo encontrado = FindByIdRec(root, recordId);

        if (encontrado == null)
        {
            return null;
        }

        return encontrado.Value;
    }

    private Nodo FindByIdRec(Nodo nodo, int recordId)
    {
        if (nodo == null)  // Si el nodo es nulo, significa que no se encontró el registro  
        {
            return null;
        }

        if (recordId == nodo.Value.RecordId) // Si el recordId coincide con el nodo actual, se devuelve el nodo
        {
            return nodo;
        }

        if (recordId < nodo.Value.RecordId) // Si el recordId es menor, se busca en el subárbol izquierdo
        {
            return FindByIdRec(nodo.Izquierda, recordId);
        }

        return FindByIdRec(nodo.Derecha, recordId); // Si el recordId es mayor, se busca en el subárbol derecho
    }


    // ELIMINAR
    public bool RemoveById(int recordId)
    {
        bool eliminado = false;

        root = RemoveByIdRec(root, recordId, ref eliminado);

        if (eliminado)
        {
            count--;
            return true;
        }

        return false;
    }

    private Nodo RemoveByIdRec(Nodo nodo, int recordId, ref bool eliminado)
    {
        if (nodo == null) // Si el nodo es nulo, significa que no se encontró el registro
        {
            return null;
        }

        if (recordId < nodo.Value.RecordId) // Si el recordId es menor, se busca en el subárbol izquierdo
        {
            nodo.Izquierda =
                RemoveByIdRec(nodo.Izquierda, recordId, ref eliminado);
        }
        else if (recordId > nodo.Value.RecordId) // Si el recordId es mayor, se busca en el subárbol derecho
        {
            nodo.Derecha =
                RemoveByIdRec(nodo.Derecha, recordId, ref eliminado);
        }
        else
        {
            // Encontramos el nodo
            eliminado = true;

            // Caso 1: no tiene hijos
            if (nodo.Izquierda == null && nodo.Derecha == null)
            {
                return null;
            }

            // Caso 2: solo tiene un hijo
            if (nodo.Izquierda == null)
            {
                return nodo.Derecha;
            }

            if (nodo.Derecha == null)
            {
                return nodo.Izquierda;
            }

            // Caso 3: tiene dos hijos
            Nodo sucesor = ObtenerMenor(nodo.Derecha);

            nodo.Value = sucesor.Value;

            nodo.Derecha = RemoveByIdRec(nodo.Derecha,sucesor.Value.RecordId, ref eliminado);
        }

        return nodo;
    }


    // No está en la interfaz, pero es útil para encontrar el nodo con el valor mínimo en un subárbol
    private Nodo ObtenerMenor(Nodo nodo)
    {
        Nodo actual = nodo;

        while (actual.Izquierda != null)
        {
            actual = actual.Izquierda;
        }

        return actual;
    }


    // Recorridos del árbol
    // INORDER
    public RatingRecord[] InOrder()
    {
        RatingRecord[] resultado = new RatingRecord[count];

        int posicion = 0;

        InOrderRec(root, resultado, ref posicion);

        return resultado;
    }

    private void InOrderRec( Nodo nodo, RatingRecord[] resultado, ref int posicion)
    {
        if (nodo != null)
        {
            InOrderRec(nodo.Izquierda, resultado, ref posicion);

            resultado[posicion] = nodo.Value;
            posicion++;

            InOrderRec(nodo.Derecha, resultado, ref posicion);
        }
    }


    // PREORDER
    public RatingRecord[] PreOrder()
    {
        RatingRecord[] resultado = new RatingRecord[count];

        int posicion = 0;

        PreOrderRec(root, resultado, ref posicion);

        return resultado;
    }

    private void PreOrderRec(Nodo nodo,RatingRecord[] resultado, ref int posicion)
    {
        if (nodo != null)
        {
            resultado[posicion] = nodo.Value;
            posicion++;

            PreOrderRec(nodo.Izquierda, resultado, ref posicion);

            PreOrderRec(nodo.Derecha, resultado, ref posicion);
        }
    }


    // POSTORDER
    public RatingRecord[] PostOrder()
    {
        RatingRecord[] resultado = new RatingRecord[count];

        int posicion = 0;

        PostOrderRec(root, resultado, ref posicion);

        return resultado;
    }

    private void PostOrderRec( Nodo nodo, RatingRecord[] resultado, ref int posicion)
    {
        if (nodo != null)
        {
            PostOrderRec(nodo.Izquierda, resultado, ref posicion);

            PostOrderRec(nodo.Derecha, resultado, ref posicion);

            resultado[posicion] = nodo.Value;
            posicion++;
        }
    }
}