string input = File.ReadAllText("input.txt");// berni es el mejor
int filas = 130;
int columnas = 130;
SortedSet<(int fila, int columna)> obstaculos = new();
(int fila, int columna) posicion = (0,0);
Direccion direccion = Direccion.Arriba; // siempre empieza mirando para arriba
for (int fila = 0; fila < filas; fila = fila + 1)
{
    for (int columna = 0; columna < columnas; columna = columna + 1)
    {
        char actual = input[fila * (columnas + 1) + columna];
        if (actual == '#')
        {
            obstaculos.Add((fila, columna));
        }
        if (actual == '^')
        {
            posicion = (fila, columna);
        }
        //Console.Write(actual);
    }
    //Console.WriteLine();
}

SortedSet<(int fila, int columna)> ruta = [posicion];

for (int pasos = 0; pasos < 99999; pasos++)
{
    //for (int fila = 0; fila < filas; fila = fila + 1)
    //{
    //    for (int columna = 0; columna < columnas; columna = columna + 1)
    //    {
    //        if (posicion == (fila, columna))
    //        {
    //            if (direccion == Direccion.Arriba)
    //            {
    //                Console.Write('^');
    //            }
    //            else if (direccion == Direccion.Derecha)
    //            {
    //                Console.Write('>');
    //            }
    //            else if (direccion == Direccion.Abajo)
    //            {
    //                Console.Write('v');
    //            }
    //            else if (direccion == Direccion.Izquierda)
    //            {
    //                Console.Write('<');
    //            }                
    //        }
    //        else if (obstaculos.Contains((fila, columna)))
    //        {
    //            Console.Write('#');
    //        }
    //        else
    //        {
    //            Console.Write('.');
    //        }
    //    }
    //    Console.WriteLine();
    //}
    Console.WriteLine(ruta.Count);
    //foreach (var p in ruta)
    //{
    //    Console.Write(p.ToString());
    //    Console.Write(' ');
    //}
    //Console.WriteLine();

    (int fila, int columna) proxima_posicion = (0, 0);
    if (direccion == Direccion.Arriba)
    {
        proxima_posicion = (posicion.fila - 1, posicion.columna);//mover ariba
    }
    if (direccion == Direccion.Derecha)
    {
        proxima_posicion = (posicion.fila, posicion.columna + 1);//mover derecha
    }
    if (direccion == Direccion.Abajo)
    {
        proxima_posicion = (posicion.fila + 1, posicion.columna);//mover abajo
    }
    if (direccion == Direccion.Izquierda)
    {
        proxima_posicion = (posicion.fila, posicion.columna - 1);//mover izquierda
    }

    if (obstaculos.Contains(proxima_posicion)) // colision
    {
        // girar

        if (direccion == Direccion.Arriba)
        {
            direccion = Direccion.Derecha;
        }
        else if (direccion == Direccion.Derecha)
        {
            direccion = Direccion.Abajo;
        }
        else if (direccion == Direccion.Abajo)
        {
            direccion = Direccion.Izquierda;
        }
        else if (direccion == Direccion.Izquierda)
        {
            direccion = Direccion.Arriba;
        }
    }
    else
    {
        posicion = proxima_posicion;

        if (posicion.fila == -1)
            break;
        if (posicion.fila == filas)
            break;
        if (posicion.columna == -1)
            break;
        if (posicion.columna == columnas)
            break;

        ruta.Add(posicion);
    }
}

enum Direccion
{
    Arriba,
    Derecha,
    Abajo,
    Izquierda,
}
