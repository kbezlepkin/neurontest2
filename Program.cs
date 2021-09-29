using System;

namespace neurontest2
{
    class Program
    {
        public class Neuron //класс нейрона
        {
            public float[,] weight = new float[2, 2];
            public float weight11 = 0.5f, weight2 = 0.5f,//вес на входах нейрона
                            weight3 =  0.5f, weight4 = 0.5f;

            public float Activefunction(int input1, int input2, int i) //функция активации нейрона
            {
                float resultexit = (weight[i, 1] * input1) / (weight[i, 2] * input2);
                return resultexit;
            }


            public bool Cikleobucheniya(int input1, int input2, float gr1, float gr2, bool ef) //функция обучения нейронки
            {
                float delta1, delta2; //погрешность ответа на выходе
                float[] delta = new float[2];
                float[] gr = { gr1, gr2 };
                for (int iteraciya = 0; iteraciya < 10000 /*количество итераций*/; iteraciya++) //метод дял ограниченного количества итераций
                {
                    for (int i = 0; i < 2; i++)
                    {
                        delta[i] = gr[i] - Activefunction(input1, input2, i);
                        //условие изменения весов
                        if (delta[i] > 0.005f || delta[i] < -0.005f)
                        {
                            Proverkavesov(delta[i], Activefunction(input1, input2, i), i);
                        }
                    }

                    //если обучение завершено
                    foreach (float d in delta)
                    { 
                        if
                    }
                    if (delta1 < 0.005f && delta1 > -0.005f && delta2 < 0.005f && delta2 > -0.005f)
                    {
                        Console.WriteLine("Обучение завершено");
                        return ef;
                    }
                    //вывод текущего состояния обучения
                    /*if (iteraciya % 200 == 0) */
                    Console.WriteLine("Количество итераций: " + iteraciya + ". Текущая погрешности: " + delta[1] + " и " + delta[2]);
                }
                Console.WriteLine("Закончилось количество попыток, обучений не завершено"); //если неуспел обучиться
                Console.Read();
                ef = true;
                return ef;
            }

            public void Proverkavesov(float oshibka, float cr, /*float[,] weight,*/ float weighti1, float weighti2, out float w1, out float w2) //функция проверки весов для нейронов
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
        public static void Main() //тело программы - точка входа
        {
            bool exitflag = false;
            int x1, x2;
            //Инициализация процесса обучения нейронки
                Console.WriteLine("Инициализация процесса обучения... \n" + "Введите целевой ответ 1: ");
                float goalresult1 = Convert.ToInt32(Console.ReadLine()); //целевой результат1
                Console.WriteLine("Введите целевой ответ 2: ");
                float goalresult2 = Convert.ToInt32(Console.ReadLine()); //целевой результат2
                Console.WriteLine("Введите обучающий параметр 1 (расстояние): ");
                x1 = Convert.ToInt32(Console.ReadLine()); //переменная 1
                Console.WriteLine("Введите обучающий параметр 2(время): ");
                x2 = Convert.ToInt32(Console.ReadLine());//переменная 2

                Neuron neuron = new Neuron(); //инициализация нейроной сетки
                neuron.Cikleobucheniya(x1, x2, goalresult1, goalresult2, exitflag); //процесс обучения

            //если обучение прошло успешно
            //работа нейронки
            if (exitflag == false)
            {
                useneuron://повторное использование нейронки
                Console.WriteLine("Введите параметр 1: ");
                x1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр 2: ");
                x2 = Convert.ToInt32(Console.ReadLine());
                neuron.Activefunction(x1, x2, out var r1, out var r2); //вызов нейронки
                Console.WriteLine("Результаты: " + r1 +" и "+ r2);//вывод ответа
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
