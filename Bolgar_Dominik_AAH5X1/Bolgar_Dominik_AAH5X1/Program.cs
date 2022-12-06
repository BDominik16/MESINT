using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;


Console.WriteLine("Kérem a munkák számát");
int munkak = int.Parse(Console.ReadLine());
Console.WriteLine("Kérem a gépek számát:");
int gepek = int.Parse(Console.ReadLine());
int[,] t = new int[munkak, gepek];
Random rnd = new Random();
for (int i = 0; i < t.GetLength(0); i++)
{
    for (int j = 0; j < t.GetLength(1); j++)
    {
        t[i, j] = rnd.Next(1, 10);
    }
}


int[] sorba = new int[t.GetLength(0)];

for (int i = 0; i < sorba.Length; i++)
{
    sorba[i] = i;
}

sorba = sorba.OrderBy(x => rnd.Next()).ToArray();


for (int i = 0; i < t.GetLength(0); i++)
{
    for (int j = 0; j < t.GetLength(1); j++)
    {
        Console.Write($"{t[i, j]},");
    }
    Console.WriteLine("");
}

List<int[]> list = new List<int[]>();




int iteracio = 1000;                             
int tabumax = 30;
int[] mostsor = new int[0];
List<int[]> tabu = new List<int[]>();
int max = 0;
for (int i = 0; i < iteracio; i++)
{
    List<int[]> szomszed = new List<int[]>();
     mostsor = sorba;
    list = szomszedok(mostsor);
    for (int j = 0; j < szomszed.Count; j++)
    {
        if (keres2(szomszed[j], tabu))
        {

            szomszed.Remove(szomszed[j]);
        }
    }
      max = eredmeny(t,  mostsor);
    foreach (var j in szomszed)
    {
        int a = eredmeny(t, j);
        if (a < max)
        {
            max = a;
            mostsor = j;
        }
    }
    tabu.Add(mostsor.ToArray());
    if (tabu.Count > tabumax)
    {
        for (int j = 0; j < tabumax; j++)
        {
            tabu.Remove(tabu[0]);
        }
    }

}
Console.WriteLine("A megoldás sora: ");
foreach (var i in mostsor)
{
    Console.Write($"{i+1},");
}
Console.WriteLine("");
Console.WriteLine($"Max érték: {max}");

static List<int[]> szomszedok(int[] mostanisor)
{
    int a;
    List<int[]> szomszed = new List<int[]>();
    
    for (int i = 0; i < mostanisor.Length; i++)
    {
        for (int j = i + 1; j < mostanisor.Length; j++)
        {
            a = mostanisor[i];
            mostanisor[i] = mostanisor[j];
            mostanisor[j] = a;
            szomszed.Add(mostanisor.ToArray());
            a = mostanisor[i];
            mostanisor[i] = mostanisor[j];
            mostanisor[j] = a;
        }
    }
    return szomszed;
   
}



static int[,] sorcsere(int[,] eredeti, int[] sor)
{

    int[,] valasz = new int[eredeti.GetLength(0), eredeti.GetLength(1)];
    for (int i = 0; i < eredeti.GetLength(0); i++)
    {
        for (int j = 0; j < eredeti.GetLength(1); j++)
        {
            valasz[i, j] = eredeti[sor[i], j];
        }

    }
    return valasz;
}
static int eredmeny(int[,] gepfel, int[] soroz)
{
    gepfel = sorcsere(gepfel, soroz);
    int[,] befejezes = new int[gepfel.GetLength(0), gepfel.GetLength(1)];
    int[,] varakozas = new int[gepfel.GetLength(0), gepfel.GetLength(1)];

    for (int oszlop = 0; oszlop < gepfel.GetLength(1); oszlop++)
    {
        for (int sor = 0; sor < gepfel.GetLength(0); sor++)
        {
            if (oszlop == 0)                                                                                 //az első oszlop generálás
            {
                int osz = 0;
                for (int k = sor; k >= 0; k--)
                {

                    osz += gepfel[k, oszlop];

                }
                befejezes[sor, oszlop] = osz;
            }
            else
            {
                int osz = befejezes[0, oszlop - 1];                                                              //mindig azzal kezdünk amivvel az elzö 1. feladat befejezési ideje 
                int osz2 = 0;
                int varakoztat = 0;
                int beflast = 0;
                for (int k = 0; k <= sor; k++)
                {
                    osz += gepfel[k, oszlop];
                    if (k != gepfel.GetLength(0) - 1 && sor != 0)
                    {
                        beflast = befejezes[k + 1, oszlop - 1];
                        osz2 = osz;
                        varakoztat = befejezes[k + 1, oszlop - 1] - osz;
                        if (befejezes[k + 1, oszlop - 1] - varakozas[k + 1, oszlop - 1] > osz)
                        {
                            osz = befejezes[k + 1, oszlop - 1];
                        }
                    }
                    // Console.WriteLine("");

                }
                //Console.WriteLine("");
                if (osz == beflast) varakozas[sor, oszlop] = beflast - osz2;
                befejezes[sor, oszlop] = osz;
            }

        }
    }


    return befejezes[befejezes.GetLength(0)-1,befejezes.GetLength(1)-1];

}

static bool keres2(int[] alap, List<int[]> tabukeres)
{
    return tabukeres.Contains(alap);
}