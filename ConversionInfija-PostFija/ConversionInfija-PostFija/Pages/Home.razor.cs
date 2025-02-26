using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace ConversionInfija_PostFija
{
    public partial class HomeComponent : ComponentBase
    {
        protected string expresionInfija = "";
        protected string expresionPostfija = "";

        protected double result = 0;

        private static readonly Dictionary<char, int> PrioridadOperadores = new()
    {
        {'+', 1}, {'-', 1}, {'*', 2}, {'/', 2}
    };

        protected void ConvertirAPostfija()
        {
            expresionPostfija = ConvertirAPostfija(expresionInfija);
        }

        // Metodo para evaludar expresion de actividad 2.
        protected void EvaluarPostfija()
        {
            result = EvaluarExpresionPostfija(expresionPostfija);
        }

        private static string ConvertirAPostfija(string expresion)
        {
            Stack<char> pila = new();
            return ProcesarExpresion(expresion, 0, pila, "");
        }

        private static string ProcesarExpresion(string expresion, int indice, Stack<char> pila, string postfija)
        {
            if (indice >= expresion.Length)
            {
                while (pila.Count > 0)
                    postfija += pila.Pop();
                return postfija;
            }

            char exp = expresion[indice];
            if (char.IsLetterOrDigit(exp)) // Valida si es un operando.
            {
                return ProcesarExpresion(expresion, indice + 1, pila, postfija + exp);
            }
            else if (exp == '(') // Valida si es un parentesis izquierdo.
            {
                pila.Push(exp);
            }
            else if (exp == ')') // Valida sii es un parentesis derecho.
            {
                while (pila.Count > 0 && pila.Peek() != '(')
                    postfija += pila.Pop();
                pila.Pop(); // Desapilar el '('
            }
            else // Si es un operador
            {
                while (pila.Count > 0 && pila.Peek() != '(' && PrioridadOperadores[pila.Peek()] >= PrioridadOperadores[exp])
                    postfija += pila.Pop();
                pila.Push(exp);
            }

            return ProcesarExpresion(expresion, indice + 1, pila, postfija);
        }

        // Metodo para evaluar expresion de actividad 2.
        private static double EvaluarExpresionPostfija(string expresion)
        {
            Stack<double> pila = new();

            foreach (char c in expresion)
            {
                if (char.IsDigit(c)) // Si es un operando, apilarlo
                {
                    pila.Push(c - '0'); // Convertir carácter a número
                }
                else // Si es un operador
                {
                    double op2 = pila.Pop();
                    double op1 = pila.Pop();
                    double resultado = 0;

                    switch (c)
                    {
                        case '+': resultado = op1 + op2; break;
                        case '-': resultado = op1 - op2; break;
                        case '*': resultado = op1 * op2; break;
                        case '/': resultado = op1 / op2; break;
                    }

                    pila.Push(resultado);
                }
            }

            return pila.Pop(); // Ultimo elemento es el resultado final
        }
    }
}
