using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3_Dama
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;

            ConsoleKeyInfo command;

            int cursorX = 5;
            int cursorY = 5;

            int TempX;
            int TempY;
            int TempZ;

            int TempX_2;
            int TempY_2;
            int TempZ_2;

            int satır;
            int sütun;
            int yeni_satır;
            int yeni_sütun;


            int[,] board = new int[,]

            {
               { 0, 0, 0, 1, 1, 1, 1, 1 },
               { 0, 0, 0, 1, 1, 1, 1, 1 },
               { 0, 0, 0, 1, 1, 1, 1, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1,},
               { 1, 1, 1, 1, 1, 1, 1, 1,},
               { 1, 1, 1, 1, 1, 2, 2, 2 },
               { 1, 1, 1, 1, 1, 2, 2, 2 },
               { 1, 1, 1, 1, 1, 2, 2, 2 },
             };


            //burda o ları '0', . ları '1', x leri '2'  olarak aldım.
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.SetCursorPosition(x + i, y);
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.SetCursorPosition(x + i, y + j);

                    switch (board[i, j])
                    {
                        case 0:
                            Console.WriteLine("o");
                            break;
                        case 1:
                            Console.WriteLine(".");
                            break;
                        case 2:
                            Console.WriteLine("x");
                            break;
                    }
                }
            }


            TempX = cursorX;
            TempY = cursorY;
            TempZ = board[TempX, TempY];
            bool TempZbool = false;
            bool TempZ_2bool = false;



            while (true)
            {
                Console.SetCursorPosition(cursorX, cursorY);
                if (Console.KeyAvailable)
                {
                    command = Console.ReadKey(true);
                    switch (command.Key)
                    {
                        case ConsoleKey.RightArrow:
                            //cursorX = cursorX + 1;
                            if (cursorX >= 7) // boundary control
                            {
                                cursorX -= 7;
                            }
                            else
                            {
                                cursorX++;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            //key control
                            if (cursorX == 0) // boundary control
                            {
                                cursorX += 7;
                            }
                            else
                            {
                                cursorX--;

                            }
                            break;
                        case ConsoleKey.UpArrow:
                            //key control
                            if (cursorY == 0) // boundary control
                            {
                                cursorY += 7;
                            }
                            else
                            {
                                cursorY--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            //key control
                            if (cursorY == 7) // boundary control
                            {
                                cursorY -= 7;
                            }
                            else
                            {
                                cursorY++;
                            }
                            break;
                        case ConsoleKey.Z://z ye basıldığında cursor'un o anki bastığındaki konumundaki x'i hafızaya alıyor TempX ve TempY diyerek x ve y eksenindeki konumunu alıyor. Daha sonra TemZ de bunu arraydeki elemanını alıyor
                            TempX = cursorX;
                            TempY = cursorY;

                            TempZ = board[TempX, TempY];
                            TempZbool = true;
                            break;
                        case ConsoleKey.X:  //x e basıldığında önce TempZ bool'unu kontrol ediyor çünkü öncesindee z ye basılmadıysa işe yaramasın x'e basmak diye.
                            if (TempZbool == true)
                            {
                                if (TempZ == 2) //x'e eşitse
                                {//alttaki if  hafızaya alındıktan sonra cursor'u hareket ettiriyosun ya en son x e basıp taşın atlamasını sağlayacağı yerin hafızadan istenilen uzaklıkta olup olmadığını kontrol etmek için. Yani 1 birim atlayıp atlamadığını kontrol için.
                                    if ((cursorX == TempX + 1 && cursorY == TempY) || (cursorX == TempX - 1 && cursorY == TempY) || (cursorY == TempY + 1 && cursorX == TempX) || (cursorY == TempY - 1 && cursorX == TempX))
                                    {
                                        if (board[cursorX, cursorY] != 0 && board[cursorX, cursorY] != 2)// bu if ise x e bastığın yerde  o veya x olup olmadığına bakıyor eğer varsa taşı atamıyor oraya
                                        {// önce cw (console.writeline) ile x i oraya yapıştırıyor. sonra yapıştırdığı yeri artık arraydde 2 olarak atıyor. sonra eski yerini de nokta olarak belirliyor yani 1 olarak.
                                            Console.WriteLine("x");
                                            board[cursorX, cursorY] = 2;
                                            board[TempX, TempY] = 1;
                                            Console.SetCursorPosition(TempX, TempY);
                                            Console.WriteLine(".");
                                            Console.SetCursorPosition(cursorX, cursorY);
                                            TempZbool = false;// burda da bunu yazmamın sebebi sürekli x e basıp yer değiştirme olmasın diye.
                                        }
                                    }
                                }

                                //Şunu yapamadım kullanıcı z ye basıp sonra bizim taşımızı atlattıktan sonra isterse başka taşa basıp gene haret ettiriyor onu engelleyemedim. sadece c ye basarsa pc otomatik taş oynatıyor ancak kullanıcın c ye basması lazım yoksa olmuyor.
                            }
                            break;
                        case ConsoleKey.C:
                            if (TempZbool == false)
                            {
                                while (TempZ_2bool == false)//pc arrayden satır ve sütun seçiyor. Sonra onu alttaki if ile 0 olup olmadığını kontro ediyor. eğer 0ise ve etrafında herhangi bir taş 1 ise o taşı oynatabilir.
                                                            //ama array sınırların dışında diye hata veriyor
                                {
                                    Random a = new Random();
                                    satır = a.Next(0, 9);
                                    sütun = a.Next(0, 9);
                                    //alttaki ifte sorun veriyor ona bakıver.
                                    if ((board[satır, sütun] == 0) && ((board[satır + 1, sütun] == 1) | (board[satır - 1, sütun] == 1) | (board[satır, sütun + 1] == 1) | (board[satır, sütun - 1] == 1)))
                                    {// eğer üst tarafı düzelttiysen altta o satır ve sütunu gene z de yaptığımız gibi hafızaya alıyor (TempX_2  şeklinde flan).
                                        TempX_2 = satır;
                                        TempY_2 = sütun;

                                        TempZ_2 = board[TempX_2, TempY_2];
                                        TempZ_2bool = true;

                                        while (TempZ_2bool == true)
                                        {// burda da seçtiğimiz taşın altında üstünde veya sağında solundan rastgele arrayin elemanını seçiyor  alttaki if ile de seçtiği kısım nokta mı diye kontrol ediyor.
                                         // eğer noktaysa x'e bastığımızdaki kısımların aynısı gerçekleşiyor.
                                            Random b = new Random();
                                            yeni_satır = b.Next(TempX_2 - 1, TempX_2 + 2);
                                            yeni_sütun = b.Next(TempY_2 - 1, TempY_2 + 2);

                                            if (board[yeni_satır, yeni_sütun] == 1)
                                            {
                                                Console.WriteLine('o');
                                                board[yeni_satır, yeni_sütun] = 0;
                                                board[TempX_2, TempY_2] = 1;
                                                Console.SetCursorPosition(TempX_2, TempY_2);
                                                Console.WriteLine(".");
                                                TempZ_2bool = false;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }


                            break;
                    }//switch console key
                }//console.KeyAvileable
            }//while döngüsü







        }
    }
}
