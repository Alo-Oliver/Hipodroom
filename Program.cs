using System;

namespace _60MinutesSpinoff
{
    class Program
    {
        //global muutujad
        //hobuste nimed
        public static string hobune1 = "VÄLK";
        public static string hobune2 = "PAUK";
        public static string hobune3 = "SILLER";
        public static string hobune4 = "RAKETT";

        //võiduvõimalused
        public static int hobune1odds;
        public static int hobune2odds;
        public static int hobune3odds;
        public static int hobune4odds;

        //raha
        public static int raha = 100;

        //kontrolli jaoks kas kasutaja sisestas Y/N vajalik
        public static string checking;

        static void Main(string[] args)
        {
            //kirjutab programmi info välja
            ProgeInfo();

            //loop, et mängu saaks otsast alustada.
            bool check = true;
            while (check == true)
            {
                //näitab raha
                Console.WriteLine("Sinu Raha:"+ raha);
                //võiduvõimaluste määramamine
                Hobused();

                //panustamine
                int panuseSumma = 0;
                string panuseNimi = "";

                //saab hobuse nime järgi valida
                VarvilineSonum(ConsoleColor.Cyan, "Vali hobune!");
                panuseNimi = Console.ReadLine();
                panuseNimi = panuseNimi.ToUpper(); //viib sisendi upperiks, et matching oleks korrektne
                
                //kontrollid, et sisend oleks õige
                bool checks = true;
                while (checks == true)
                {
                    //kontrollib sisestatud hobuse nime
                    if (panuseNimi != hobune1 && panuseNimi != hobune2 && panuseNimi != hobune3 && panuseNimi != hobune4)
                    {
                        VarvilineSonum(ConsoleColor.Red, "Sellist hobust ei eksisteeri!");
                        VarvilineSonum(ConsoleColor.Cyan, "Vali hobune!");
                        panuseNimi = Console.ReadLine();
                        panuseNimi = panuseNimi.ToUpper();
                        continue;
                    }

                    //saab summa valida
                    VarvilineSonum(ConsoleColor.Cyan, "Vali panustatav summa!");
                    string summaKontroll = Console.ReadLine();

                    //kontrollib kas sisestatud summa on number
                    try
                    {
                        panuseSumma = Int32.Parse(summaKontroll);
                    }
                    catch (Exception)
                    {
                        VarvilineSonum(ConsoleColor.Red, "Sisesta number!");
                        continue;
                    }

                    //kontrollib et panus ei oleks null, negatiivne või rohkem kui mängijal raha on
                    if (panuseSumma > raha || panuseSumma <= 0)
                    {
                        VarvilineSonum(ConsoleColor.Red, "Sisestatud summa ei ole korrektne!");
                        continue;
                    }
                    checks = false;

                }

                //Mängija panus loetakse sisse ja arvutatakse võitja.
                Panusta(panuseNimi, panuseSumma);
                
                //kui mängija on pankrottis kuvab seda:
                if (raha <= 0)
                {
                VarvilineSonum(ConsoleColor.Red, "Oled pankrotis, mäng läbi!");
                VarvilineSonum(ConsoleColor.Cyan, "Alusta mängu otsast peale? Y/N");
                checking = Console.ReadLine();
                    Kontroll(checking.ToUpper());
                    if (checking.ToUpper() == "N")
                    {
                        Environment.Exit(0);
                    }
                    else if (checking.ToUpper() == "Y") 
                    {
                        raha = 100;
                        continue;
                    }
                }

                //kui mängija ei ole pankrottis kuvab seda:
                if (raha > 0)
                {
                    VarvilineSonum(ConsoleColor.Cyan, "Panusta veel? Y/N");
                    checking = Console.ReadLine();

                    //kontrollib et kasutaja oleks sisestanud Y või N
                    Kontroll(checking.ToUpper());

                    if (checking.ToUpper() == "N")
                    {
                        Environment.Exit(0);
                    }
                    else if (checking.ToUpper() == "Y")
                    {
                        continue;
                    }
                    
                }

            }   
        }

        static void Kontroll(string chk)
        {
            checking = chk;
            //väike loop, kui kasutaja ei oska Y või N sisestada, siis küsib uuesti
            bool asking = true;
            while (asking == true)
            {
                if (checking == "Y" || checking == "N")
                {
                    asking = false;
                }
                else
                {
                    //Veateade Värvus funktsiooni abil
                    VarvilineSonum(ConsoleColor.Red, "Sisesta täht Y - Jah või N - ei");
                    //uus sisend
                    checking = Console.ReadLine().ToUpper();
                }
            }
        }


        //Funktsioon konsooli sõnumi värvi muutmiseks
        static void VarvilineSonum(ConsoleColor Color, string Sonum)
        {
            Console.ForegroundColor = Color; //värvi määramine
            Console.WriteLine(Sonum); //sõnumi print
            Console.ResetColor(); //värvi reset
        }

        //programmi info ja selle välja printimine
        static void ProgeInfo()
        {
            //proge muutujad
            string progeVers = "1.0.0";
            string autor = "Alo-Oliver Alas";
            string progeNimi = "Hipodroom";
            //prindib funktsiooni abil üleval määratud info
            VarvilineSonum(ConsoleColor.Yellow, progeNimi + " " + progeVers +" Autor: "+ autor);
        }

        //loome hobused ja nende võiduvõimalused
        static void Hobused()
        {

            //Määrame suvaliselt hobuste võiduvõimalused
            Random odds = new Random();
            hobune1odds = odds.Next(1, 5);
            hobune2odds = odds.Next(1, 5);
            hobune3odds = odds.Next(1, 5);
            hobune4odds = odds.Next(1, 5);

            //Teeb kindlaks, et igal hobusel oleks erinevad võiduvõimalused, kui ei ole, siis annab uue väärtuse
            bool uniq = true;
            while (uniq == true)
            {
                if (hobune1odds == hobune2odds)
                {
                    hobune2odds = odds.Next(1, 5);
                }
                else if (hobune1odds == hobune3odds || hobune2odds == hobune3odds)
                {
                    hobune3odds = odds.Next(1, 5);
                }
                else if (hobune1odds == hobune4odds || hobune2odds == hobune4odds || hobune3odds == hobune4odds)
                {
                    hobune4odds = odds.Next(1, 5);
                }
                else 
                {
                    //kirjutab võiduvõimalused välja
                    Console.WriteLine("Hobuste võiduvõimalused:");
                    Console.WriteLine(hobune1 + ": 1/" +hobune1odds + " " + hobune2 + ": 1/" + hobune2odds + " " + hobune3 + ": 1/" + hobune3odds + " " + hobune4 + ": 1/" + hobune4odds);
                    uniq = false;
                }
            }   
        }

        //paneme panuse, vaatame kumb hobune võidab ja väljastame panuse.
        static void Panusta(string hobuseNimi, int summa)
        {
            //võiduvõimalused, väiksem = väiksemad võimalused
            string upperNimi = hobuseNimi.ToUpper();
            int tempOdd1 = 24 / hobune1odds;
            int tempOdd2 = 24 / hobune2odds + tempOdd1;
            int tempOdd3 = 24 / hobune3odds + tempOdd2;
            int tempOdd4 = 24 / hobune4odds + tempOdd3;

            //valib võitja
            Random win = new Random();
            int winner = win.Next(1, 51);

            //kui õigesti arvatud, siis arvutab tagastatud panuse ja lisab rahale kui ei, siis lahutab panuse maha.
            if(tempOdd1 >= winner && hobune1 == upperNimi)
            {
                raha += summa * hobune1odds;
                Console.WriteLine(hobune1 + " on Võitja! Sa võitsid " + summa * hobune1odds);
            }
            else if (tempOdd2 >= winner && hobune2 == upperNimi)
            {
                raha += summa * hobune2odds;
                Console.WriteLine(hobune2 + " on Võitja! Sa võitsid " + summa * hobune2odds);
            }
            else if (tempOdd3 >= winner && hobune3 == upperNimi)
            {
                raha += summa * hobune3odds;
                Console.WriteLine(hobune3 + " on Võitja! Sa võitsid " + summa * hobune3odds);
            }
            else if (tempOdd4 >= winner && hobune4 == upperNimi)
            {
                raha += summa * hobune4odds;
                Console.WriteLine(hobune4 + " on Võitja! Sa võitsid " + summa * hobune4odds);
            }
            else
            {
                if (tempOdd1 >= winner)
                {
                    Console.WriteLine(hobune1 + " on Võitja!");
                }
                else if (tempOdd2 >= winner)
                {
                    Console.WriteLine(hobune2 + " on Võitja!");
                }
                else if (tempOdd3 >= winner)
                {
                    Console.WriteLine(hobune3 + " on Võitja!");
                }
                else if (tempOdd4 >= winner)
                {
                    Console.WriteLine(hobune4 + " on Võitja!");
                }
                Console.WriteLine("Sa kaotasid " + summa);
                raha -= summa;
            }
        }
    }
}
