
/* Algoritmo genético: Operador cruce */
/*
 * Este código implementa un algoritmo genético que evoluciona una población de cadenas de texto 
 * para que coincidan con la frase objetivo "¡Hola, mundo!". 
 * El algoritmo selecciona padres, cruza sus genes y aplica mutaciones para buscar la solución deseada. 
 * Cada generación muestra la cadena de texto con el mejor ajuste (fitness) hasta que se alcance el objetivo.
 * */
using System;

namespace AlgoGenetico01
{
    class Program
    {
        static void Main()
        {
            string cadOriginal = "Estoy exponiendo sobre un codigo de algoritmo genético";
            Poblacion objPoblacion = new Poblacion();
            int TotalIndividuos = 2000;
            int TotalCiclos = 70000;

            Console.WriteLine("Evolucionando la población... Por favor espera.");
            string MejorIndiv = objPoblacion.Proceso(cadOriginal, TotalIndividuos, TotalCiclos);

            // Muestra el individuo mejor adaptado
            Console.WriteLine("\nResultado final obtenido:");
            Console.WriteLine(MejorIndiv);
            Console.ReadKey();
        }
    }
}

