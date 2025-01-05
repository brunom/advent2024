string input = File.ReadAllText("input.txt");// BERNI ES EL MEJOR
int filas = 130;//definicion FILAS
int columnas = 130;// definicion COLUMNAS
SortedSet<(int fila, int columna)> obstaculos = new(); // definicion OBSTACULO
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
        if (actual == '^') // "JUGADOR" principio
        {
            posicion = (fila, columna);
        }
        //Console.Write(actual);
    }
    //Console.WriteLine();
}

SortedSet<(int fila, int columna)> ruta = [posicion];

for (int pasos = 0; pasos < 16900; pasos++)// cantidad de "movimientos" del guardia
{
    //for (int fila = 0; fila < filas; fila = fila + 1)
    //{
    //    for (int columna = 0; columna < columnas; columna = columna + 1)
    //    {
    //        if (posicion == (fila, columna))// cambiar "SKIN" / dibujar movimientos
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
    //            Console.Write('#');// obstaculos
    //        }
    //        else
    //        {
    //            Console.Write('.');
    //        }
    //    }
    //    Console.WriteLine();
    //}
    Console.WriteLine(ruta.Count);// contar espacios RUTA
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
        posicion = proxima_posicion;// salir del mapa

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
