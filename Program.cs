using System;
using System.Linq;
using System.Collections.Generic;

namespace Stats
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("Naber Lan :-)) ");
            Console.WriteLine("");

            double[] dizi={8.04 , 6.95 , 7.58 , 8.81 , 8.33 , 9.96 , 7.24 , 4.26 , 10.84 , 4.82 , 5.68};

            try
            {
                    double[] sonuc=hesapla(dizi);


                    Console.WriteLine("G: "+sonuc[0].ToString());
                    Console.WriteLine("O: "+sonuc[1].ToString());
                    Console.WriteLine("DLeft: "+sonuc[2].ToString());
                    Console.WriteLine("DRight: "+sonuc[3].ToString());

  

            }
            catch  (Exception ex)
            {
                    Console.WriteLine("Hata: "+ex.Message);
                    Console.ReadLine();

            }


        }

        static double[] hesapla(double[] dizi)
        {
            double G, O, DLeft, DRight;
            double[] result=new double[4];


            int N=dizi.Length;
            double med=getMedian(dizi);
            Array.Sort(dizi);
            double[] normX=new double[dizi.Length];
            
            for(int i=0;i<dizi.Length;i++)
            {
                 normX[i]=dizi[i]-med;
            }

            List<int> k4neg=new List<int>();
            List<int> k4pos=new List<int>();

            for(int i=0;i<normX.Length;i++)
            {
                  if(normX[i]<0)
                  {
                       k4neg.Add(i);
                  }
                  else
                  {
                       k4pos.Add(i);
                  }

            }


            G= normX.Where(a=>a<0).Sum() / normX.Where(a=>a>=0).Sum();
            double gr=(1+Math.Sqrt(5))/2;    /////  % The Golden Ratio
            List<double> Mci=new List<double>();

            for(int i=0;i<k4neg.Count;i++)
            {
                     Mci.Add((1/gr)+2*(k4neg[i]/Convert.ToDouble(N-1)));
            }

            for(int i=0;i<k4pos.Count;i++)
            {
                     Mci.Add((1+gr)-2*(k4pos[i]/Convert.ToDouble(N-1)));
            }


            O=med;

            for (int i=0;i<Mci.Count;i++)
            {
                 O=O+((Mci[i]*normX[i])/Mci.Sum());

            }
        ////////////////////////////////////
            for(int i=0;i<dizi.Length;i++)
            {
                 normX[i]=dizi[i]-O;
            }

            k4neg.Clear();
            k4pos.Clear();

            for(int i=0;i<normX.Length;i++)
            {
                  if(normX[i]<0)
                  {
                       k4neg.Add(i);
                  }
                  else
                  {
                       k4pos.Add(i);
                  }

            }
            
            int K= k4neg.Count;
            /////////////////////////////////////

            List<double> Dci=new List<double>();

            for(int i=0;i<k4neg.Count;i++)
            {
                     Dci.Add(gr-(k4neg[i]/Convert.ToDouble(K-1)));
            }

            for(int i=0;i<k4pos.Count;i++)
            {
                     Dci.Add((gr-1)+((k4pos[i]-K)/Convert.ToDouble(N-K-1)));
            }
            


            double DLeftUp=0;
            double DLeftDown=0;

            for(int i=0;i<k4neg.Count;i++)
            {
                     DLeftUp=DLeftUp+(Dci[k4neg[i]]*normX[k4neg[i]]);
                     DLeftDown=DLeftDown+Dci[k4neg[i]];
            }

            DLeft=DLeftUp/DLeftDown;

            double DRightUp=0;
            double DRightDown=0;

            for(int i=0;i<k4pos.Count;i++)
            {
                     DRightUp=DRightUp+(Dci[k4pos[i]]*normX[k4pos[i]]);
                     DRightDown=DRightDown+Dci[k4pos[i]];
            }

            DRight=DRightUp/DRightDown;
           ///////////////////////////////
           result[0]=G;
           result[1]=O;
           result[2]=DLeft;
           result[3]=DRight;


            return result;
        }

        static double getMedian(double[] dizi)
        {
                   double result=0;
                   Array.Sort(dizi);

                   if(dizi.Length%2==1)
                   {
                        result=dizi[(dizi.Length-1)/2];

                   }
                   else
                   {
                        int ind=dizi.Length/2;
                        result=(dizi[ind-1] +dizi[ind] ) /2.0;

                   }

                   return result;

        }

    }
}
