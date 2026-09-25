using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AlgoGenetico01
{
    class Poblacion // crea la clase poblacion
    {
        private readonly List<string> Individuos; //crea una lista privada de solo lectura que almacena las lineas de texto
        //readonly hace que sea solo lectura
        private readonly Random azar; // Declara una variable privada y de solo lectura para generar números aleatorios necesarios en la evolución.

        public Poblacion() //crea el constructor poblacion
        {
            Individuos = new List<string>(); // Inicializa la lista en la memoria para que esté lista para recibir los textos.
            azar = new Random(); //inicializa el generador de numeros aleatorios
        }

        public string Proceso(string cadOriginal, int numIndividuos, int numCiclos) // Define el método principal que ejecuta la evolución; recibe la frase objetivo, el tamaño de la población y el límite de ciclos.
        {
            Cadena objCad = new Cadena();

            Individuos.Clear();

            // Crea la población inicial
            for (int cont = 1; cont <= numIndividuos; cont++)
            {
                Individuos.Add(objCad.CadenaAzar(azar, cadOriginal.Length));
            }
            // cramos la variable mejor puntaje global y Guardamos el mejor puntaje global para ir mostrando avances en consola
            int mejorPuntajeGlobal = 0;

            // Proceso de algoritmo genético
            for (int ciclo = 1; ciclo <= numCiclos; ciclo++)
            {
                // Selecciona dos padres al azar
                int indivA = azar.Next(Individuos.Count);
                int indivB;
                do
                {
                    indivB = azar.Next(Individuos.Count);
                } while (indivA == indivB);

                // Operador cruce
                int posAzar = azar.Next(cadOriginal.Length);
                string HijoA = Individuos[indivA].Substring(0, posAzar) + Individuos[indivB].Substring(posAzar);
                string HijoB = Individuos[indivB].Substring(0, posAzar) + Individuos[indivA].Substring(posAzar);

                // Operador mutación (Probabilidad del 20% para asegurar cambios constantes)
                if (azar.Next(100) < 20) HijoA = objCad.MutaCadena(azar, HijoA);
                if (azar.Next(100) < 20) HijoB = objCad.MutaCadena(azar, HijoB);

                // Evaluamos a todos
                int valorIndivA = objCad.EvaluaCadena(cadOriginal, Individuos[indivA]);
                int valorIndivB = objCad.EvaluaCadena(cadOriginal, Individuos[indivB]);
                int valorHijoA = objCad.EvaluaCadena(cadOriginal, HijoA);
                int valorHijoB = objCad.EvaluaCadena(cadOriginal, HijoB);

                // ESTRATEGIA DE REEMPLAZO MEJORADA: El hijo sobrevive si supera al PEOR de los dos padres
                int peorPadre = (valorIndivA < valorIndivB) ? indivA : indivB;
                int valorPeorPadre = Math.Min(valorIndivA, valorIndivB);

                if (valorHijoA > valorPeorPadre)
                {
                    Individuos[peorPadre] = HijoA;
                    // Actualizamos el valor por si el HijoB también quiere entrar
                    valorPeorPadre = valorHijoA;
                }
                if (valorHijoB > valorPeorPadre)
                {
                    Individuos[peorPadre] = HijoB;
                }

                // MOSTRAR PROGRESO: Imprime la frase si mejora la puntuación anterior
                int maxHijo = Math.Max(valorHijoA, valorHijoB);
                if (maxHijo > mejorPuntajeGlobal)
                {
                    mejorPuntajeGlobal = maxHijo;
                    string mejorFraseActual = (valorHijoA > valorHijoB) ? HijoA : HijoB;
                    Console.WriteLine($"Ciclo {ciclo:N0} | Coincidencias: {mejorPuntajeGlobal}/{cadOriginal.Length} | {mejorFraseActual}");
                }

                // Control de éxito total
                if (mejorPuntajeGlobal == cadOriginal.Length)
                {
                    Console.WriteLine($"\n¡Objetivo alcanzado con éxito en el ciclo {ciclo}!");
                    break;
                }
            }

            // Busca y retorna el mejor de la población final
            int individuoMejor = 0;
            int puntajeMejor = -1;
            for (int cont = 0; cont < Individuos.Count; cont++)
            {
                int valorIndiv = objCad.EvaluaCadena(cadOriginal, Individuos[cont]);
                if (valorIndiv > puntajeMejor)
                {
                    individuoMejor = cont;
                    puntajeMejor = valorIndiv;
                }
            }
            return Individuos[individuoMejor];
        }
    }
}