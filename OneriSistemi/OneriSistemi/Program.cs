using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oneri_Sistemi
{
    class UrunCifti
    {
        public string urunAdi1, urunAdi2;
        public int sayi = 0;
    }

    class UrunUclusu
    {
        public string urunAdi1, urunAdi2, urunAdi3;
        public int sayi = 0;
    }

    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            string[] urunler = { "Ekmek", "Simit", "Peynir", "Tereyağı", "Zeytin", "Çay", "Makarna", "Bal", "Reçel", "Yumurta" };

            string[,] musteriSepeti = musteriSepetiOlustur(urunler, random);

            Console.WriteLine("Musteri Sepeti:");

            for (int i = 0; i < musteriSepeti.GetLength(0); i++)
            {
                Console.Write("{0}. Musterinin Aldiklari:", i + 1);

                for (int j = 0; j < musteriSepeti.GetLength(1); j++)
                {
                    if (musteriSepeti[i, j] != null)
                    {
                        Console.Write("{0} ", musteriSepeti[i, j]);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            UrunCifti[] urunCiftleri = urunCiftiKombinasyonlariOlustur(musteriSepeti);
            urunCiftleriniEkranaListele(urunCiftleri);
            Console.WriteLine();

            string[] aranan = new string[2];

            Console.WriteLine("Ilk Urünü Giriniz:");
            aranan[0] = Console.ReadLine();

            Console.WriteLine("Ikinci Urünün Giriniz:");
            aranan[1] = Console.ReadLine();
            Console.WriteLine();

            if (aranan[0].CompareTo(aranan[1]) == 1) // Alfabetik Olarak Duzenledik
            {
                string yedek = aranan[0];
                aranan[0] = aranan[1];
                aranan[1] = yedek;
            }

            arananiEkranaListele(musteriSepeti, aranan, urunCiftleri);
            Console.ReadKey();
        }

        static string[,] musteriSepetiOlustur(string[] urunler, Random random)
        {
            string[] urunlerKopya = new string[urunler.Length];
            string[,] musteriSepeti = new string[5, 5];

            for (int k = 0; k < 5; k++)
            {
                Array.Copy(urunler, urunlerKopya, urunler.Length);
                int maxDeger = urunlerKopya.Length;
                int sepetBoyutu = random.Next(1, 6);
                for (int i = 0; i < sepetBoyutu; i++)
                {
                    int randomNumber = random.Next(0, maxDeger);
                    musteriSepeti[k, i] = urunlerKopya[randomNumber];

                    for (int j = randomNumber; j < urunlerKopya.Length - 1; j++)
                    {
                        urunlerKopya[j] = urunlerKopya[j + 1];
                    }
                    maxDeger--;
                }
            }
            return musteriSepeti;
        }

        static UrunCifti[] urunCiftiKombinasyonlariOlustur(string[,] musteriSepeti)
        {
            UrunCifti[] urunCiftiDizisi = new UrunCifti[50];
            int index = 0;

            for (int k = 0; k < 5; k++)
            {
                for (int i = 0; i < musteriSepeti.GetLength(1) - 1; i++)
                {
                    for (int j = i + 1; j < musteriSepeti.GetLength(1); j++)
                    {
                        UrunCifti urunCifti = new UrunCifti();
                        if (musteriSepeti[k, j] != null)
                        {
                            urunCifti.urunAdi1 = musteriSepeti[k, i];
                            urunCifti.urunAdi2 = musteriSepeti[k, j];
                            if (urunCifti.urunAdi1.CompareTo(urunCifti.urunAdi2) == 1) // Alfabetik Olarak İkilileri Düzenledik
                            {
                                urunCifti.urunAdi2 = musteriSepeti[k, i];
                                urunCifti.urunAdi1 = musteriSepeti[k, j];
                            }
                            urunCiftiDizisi[index++] = urunCifti;
                        }

                    }
                }
            }

            for (int m = 0; m < urunCiftiDizisi.Length; m++) // Urun Ciftlerinden 1 den Fazla Olanlari Temizliyor Tekrar Yazdirilmamasi Icin
            {
                for (int b = 0; b < urunCiftiDizisi.Length; b++)
                {
                    if (urunCiftiDizisi[m] != null && urunCiftiDizisi[b] != null)
                    {
                        if (string.Equals(urunCiftiDizisi[m].urunAdi1, urunCiftiDizisi[b].urunAdi1) == true && string.Equals(urunCiftiDizisi[m].urunAdi2, urunCiftiDizisi[b].urunAdi2) == true)
                        {
                            urunCiftiDizisi[m].sayi++;
                            if (urunCiftiDizisi[m].sayi > 1)
                            {
                                Array.Clear(urunCiftiDizisi, b, 1);
                            }
                        }
                    }
                }
            }
            return urunCiftiDizisi;
        }

        private static void urunCiftleriniEkranaListele(UrunCifti[] urunCiftleri)
        {
            Console.WriteLine("Urun ciftleri:");
            int i = 0;
            do
            {
                if (urunCiftleri[i] != null)
                {
                    Console.Write("Urun Adi 1: {0}\nUrun Adi 2: {1}\nSayi: {2}\n", urunCiftleri[i].urunAdi1, urunCiftleri[i].urunAdi2, urunCiftleri[i].sayi);
                    Console.WriteLine();
                }
                i++;
            } while (i < 44);
        }

        private static void arananiEkranaListele(string[,] musteriSepeti, string[] aranan, UrunCifti[] urunCiftleri) //Girilen Urunlere Gore Oneri ve Guven Araligini Verir
        {
            UrunUclusu[] urunUcluleri = new UrunUclusu[15];
            int index = 0;

            for (int k = 0; k < 5; k++)
            {
                UrunUclusu ucluler = new UrunUclusu();

                for (int i = 0; i < musteriSepeti.GetLength(1) - 1; i++)
                {
                    if (musteriSepeti[k, i] != null)
                    {
                        if (string.Equals(aranan[0], musteriSepeti[k, i]) == true) // Girilen Urunden 1. sini Ariyor
                        {
                            for (int j = 0; j < musteriSepeti.GetLength(1) - 1; j++)
                            {
                                if (musteriSepeti[k, j] != null)
                                {
                                    if (string.Equals(aranan[1], musteriSepeti[k, j]) == true) // Girilen Urunden 2. sini Ariyor
                                    {
                                        ucluler.urunAdi1 = musteriSepeti[k, i];
                                        musteriSepeti[k, i] = null;
                                        ucluler.urunAdi2 = musteriSepeti[k, j];
                                        musteriSepeti[k, j] = null;

                                        for (int m = 0; m < musteriSepeti.GetLength(1) - 1; m++) //Bulursa Diger Uc Elemandan 1. sini Bakiyor
                                        {
                                            if (musteriSepeti[k, m] != null)
                                            {
                                                ucluler.urunAdi3 = musteriSepeti[k, m];
                                                urunUcluleri[index] = ucluler;
                                                musteriSepeti[k, m] = null;

                                                break;
                                            }
                                        }
                                        for (int p = 0; p < musteriSepeti.GetLength(1) - 1; p++) //Diger Uc Elemandan 2. sini Ariyor
                                        {
                                            if (musteriSepeti[k, p] != null)
                                            {
                                                UrunUclusu urunUclulerCopy = new UrunUclusu();
                                                index++;
                                                urunUclulerCopy.urunAdi1 = ucluler.urunAdi1;
                                                urunUclulerCopy.urunAdi2 = ucluler.urunAdi2;
                                                urunUclulerCopy.urunAdi3 = musteriSepeti[k, p];
                                                urunUcluleri[index] = urunUclulerCopy;
                                                musteriSepeti[k, p] = null;

                                                break;
                                            }
                                        }
                                        for (int x = 0; x < musteriSepeti.GetLength(1); x++) //Diger Uc Elemandan 3. sunu Ariyor
                                        {
                                            if (musteriSepeti[k, x] != null)
                                            {
                                                UrunUclusu urunUclulerCopy2 = new UrunUclusu();
                                                index++;
                                                urunUclulerCopy2.urunAdi1 = ucluler.urunAdi1;
                                                urunUclulerCopy2.urunAdi2 = ucluler.urunAdi2;
                                                urunUclulerCopy2.urunAdi3 = musteriSepeti[k, x];
                                                urunUcluleri[index] = urunUclulerCopy2;

                                                break;
                                            }
                                        }

                                        index++;
                                        if (index > 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int a = 0; a < urunUcluleri.Length - 1; a++) // Urun Uclulerinden Sayisi 1 den Fazla Olanları Temizliyor Tekrar Yazdirilmamasi Icin
            {
                for (int b = 0; b < urunUcluleri.Length - 1; b++)
                {
                    if (urunUcluleri[a] != null && urunUcluleri[b] != null)
                    {
                        if (string.Equals(urunUcluleri[a].urunAdi1, urunUcluleri[b].urunAdi1) == true && string.Equals(urunUcluleri[a].urunAdi2, urunUcluleri[b].urunAdi2) == true && string.Equals(urunUcluleri[a].urunAdi3, urunUcluleri[b].urunAdi3) == true)
                        {
                            urunUcluleri[a].sayi++;
                            if (urunUcluleri[a].sayi > 1)
                            {
                                Array.Clear(urunUcluleri, b, 1);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Onerilenler:");

            for (int r = 0; r < urunCiftleri.Length - 1; r++)
            {
                if (urunCiftleri[r] != null)
                {
                    if (string.Equals(aranan[0], urunCiftleri[r].urunAdi1) == true && string.Equals(aranan[1], urunCiftleri[r].urunAdi2) == true)
                    {
                        for (int e = 0; e < urunUcluleri.Length - 1; e++)
                        {
                            if (urunUcluleri[e] != null)
                            {
                                if ((string.Equals(aranan[0], urunUcluleri[e].urunAdi1) == true && string.Equals(aranan[1], urunUcluleri[e].urunAdi2) == true) || (string.Equals(aranan[0], urunUcluleri[e].urunAdi2) == true && string.Equals(aranan[1], urunUcluleri[e].urunAdi1) == true))
                                {
                                    Console.WriteLine("{0} , Guven: % {1}", urunUcluleri[e].urunAdi3, (urunUcluleri[e].sayi * 100) / urunCiftleri[r].sayi);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}