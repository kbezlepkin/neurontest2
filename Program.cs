using System;

namespace neurontest2
{
    class Program
    {
        public class Neuron //класс нейрона
        {
            public float[,] weight = new float[2/*максимальное количество нейронов*/, 2/* максимальное количество входов на нейроне*/];

            public float Activefunction(int input1, int input2, int i) //функция активации нейрона
            {
                float resultexit = (weight[i, 0] * input1) / (weight[i, 1] * input2);
                return resultexit;
            }


            public bool Cikleobucheniya(int input1, int input2, float gr1, float gr2, bool ef) //функция обучения нейронки
            {
                //погрешность ответа на выходе
                float[] delta = new float[2];
                float[] gr = { gr1, gr2 };
                for (int iteraciya = 0; iteraciya < 10000 /*количество итераций*/; iteraciya++) //метод дял ограниченного количества итераций
                {
                    for (int i = 0; i < 2; i++)
                    {
                        delta[i] = gr[i] - Activefunction(input1, input2, i);
                        //условие изменения весов
                        if (delta[i] > 0.005f || delta[i] < -0.005f)
                        { Proverkavesov(delta[i], Activefunction(input1, input2, i), i); }
                    }

                    //вывод текущего состояния обучения
                    /*if (iteraciya % 200 == 0) */
                    Console.WriteLine("Количество итераций: " + iteraciya + ". Текущая погрешности: " + delta[0] + " и " + delta[1]);
                    
                    //если обучение завершено
                    bool suc = true;
                    foreach (float d in delta)
                    {
                        if (d > 0.005f || d < -0.005f)
                        { suc = false; }
                    }
                    if (suc == true)
                    { Console.WriteLine("Обучение завершено"); return ef; }
                }
                Console.WriteLine("Закончилось количество попыток, обучений не завершено"); //если неуспел обучиться
                Console.Read(); ef = true; return ef;
            }

            public void Proverkavesov(float oshibka, float cr, int i /*float[,] weight,*/) //функция проверки весов для нейронов
            {
                if (oshibka > 0)
                { weight[i, 0] = weight[i, 0] + oshibka / cr; }
                else
                { weight[i, 1] = weight[i, 1] + oshibka / cr; }
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

            //инициализация нейроной сетки
            Neuron neuron = new Neuron(); //инициализация экземпляра нейроной сетки
            for (int i = 0; i < 2; i++) //инициализация весов экземпляра нейроной сетки
            {
                for (int j = 0; j < 2; j++)
                { neuron.weight[i, j] = 0.5f; }
            }

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
                Console.WriteLine("Результаты: " + neuron.Activefunction(x1, x2, 0) +" и "+ neuron.Activefunction(x1, x2, 1));//вывод ответа
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
