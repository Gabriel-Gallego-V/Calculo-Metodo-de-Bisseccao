using System;
using System.Collections.Generic;

class Program
{
    public static void Main(String[] args)
    {
        /*
          Criação dos parametros para numero de iterações, criação da lista vars, responsável por
          guardar os valores do intervalo fornecido, na estrutura {A, x, B} (Manter o x como 0 para a primeira
          iteração) 
        */
        int iter = 0;
        List<double> vars = new List<double> { -2.0, 0.0, 0.0 }; 
        List<double> funcRes = new List<double>();

        /*Função responsável por calcular a função fornecida e retornar o valor com no máximo 6
          casas após o 0
        */
        double execFunc(double funcX)
        {
            double res = Math.Pow(Math.E, funcX) - funcX - 2;
            return Math.Round(res, 6, MidpointRounding.AwayFromZero);
        }

        //Função responsável por encontrar o ponto em que as funções (funcRes) têm alteração no símbolo
        double achaSimbolo()
        {
            for (int j = 0; j < funcRes.Count - 1; j++)
            {
                //Valor de j é utilizado para buscar os valores nas listas disponíveis
                double funcAtual = Math.Round(funcRes[j], 6);
                double funcProx = Math.Round(funcRes[j + 1], 6);

                double varAtual = Math.Round(vars[j], 6);
                double varProx = Math.Round(vars[j + 1], 6);
                double varX = Math.Round((varAtual + varProx) / 2, 6);

                /*
                  Caso o valor da função atual (presente no valor j da lista), multiplicado
                  pela próxima função seja menor ou igual a 0, a lista de intervalos será
                  resetada, e os valores dos intervalos atuais serão inseridos na lista, na estrutura
                  {varAtual(A), varX(X), varProx(B)}
                */
                if (funcAtual * funcProx <= 0)
                {
                    vars.Clear();
                    vars.Add(varAtual);
                    vars.Add(varX);
                    vars.Add(varProx);
                    //Caso a iteração seja maior que zero, será printado o resultado
                    if (iter > 0)
                    {
                        Console.WriteLine("#===Va====#===Vx====#===Vb====#");
Console.WriteLine("|{0}|{1}|{2}|", 
    varAtual.ToString("0.######").PadLeft(8).PadRight(9), 
    varX.ToString("0.######").PadLeft(8).PadRight(9), 
    varProx.ToString("0.######").PadLeft(8).PadRight(9));
                        Console.WriteLine("#=========#=========#=========#");
                    }
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }
      
        //Resultado é inicialmente declarado como falso para manter o loop na ultima função
        bool resultado = false;

      //Função principal do programa
        double metodoBissex(double a, double b)
        {
            //Declaradas as variáveis para calcular o X e o Erro
            double x = ((a + b) / 2);
            double E = Math.Round(b - a, 6);

            /*
              Caso o Erro seja menor que 10^-3 (ou 1E-3, batendo o critério de parada), 
              os valores dos intervalos, funções e o valor atual de erro serão printados
            */
            if (E < 1E-3)
            {

              Console.WriteLine("#=========#=========#=========#");
              Console.WriteLine("#===Va====#===Vx====#===Vb====#");
              Console.WriteLine("|{0}|{1}|{2}|", 
                  vars[0].ToString("0.######").PadLeft(8).PadRight(9), 
                  vars[1].ToString("0.######").PadLeft(8).PadRight(9), 
                  vars[2].ToString("0.######").PadLeft(8).PadRight(9));
              Console.WriteLine("#=========#=========#=========#");
              Console.WriteLine("#===Fa====#===Fx====#===Fb====#");
              Console.WriteLine("|" + funcRes[0].ToString("0.######").PadLeft(8).PadRight(9) + "|" + funcRes[1].ToString("0.######").PadLeft(8).PadRight(9) + "|" + funcRes[2].ToString("0.######").PadLeft(8).PadRight(9) + "|");
              Console.WriteLine("#=========#=========#=========#");
              Console.WriteLine("#Valor final de erro: {0}#", E.ToString("0.#######"));
              Console.WriteLine("#===Fim===#===Do====#=Programa#");
              //resultado marcado como true para finalizar o loop
              resultado = true;
            }
            /*
              Caso o Erro seja maior, será calculado o valor de fa, fx e fx utilizando
              a função execFunc. Após isso a lista de funções será resetada e os valores
              de função serão inseridos na estrutura 
              {fa(Resultado Função A), fx(Resultado Função X), fb(Resultado Função B)}
            */
            else
            {
                double fa = execFunc(a);
                double fx = execFunc(x);
                double fb = execFunc(b);
                funcRes.Clear();
                funcRes.Add(Math.Round(fa, 6));
                funcRes.Add(Math.Round(fx, 6));
                funcRes.Add(Math.Round(fb, 6));
                //Caso a iteração seja maior que 0, o valor das funções será printado na tela
                if (iter > 0)
                {
                  Console.WriteLine("#=========#=========#=========#");
                  Console.WriteLine("#VAL_ERRO=#"+ E.ToString("0.#######").PadLeft(5).PadRight(9) +"#=VAL_ERRO#");
                    Console.WriteLine("#===Fa====#===Fx====#===Fb====#");
                    Console.WriteLine("|" + fa.ToString("0.######").PadLeft(8).PadRight(9) + "|" + fx.ToString("0.######").PadLeft(8).PadRight(9) + "|" + fb.ToString("0.######").PadLeft(8).PadRight(9) + "|");
                  Console.WriteLine("#=========#=========#=========#");
                }
                achaSimbolo();
            }
            return 0;
        }
        /*
          Enquanto o resultado for falso, será printado a quantidade de iterações, a função metodoBissex
          será chamada utilizando os valores A e B da lista de intervalos.
        */
        while (resultado == false)
        {
            if (iter > 0)
            {
              Console.WriteLine("#=========#=========#=========#");
              Console.WriteLine("#iterações#{0}#iterações#", iter.ToString("##").PadLeft(5).PadRight(9));
            }
            metodoBissex(vars[0], vars[2]);
            Console.WriteLine("\n \n");
            Console.WriteLine("");
            //Caso o loop não seja quebrado, incrementa o número de iterações
            iter++;
        }
    }
}

/*
  No caso de dúvidas, o valor de iterações é setado como 0 inicialmente apenas para fazer o primeiro cálculo
  e encontrar o valor de X e das funções antes de iniciar as iterações (Provavelmente o cálculo pode quebrar caso
  esse valor seja alterado)
*/