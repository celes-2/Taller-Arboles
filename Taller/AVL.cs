public class AVL : IRatingTree
{
    private Nodo root;
    private int count;

    public AVL()
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
        if (FindById(value.RecordId) != null)
        {
            return false;
        }

        root = InsertarRec(root, value);
        count++;

        return true;
    }

    private Nodo InsertarRec(Nodo nodo, RatingRecord value)
    {
        // Si encontramos un espacio vacío
        if (nodo == null)
        {
            return new Nodo(value);
        }

        // Insertar a la izquierda
        if (value.RecordId < nodo.Value.RecordId)
        {
            nodo.Izquierda = InsertarRec(nodo.Izquierda, value);
        }
        // Insertar a la derecha
        else
        {
            nodo.Derecha = InsertarRec(nodo.Derecha, value);
        }

        // Actualizar altura
        nodo.Altura = 1 + Math.Max(
            ObtenerAltura(nodo.Izquierda),
            ObtenerAltura(nodo.Derecha)
        );

        // Revisar balance
        int balance = ObtenerBalance(nodo);

        // Caso izquierda-izquierda
        if (balance > 1 && value.RecordId < nodo.Izquierda.Value.RecordId)
        {
            return RotarDerecha(nodo);
        }

        // Caso derecha-derecha
        if (balance < -1 && value.RecordId > nodo.Derecha.Value.RecordId)
        {
            return RotarIzquierda(nodo);
        }

        // Caso izquierda-derecha
        if (balance > 1 && value.RecordId > nodo.Izquierda.Value.RecordId)
        {
            nodo.Izquierda = RotarIzquierda(nodo.Izquierda);
            return RotarDerecha(nodo);
        }

        // Caso derecha-izquierda
        if (balance < -1 && value.RecordId < nodo.Derecha.Value.RecordId)
        {
            nodo.Derecha = RotarDerecha(nodo.Derecha);
            return RotarIzquierda(nodo);
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
        if (nodo == null)
        {
            return null;
        }

        if (nodo.Value.RecordId == recordId)
        {
            return nodo;
        }

        if (recordId < nodo.Value.RecordId)
        {
            return FindByIdRec(nodo.Izquierda, recordId);
        }

        return FindByIdRec(nodo.Derecha, recordId);
    }

    // ELIMINAR

    public bool RemoveById(int recordId)
    {
        if (FindById(recordId) == null)
        {
            return false;
        }

        root = RemoveByIdRec(root, recordId);
        count--;

        return true;
    }

    private Nodo RemoveByIdRec(Nodo nodo, int recordId)
    {
        if (nodo == null)
        {
            return null;
        }

        // Buscar a la izquierda
        if (recordId < nodo.Value.RecordId)
        {
            nodo.Izquierda = RemoveByIdRec(nodo.Izquierda, recordId);
        }
        // Buscar a la derecha
        else if (recordId > nodo.Value.RecordId)
        {
            nodo.Derecha = RemoveByIdRec(nodo.Derecha, recordId);
        }
        // Encontramos el nodo
        else
        {
            // No tiene hijos
            if (nodo.Izquierda == null && nodo.Derecha == null)
            {
                return null;
            }

            // Solo tiene hijo derecho
            if (nodo.Izquierda == null)
            {
                return nodo.Derecha;
            }

            // Solo tiene hijo izquierdo
            if (nodo.Derecha == null)
            {
                return nodo.Izquierda;
            }

            // Tiene dos hijos
            Nodo sucesor = ObtenerMenor(nodo.Derecha);

            nodo.Value = sucesor.Value;

            nodo.Derecha = RemoveByIdRec(
                nodo.Derecha,
                sucesor.Value.RecordId
            );
        }

        // Actualizar altura
        nodo.Altura = 1 + Math.Max(
            ObtenerAltura(nodo.Izquierda),
            ObtenerAltura(nodo.Derecha)
        );

        // Revisar balance
        int balance = ObtenerBalance(nodo);

        // Izquierda - Izquierda
        if (balance > 1 && ObtenerBalance(nodo.Izquierda) >= 0)
        {
            return RotarDerecha(nodo);
        }

        // Izquierda - Derecha
        if (balance > 1 && ObtenerBalance(nodo.Izquierda) < 0)
        {
            nodo.Izquierda = RotarIzquierda(nodo.Izquierda);
            return RotarDerecha(nodo);
        }

        // Derecha - Derecha
        if (balance < -1 && ObtenerBalance(nodo.Derecha) <= 0)
        {
            return RotarIzquierda(nodo);
        }

        // Derecha - Izquierda
        if (balance < -1 && ObtenerBalance(nodo.Derecha) > 0)
        {
            nodo.Derecha = RotarDerecha(nodo.Derecha);
            return RotarIzquierda(nodo);
        }

        return nodo;
    }

    // ALTURA

    private int ObtenerAltura(Nodo nodo)
    {
        if (nodo == null)
        {
            return 0;
        }

        return nodo.Altura;
    }

    // BALANCE

    private int ObtenerBalance(Nodo nodo)
    {
        if (nodo == null)
        {
            return 0;
        }

        return ObtenerAltura(nodo.Izquierda)
             - ObtenerAltura(nodo.Derecha);
    }

    // ROTACIÓN DERECHA

    private Nodo RotarDerecha(Nodo nodo)
    {
        Nodo nuevaRaiz = nodo.Izquierda;
        Nodo temporal = nuevaRaiz.Derecha;

        nuevaRaiz.Derecha = nodo;
        nodo.Izquierda = temporal;

        nodo.Altura = 1 + Math.Max(
            ObtenerAltura(nodo.Izquierda),
            ObtenerAltura(nodo.Derecha)
        );

        nuevaRaiz.Altura = 1 + Math.Max(
            ObtenerAltura(nuevaRaiz.Izquierda),
            ObtenerAltura(nuevaRaiz.Derecha)
        );

        return nuevaRaiz;
    }

    // ROTACIÓN IZQUIERDA

    private Nodo RotarIzquierda(Nodo nodo)
    {
        Nodo nuevaRaiz = nodo.Derecha;
        Nodo temporal = nuevaRaiz.Izquierda;

        nuevaRaiz.Izquierda = nodo;
        nodo.Derecha = temporal;

        nodo.Altura = 1 + Math.Max(
            ObtenerAltura(nodo.Izquierda),
            ObtenerAltura(nodo.Derecha)
        );

        nuevaRaiz.Altura = 1 + Math.Max(
            ObtenerAltura(nuevaRaiz.Izquierda),
            ObtenerAltura(nuevaRaiz.Derecha)
        );

        return nuevaRaiz;
    }

    // OBTENER MENOR

    private Nodo ObtenerMenor(Nodo nodo)
    {
        Nodo actual = nodo;

        while (actual.Izquierda != null)
        {
            actual = actual.Izquierda;
        }

        return actual;
    }

    // INORDER

    public RatingRecord[] InOrder()
    {
        RatingRecord[] resultado = new RatingRecord[count];
        int posicion = 0;

        InOrderRec(root, resultado, ref posicion);

        return resultado;
    }

    private void InOrderRec(
        Nodo nodo,
        RatingRecord[] resultado,
        ref int posicion)
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

    private void PreOrderRec(
        Nodo nodo,
        RatingRecord[] resultado,
        ref int posicion)
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

    private void PostOrderRec(
        Nodo nodo,
        RatingRecord[] resultado,
        ref int posicion)
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