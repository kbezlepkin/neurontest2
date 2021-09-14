using System;

namespace neurontest2
{
    class Program
    {
        public class Neuron //класс нейрона
        { public float resultexit; //выход нейрона
            private float weight1 = 0.5f,//вес на входах нейрона
                            weight2 = 0.5f;
            public float activefunction(int input1, int input2) //функция активации нейрона
            {
                /*пример функции активации нейрона: перевод скорости мотоцикла в м/с*/
                resultexit = (weight1 * input1) / (input2 * weight2);
                return resultexit;
            }


            public bool cikleobucheniya(int input1, int input2, float gr, bool ef) //функция обучения нейронки
            {
                float delta, //погрешность ответа на выходе
                     curresult; //текущий результат

                for (int iteraciya = 0; iteraciya < 10000 /*количество итераций*/; iteraciya++) //метод дял ограниченного количества итераций
                {
                    curresult = activefunction(input1, input2);
                    delta = gr - curresult; //погрешность измерений
                    //условие изменения весов
                    if (delta > 0.005f || delta < -0.005f) 
                    {
                        proverkavesov(delta, curresult);
                    }
                    //если обучение завершено
                    else
                    {
                        Console.WriteLine("Обучение завершено");
                        return ef;
                    }
                    //вывод текущего состояния обучения
                    /*if (iteraciya % 200 == 0) */
                    Console.WriteLine("Количество итераций: " + iteraciya + ". Текущая погрешность: " + delta);
                }
                Console.WriteLine("Закончилось количество попыток, обучений не завершено"); //если неуспел обучиться
                Console.Read();
                return ef = true;
            }

            void proverkavesov(float oshibka, float cr) //функция проверки весов для нейронов
            {
                if (oshibka > 0)
                { weight1 += oshibka / cr; }
                else
                { weight2 += oshibka / cr; }
            }

        }
        public static void Main(string[] args)
        {
            bool exitflag = false;
            int x1, x2;
            Console.WriteLine("Инициализация проыесса обучения... \n" +
                "Введите целевой результат обучения: ");
            int goalresult = Convert.ToInt32(Console.ReadLine()); //целевой результат
            Console.WriteLine("Введите обучающий параметр 1: ");
            x1 = Convert.ToInt32(Console.ReadLine()); //переменная 1
            Console.WriteLine("Введите обучающий параметр 2: ");
            x2 = Convert.ToInt32(Console.ReadLine());//переменная 2

            Neuron neuron = new Neuron(); //инициализация нейрона
            neuron.cikleobucheniya(x1,x2,goalresult, exitflag); //процесс обучения

            if (exitflag == false)
            {
                useneuron://повторное использование нейронки
                Console.WriteLine("Введите параметр 1: ");
                x1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр 2: ");
                x2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(neuron.activefunction(x1, x2));//вывод ответа
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
