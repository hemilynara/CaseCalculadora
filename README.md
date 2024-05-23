# CaseCalculadora

Este projeto é uma aplicação de calculadora implementada em C#. A calculadora realiza operações básicas (adição, subtração, multiplicação e divisão) em uma fila de operações e armazena os resultados em uma pilha.

## Estrutura do Projeto

O projeto consiste em três arquivos principais:

1. `Calculadora.cs` - Define a classe `Calculadora` com métodos para realizar as operações.
2. `Operacoes.cs` - Define a classe `Operacoes` que representa cada operação a ser realizada.
3. `Program.cs` - Contém o ponto de entrada do programa, onde as operações são processadas.

## Funcionalidades Implementadas

- **Adição**
- **Subtração**
- **Multiplicação**
- **Divisão**

## Mudanças e Correções Realizadas

### 1. Corrigir Loop Infinito

O problema original era que a aplicação estava processando o primeiro item da fila infinitamente. Isso foi corrigido substituindo `filaOperacoes.Peek()` por `filaOperacoes.Dequeue()`, que remove o item processado da fila:

```csharp
while (filaOperacoes.Count > 0)
{
    Operacoes operacao = filaOperacoes.Dequeue();
    // Processar a operação
}
```

### 2. Implementar Funcionalidade de Divisão
Foi adicionada a funcionalidade de divisão na classe Calculadora com um método divisao. Também foi tratado o caso de divisão por zero para evitar exceções:
```csharp
    public decimal divisao(Operacoes operacao)
    {
        if (operacao.valorB == 0)
        {
            throw new DivideByZeroException("Divisão por zero não é permitida.");
        }
        return Math.Round((decimal)operacao.valorA / operacao.valorB);
    }
}
```
### 3. Ajustar Tipos de Dados
Para garantir que valores grandes e operações fracionárias sejam manipulados corretamente, os tipos de dados foram ajustados:

- valorA e valorB foram alterados de int para long.
- resultado foi mantido como decimal.

### 4. Armazenar Resultados em uma Pilha
Uma nova pilha (**`Stack<long>`**) foi criada para armazenar os resultados de cada operação. Após cada cálculo, o resultado é empilhado. Além disso, a pilha de resultados é impressa ao final do processamento das operações.
```csharp
Stack<long> pilhaResultados = new Stack<long>();

Operacoes primeiraOperacao = filaOperacoes.Dequeue();
calculadora.calcular(primeiraOperacao);
pilhaResultados.Push((long)primeiraOperacao.resultado);
```
###  5. Tratamento de Exceções com Try-Catch
Foi adicionado um bloco try-catch para capturar exceções durante o processamento das operações na fila.

```csharp
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
```

### 6. Funcionalidade: Impressão da Lista de Operações Pendentes:

Adicionada a funcionalidade para imprimir toda a lista de operações a ser processada após cada cálculo realizado. As operações pendentes são exibidas como "Operações restantes na fila" e mostram os valores de cada operação pendente.
```csharp
if (filaOperacoes.Count > 0)
{
    Console.WriteLine("\nOperações restantes na fila:");
    foreach (var op in filaOperacoes)
    {
        Console.WriteLine("{0} {1} {2}", op.valorA, op.operador, op.valorB);
    }
}
```

### Execução do Programa
Para executar o programa, compile e execute o arquivo `Program.cs`. A saída será os resultados das operações seguidas dos resultados armazenados na pilha.

```
Resultado: 14 - 8 = 6

Operações restantes na fila:  
5 * 6  
2147483647 + 2  
18 / 3  

Resultado: 5 * 6 = 30

Operações restantes na fila:  
2147483647 + 2  
18 / 3  

Resultado: 2147483647 + 2 = 2147483649

Operações restantes na fila:  
18 / 3  

Resultado: 18 / 3 = 6  

Resultados processados:  
6  
2147483649  
30  
6  
Processado porém não visível: 5  
```

### Observações
- A implementação atual suporta operações básicas com tratamento para divisão por zero.
- A precisão dos resultados é garantida pelo uso do tipo decimal.
