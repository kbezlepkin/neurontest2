using System;

namespace neurontest2
{
    class Program
    {
        public class Neuron //класс нейрона
        { 
            public float weight11 = 0.5f, weight2 = 0.5f,//вес на входах нейрона
                            weight3 =  0.5f, weight4 = 0.5f;

            public void Activefunction(int input1, int input2, out float resultexit1, out float resultexit2) //функция активации нейрона
            {
                /*пример функции активации нейрона: перевод скорости мотоцикла в м/с*/
                resultexit1 = (weight11 * input1) / (input2 * weight2);
                resultexit2 = (weight3 * input1) / (weight4 * input2);
            }


            public bool Cikleobucheniya(int input1, int input2, float gr1, float gr2, bool ef) //функция обучения нейронки
            {
                float delta1, delta2; //погрешность ответа на выходе

                for (int iteraciya = 0; iteraciya < 10000 /*количество итераций*/; iteraciya++) //метод дял ограниченного количества итераций
                {
                    Activefunction(input1, input2, out float curresult1, out float curresult2);//где curresult1, curresult2 текущий результат
                    delta1 = gr1 - curresult1; //погрешность измерений
                    delta2 = gr2 - curresult2;
                    //условие изменения весов
                    if (delta1 > 0.005f || delta1 < -0.005f) 
                    {
                        Proverkavesov(delta1, curresult1, weight11, weight2, out  weight11, out weight2);
                    }
                    if (delta2 > 0.005f || delta2 < -0.005f)
                    {
                        Proverkavesov(delta2, curresult2, weight3, weight4, out weight3, out weight4);
                    }
                    //если обучение завершено
                    if (delta1 < 0.005f && delta1 > -0.005f && delta2 < 0.005f && delta2 > -0.005f)
                    {
                        Console.WriteLine("Обучение завершено");
                        return ef;
                    }
                    //вывод текущего состояния обучения
                    /*if (iteraciya % 200 == 0) */
                    Console.WriteLine("Количество итераций: " + iteraciya + ". Текущая погрешности: " + delta1 + " и " + delta2);
                }
                Console.WriteLine("Закончилось количество попыток, обучений не завершено"); //если неуспел обучиться
                Console.Read();
                ef = true;
                return ef;
            }

            public void Proverkavesov(float oshibka, float cr, float weighti1, float weighti2, out float w1, out float w2) //функция проверки весов для нейронов
            {
                if (oshibka > 0)
                { 
                    w1 = weighti1 + oshibka / cr;
                    w2 = weighti2;
                }
                else
                { 
                    w2 = weighti2 + oshibka / cr;
                    w1 = weighti1;
                }
            }

        }
        public static void Main()
        {
            bool exitflag = false;
            int x1, x2;
            Console.WriteLine("Инициализация процесса обучения... \n" +
                "Введите целевой результат обучения 1: ");
            float goalresult1 = Convert.ToInt32(Console.ReadLine()); //целевой результат1
            Console.WriteLine("Введите целевой результат обучения 2: ");
            float goalresult2 = Convert.ToInt32(Console.ReadLine()); //целевой результат2
            Console.WriteLine("Введите обучающий параметр 1 (расстояние): ");
            x1 = Convert.ToInt32(Console.ReadLine()); //переменная 1
            Console.WriteLine("Введите обучающий параметр 2(время): ");
            x2 = Convert.ToInt32(Console.ReadLine());//переменная 2

            Neuron neuron = new Neuron(); //инициализация нейрона
            neuron.Cikleobucheniya(x1,x2,goalresult1, goalresult2, exitflag); //процесс обучения

            if (exitflag == false)
            {
                useneuron://повторное использование нейронки
                Console.WriteLine("Введите параметр 1: ");
                x1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр 2: ");
                x2 = Convert.ToInt32(Console.ReadLine());
                neuron.Activefunction(x1, x2, out var r1, out var r2); //вызов нейронки
                Console.WriteLine("Результаты: " + r1 +" и "+ r2);//вывод ответа
                Console.Read();
                checkmetka: //метка повтора ввода при ошибке
                Console.WriteLine("Повторить?  y/n");
                string check = Console.ReadLine();
                if (check == "y")
                { goto useneuron; }
                else
                {
                    if (check != "n")
                    { goto checkmetka; }
                }
                
            }
        }
    }
}
