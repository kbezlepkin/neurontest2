using System;

namespace neurontest2
{
    class Program
    {
        public class NeuronNet //класс нейрона
        {
            public float[,,] weight = new float[2/*количество слоев*/,2/*максимальное количество нейронов*/, 2/* максимальное количество входов на нейроне*/];

            public float Activefunction(int input1, int input2, int sloyneyronki, int nomerneyrona) //функция активации нейрона
            {
                if (sloyneyronki > 0)
                {
                    float resultexit = (weight[sloyneyronki, nomerneyrona, 0] * Activefunction(input1, input2, sloyneyronki-1, nomerneyrona)) / (weight[sloyneyronki, nomerneyrona, 1] * Activefunction(input1, input2, sloyneyronki - 1, nomerneyrona));
                    return resultexit;
                }
                else
                {
                    float resultexit = (weight[sloyneyronki, nomerneyrona, 0] * input1) / (weight[sloyneyronki, nomerneyrona, 1] * input2);
                    return resultexit;
                }
            }

            public bool Cikleobucheniya(int input1, int input2, float gr1, float gr2, bool ef) //функция обучения нейронки
            {
                //погрешность ответа на выходе
                float[,] delta = new float[2,2];
                float[] gr = { gr1, gr2 };
                for (int iteraciya = 0; iteraciya < 10000 /*количество итераций*/; iteraciya++) //метод дял ограниченного количества итераций
                {
                    Console.WriteLine("Количество итераций: " + iteraciya +'.');
                    Console.WriteLine("Текущии погрешности: ");
                    for (int sloy = 0; sloy < 2; sloy++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            delta[sloy, j] = gr[j] - Activefunction(input1, input2,sloy, j);
                            //условие изменения весов
                            if (delta[sloy, j] > 0.005f || delta[sloy, j] < -0.005f)
                            { Proverkavesov(delta[sloy, j], Activefunction(input1, input2,sloy, j),sloy, j); }
                        }

                        //вывод текущего состояния обучения
                        /*if (iteraciya % 200 == 0) */
                        for (int j = 0; j < 2; j++)
                        { Console.WriteLine(delta[sloy, j]); }

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
                }
                Console.WriteLine("Закончилось количество попыток, обучений не завершено"); //если неуспел обучиться
                Console.Read(); ef = true; return ef;
            }

            public void Proverkavesov(float oshibka, float cr, int i, int j) //функция проверки весов для нейронов
            {
                if (oshibka > 0)
                { weight[i, j, 0] = weight[i, j, 0] + oshibka / cr; }
                else
                { weight[i, j, 1] = weight[i, j, 1] + oshibka / cr; }
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
            NeuronNet neuron = new NeuronNet(); //инициализация экземпляра нейроной сетки
            for (int i = 0; i < 2; i++) //инициализация весов экземпляра нейроной сетки
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    { neuron.weight[i, j, k] = 0.5f; }
                }
            }
            neuron.Cikleobucheniya(x1, x2, goalresult1, goalresult2, exitflag); //процесс обучения

            //работа нейронки
            if (exitflag == false) //если обучение прошло успешно
            {
                useneuron://повторное использование нейронки
                Console.WriteLine("Введите параметр 1: ");
                x1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр 2: ");
                x2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Результаты: " + neuron.Activefunction(x1, x2, 1, 0) +" и "+ neuron.Activefunction(x1, x2, 1,1));//вывод ответа
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
