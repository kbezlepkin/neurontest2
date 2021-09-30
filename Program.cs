using System;

namespace neurontest2
{
    class Program
    {
        public class NeuronNet //класс нейрона
        {
            //массив весов всех нейронов, где
            public static int cs = 2;//количество слоев
            public static int cn = 2;//максимальное количество нейронов
            public static int cen = 2;//максимальное количество входов на нейроне
            public float[,,] weight = new float[cs, cn, cen];
            float[,] exitmass = new float[cs, cn];

            public float Activefunction1sloy(float input1, float input2, int sloyneyronki, int nomerneyrona) //функция активации нейрона 1го слоя
            {
                exitmass[sloyneyronki, nomerneyrona] = (weight[sloyneyronki, nomerneyrona, 0] * input1) / (weight[sloyneyronki, nomerneyrona, 1] * input2);
                return exitmass[sloyneyronki, nomerneyrona];
            }

            public float Activefunction2sloy(float input1, float input2, int sloyneyronki, int nomerneyrona) //функция активации нейрона 2го слоя
            {
                exitmass[sloyneyronki, nomerneyrona] = (weight[sloyneyronki, nomerneyrona, 0] * input1) + (weight[sloyneyronki, nomerneyrona, 1] * input2);
                return exitmass[sloyneyronki, nomerneyrona];
            }

            public void FunctionObucheniya(float input1, float input2, float gr1, float[,] d) //функция обучения нейронки
            {
                //погрешность ответа на выходе
                for (int i = 0; i < cn; i++)
                { 
                    float currentiterresult = Activefunction2sloy(Activefunction1sloy(), Activefunction1sloy(), NeuronNet.cs, i);
                    float[] currentdelta = new float[cn];
                    currentdelta[i] = gr1 - currentiterresult; 
                }



                float[] gr = { gr1 };

                for (int j = 0; j < NeuronNet.cn; j++)
                {
                    d[sloy, j] = gr[j] - Activefunction2sloy(input1, input2, sloy, j);
                    //условие изменения весов
                    if (d[sloy, j] > 0.005f || d[sloy, j] < -0.005f)
                    { Proverkavesov(delta[sloy, j], Activefunction2sloy(input1, input2, sloy, j), sloy, j); }
                }
            }

            public void Proverkavesov(float oshibka, float cr, int i, int j) //функция проверки весов для нейронов
            {
                if (oshibka > 0)
                { weight[i, j, 0] = weight[i, j, 0] + oshibka / cr; }
                else
                { weight[i, j, 1] = weight[i, j, 1] + oshibka / cr; }
            }
        }

        public static bool ProcessObucheniya(int input1, int input2, float gr1, bool ef, NeuronNet n)
        {
            for (int iteraciya = 0; iteraciya < 10000 /*количество итераций*/; iteraciya++) //метод дял ограниченного количества итераций
            {
                Console.WriteLine("Количество пройденых итераций: " + iteraciya + '.');
                //массив отклонений на текущей итерации
                float[,] delta = new float[NeuronNet.cs, NeuronNet.cn];
                n.FunctionObucheniya(input1, input2, gr1, delta);
                Console.WriteLine("Текущии погрешности: ");
                //тут реализация проверки

                //если обучение завершено
                bool suc = true;
                for (int i = 0; i<NeuronNet.cn; i++)
                {
                    if (delta[NeuronNet.cs,i] > 0.005f || delta[NeuronNet.cs, i] < -0.005f)
                    { suc = false; }
                }
                if (suc == true)
                { Console.WriteLine("Обучение завершено"); return ef; }
            }
            Console.WriteLine("Закончилось количество попыток, обучений не завершено"); //если неуспел обучиться
            Console.Read(); ef = true; return ef;
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
            
            ProcessObucheniya(x1, x2, goalresult1, goalresult2, exitflag, neuron); //процесс обучения

            //работа нейронки
            if (exitflag == false) //если обучение прошло успешно
            {
                useneuron://повторное использование нейронки
                Console.WriteLine("Введите параметр 1: ");
                x1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр 2: ");
                x2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Результаты: " + neuron.Activefunction1sloy(x1, x2, 1, 0) +" и "+ neuron.Activefunction1sloy(x1, x2, 1,1));//вывод ответа
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
