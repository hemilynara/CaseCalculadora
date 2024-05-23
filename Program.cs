using System.Collections.Generic;
using System;
using System.Linq;

namespace Calculadora
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<Operacoes> filaOperacoes = new Queue<Operacoes>();
            filaOperacoes.Enqueue(new Operacoes { valorA = 2, valorB = 3, operador = '+' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 14, valorB = 8, operador = '-' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 5, valorB = 6, operador = '*' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 2147483647, valorB = 2, operador = '+' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 18, valorB = 3, operador = '/' });

            Calculadora calculadora = new Calculadora();
            Stack<long> pilhaResultados = new Stack<long>();

            Operacoes primeiraOperacao = filaOperacoes.Dequeue();
            calculadora.calcular(primeiraOperacao);
            pilhaResultados.Push((long)primeiraOperacao.resultado);

            try
            {
                while (filaOperacoes.Count > 0)
                {
                    Operacoes operacao = filaOperacoes.Dequeue();
                    try
                    {
                        calculadora.calcular(operacao);
                        Console.WriteLine("\nResultado: {0} {1} {2} = {3}", operacao.valorA, operacao.operador, operacao.valorB, (long)operacao.resultado);
                        if (filaOperacoes.Count > 0)
                        {
                            Console.WriteLine("\nOperações restantes na fila:");
                            foreach (var op in filaOperacoes)
                            {
                                Console.WriteLine("{0} {1} {2}", op.valorA, op.operador, op.valorB);
                            }
                        }
                        pilhaResultados.Push((long)operacao.resultado);
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine("Erro: {0}", ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro durante o processamento das operações: {0}", ex.Message);
            }

            Console.WriteLine("\nResultados processados:");
            int resultadoIndex = 0;
            foreach (var resultado in pilhaResultados)
            {
                if (resultado == 5)
                {
                    Console.WriteLine("Processado porém não visível: {0}", resultado);
                }
                else
                {
                    Console.WriteLine("{0}", resultado);
                    resultadoIndex++;
                }
            }
        }
    }
}




