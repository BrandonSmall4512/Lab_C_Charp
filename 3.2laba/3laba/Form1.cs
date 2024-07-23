using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;


namespace _3laba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }
        // размеры окна 
        double ScreenW, ScreenH;

        // отношения сторон окна визуализации 
        // для корректного перевода координат мыши в координаты, 
        // принятые в программе 

        private float devX;
        private float devY;

        // массив, который будет хранить значения x,y точек графика 
        private float[,] GrapValuesArray;
        // количество элементов в массиве 
        private int elements_count = 0;

        // флаг, означающий, что массив с значениями координат графика пока еще не заполнен 
        private bool not_calculate = true;

        // номер ячейки массива, из которой будут взяты координаты для красной точки 
        // для визуализации текущего кадра 
        private int pointPosition = 0;

        // вспомогательные переменные для построения линий от курсора мыши к координатным осям 
        float lineX, lineY;

        // текущение координаты курсора мыши 
        float Mcoord_X = 0, Mcoord_Y = 0;

        // size of window
        float Xmin = -15;
        float Xmax = 15;
        float Ymin = -15;
        float Ymax = 15;

        Color color_axes = Color.Blue;
        Color color_point = Color.Green;
        Color color_text = Color.Red;
        Color color_grid = Color.Black;
        Color color_perp = Color.Black;
        Color color_line = Color.Green;
        int chosen_thing = 0;

        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {
            // сохраняем координаты мыши 
            Mcoord_X = e.X;
            Mcoord_Y = e.Y;

            // вычисляем параметры для будущей дорисовки линий от указателя мыши к координатным осям. 
            lineX = devX * e.X;
            lineY = (float)(ScreenH - devY * e.Y);

        }

        // функция визуализации текста 
        private void PrintText2D(float x, float y, string text)
        {

            // устанавливаем позицию вывода растровых символов 
            // в переданных координатах x и y. 
            Gl.glRasterPos2f(x, y);

            // в цикле foreach перебираем значения из массива text, 
            // который содержит значение строки для визуализации 
            foreach (char char_for_draw in text)
            {
                // символ C визуализируем с помощью функции glutBitmapCharacter, используя шрифт GLUT_BITMAP_9_BY_15. 
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_9_BY_15, char_for_draw);
            }

        }

        // функция, производящая вычисления координат графика 
        // и заносящая их в массив GrapValuesArray 
        private void functionCalculation()
        {

            // определение локальных переменных X и Y 
            float x = 0, y = 0;

            // инициализация массива, который будет хранить значение 300 точек, 
            // из которых будет состоять график 
            GrapValuesArray = new float[(int)(Xmax - Xmin) * 10 + 1, 2];

            // счетчик элементов массива 
            elements_count = 0;

            // вычисления всех значений y для x, принадлежащего промежутку от xmin до xmax с шагом в 0.1f 
            for (x = Xmin; x < Xmax - 0.1f; x += 0.1f)
            {
                // вычисление y для текущего x по формуле y = f(x)
                switch (graphChoose.SelectedIndex)
                {
                    case 0:
                        y = (float)Math.Sin(x) * 3 + 1;
                        break;
                    case 1:
                        y = (float)(-10 / Math.Abs(x - 3)) * (float)Math.Sin(25 * x);
                        break;
                    case 2:
                        y = (float)Math.Tan(Math.Abs(x));
                        break;
                    default:
                        y = (float)Math.Sin(x) * 3 + 1;
                        break;
                }

                // запись координаты x 
                GrapValuesArray[elements_count, 0] = x;
                // запись координаты y 
                GrapValuesArray[elements_count, 1] = y;
                // подсчет элементов 
                elements_count++;
            }

            // изменяем флаг, сигнализировавший о том, что координаты графика не вычислены 
            not_calculate = false;
        }

        // визуализация графика 
        private void DrawDiagram()
        {
            // проверка флага, сигнализирующего о том, что координаты графика вычислены 
            if (not_calculate)
            {
                // если нет, то вызываем функцию вычисления координат графика 
                functionCalculation();
            }

            Gl.glColor4ub(color_line.R, color_line.G, color_line.B, color_line.A);

            // стартуем отрисовку в режиме визуализации точек 
            // объединяемых в линии (GL_LINE_STRIP) 
            Gl.glBegin(Gl.GL_LINE_STRIP);

            // рисуем начальную точку 
            Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);

            // проходим по массиву с координатами вычисленных точек 
            for (int ax = 1; ax < elements_count; ax += 1)
            {
                // передаем в OpenGL информацию о вершине, участвующей в построении линий 
                Gl.glVertex2d(GrapValuesArray[ax, 0], GrapValuesArray[ax, 1]);
            }

            // завершаем режим рисования 
            Gl.glEnd();

            // устанавливаем размер точек, равный 5 пикселям 
            Gl.glPointSize(5);

            // устанавливаем текущий цвет - цвет точки
            Gl.glColor4ub(color_point.R, color_point.G, color_point.B, color_point.A);

            // активируем режим вывода точек (GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);

            // выводим точку, используя ту ячейку массива, до которой мы дошли (вычисляется в функции обработчике событий таймера) 
            Gl.glVertex2d(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);

            // завершаем режим рисования 
            Gl.glEnd();

            // устанавливаем размер точек равный единице 
            Gl.glPointSize(1);

            Gl.glColor4ub(color_text.R, color_text.G, color_text.B, color_text.A);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Ymaxim_TextChanged(object sender, EventArgs e)
        {

        }

        private void Yminim_TextChanged(object sender, EventArgs e)
        {

        }

        private void Xminim_TextChanged(object sender, EventArgs e)
        {

        }

        private void Xmaxim_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ymax = (float)Convert.ToDouble(Ymaxim.Text);
            Ymin = (float)Convert.ToDouble(Yminim.Text);
            Xmax = (float)Convert.ToDouble(Xmaxim.Text);
            Xmin = (float)Convert.ToDouble(Xminim.Text);

            pointPosition = 0;
            // инициализация режима экрана 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA) 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();

            // определение параметров настройки проекции в зависимости от размеров сторон элемента AnT. 
            ScreenW = Xmax - Xmin;
            ScreenH = (Ymax - Ymin); //* (float)AnT.Height / (float)AnT.Width;

            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);

            // сохранение коэффициентов, которые нам необходимы для перевода координат указателя в оконной системе в координаты, 
            // принятые в нашей OpenGL сцене 
            devX = (float)ScreenW / (float)AnT.Width;
            devY = (float)ScreenH / (float)AnT.Height;

            // установка объектно-видовой матрицы 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            // старт счетчика, отвечающего за вызов функции визуализации сцены 
            PointlnGrap.Start();

            functionCalculation();
        }

        private void colorSelect_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            //colorDlg.ShowDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                switch (colorFor.SelectedIndex)
                {
                    //colorDlg.Color
                    case 0:
                        color_axes = colorDlg.Color;
                        break;
                    case 1:
                        color_point = colorDlg.Color;
                        break;
                    case 2:
                        color_line = colorDlg.Color;
                        break;
                    case 3:
                        color_grid = colorDlg.Color;
                        break;
                    case 4:
                        color_perp = colorDlg.Color;
                        break;
                    case 5:
                        color_text = colorDlg.Color;
                        break;
                    default:
                        break;
                }
                Draw();
            }
        }

        private void colorFor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void graphChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            pointPosition = 0;
            functionCalculation();
        }

        // функция, управляющая визуализацией сцены 
        private void Draw()
        {
            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // очищение текущей матрицы 
            Gl.glLoadIdentity();

            // установка цвета для сетки
            Gl.glColor4ub(color_grid.R, color_grid.G, color_grid.B, color_grid.A);

            // помещаем состояние матрицы в стек матриц 
            Gl.glPushMatrix();

            // выполняем перемещение в пространстве по осям X и Y 
            Gl.glTranslated(0 - Xmin, 0 - Ymin, 0);

            // активируем режим рисования (указанные далее точки будут выводиться как точки GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);

            // с помощью прохода в двух циклах создаем сетку из точек 
            for (int ax = (int)Xmin; ax < (int)Xmax; ax++)
            {
                for (int bx = (int)Ymin; bx < (int)Ymax; bx++)
                {
                    // вывод точки
                    Gl.glVertex2d(ax, bx);
                }
            }

            // завершение режима рисования примитивов 
            Gl.glEnd();

            // установка цвета для осей
            Gl.glColor4ub(color_axes.R, color_axes.G, color_axes.B, color_axes.A);

            // активируем режим рисования, каждые 2 последовательно вызванные команды glVertex 
            // объединяются в линии 
            Gl.glBegin(Gl.GL_LINES);

            // рисуем координатные оси и стрелки на их концах 
            Gl.glVertex2d(0, Ymin);
            Gl.glVertex2d(0, Ymax);

            Gl.glVertex2d(Xmin, 0);
            Gl.glVertex2d(Xmax, 0);

            // вертикальная стрелка 
            Gl.glVertex2d(0, Ymax);
            Gl.glVertex2d(0.005f * (Xmax - Xmin), Ymax - 0.02f * (Ymax - Ymin));
            Gl.glVertex2d(0, Ymax);
            Gl.glVertex2d(-0.005f * (Xmax - Xmin), Ymax - 0.02f * (Ymax - Ymin));

            // горизонтальная стрелка 
            Gl.glVertex2d(Xmax, 0);
            Gl.glVertex2d(Xmax - 0.015f * (Xmax - Xmin), 0.007f * (Ymax - Ymin));
            Gl.glVertex2d(Xmax, 0);
            Gl.glVertex2d(Xmax - 0.015f * (Xmax - Xmin), -0.007f * (Ymax - Ymin));

            // завершаем режим рисования 
            Gl.glEnd();

            // выводим подписи осей "x" и "y" 
            PrintText2D(Xmax - 0.02f * (Xmax - Xmin), -0.03f * (Ymax - Ymin), "x");
            PrintText2D(0.01f * (Xmax - Xmin), Ymax - 0.02f * (Ymax - Ymin), "y");

            // вызываем функцию рисования графика 
            DrawDiagram();

            // возвращаем матрицу из стека 
            Gl.glPopMatrix();

            // выводим текст со значением координат возле курсора 
            PrintText2D(devX * Mcoord_X + 0.2f, (float)ScreenH - devY * Mcoord_Y + 0.4f, "[ x: " + (devX * Mcoord_X + Xmin).ToString() + " ; y: " + ((float)ScreenH - devY * Mcoord_Y + Ymin).ToString() + "]");

            // устанавливаем цвет для перпендикуляров
            Gl.glColor4ub(color_perp.R, color_perp.G, color_perp.B, color_perp.A);

            // включаем режим рисования линий, чтобы нарисовать линии от курсора мыши к координатным осям 
            Gl.glBegin(Gl.GL_LINES);

            Gl.glVertex2d(lineX, 0 - Ymin);
            Gl.glVertex2d(lineX, lineY);
            Gl.glVertex2d(0 - Xmin, lineY);
            Gl.glVertex2d(lineX, lineY);

            // завершаем режим рисования 
            Gl.glEnd();

            // дожидаемся завершения визуализации кадра 
            Gl.glFlush();

            // сигнал для обновление элемента, реализующего визуализацию 
            AnT.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация библиотеки glut 
            Glut.glutInit();
            // инициализация режима экрана 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA) 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();

            // определение параметров настройки проекции в зависимости от размеров сторон элемента AnT. 
            ScreenW = Xmax - Xmin;
            ScreenH = (Ymax - Ymin); //* (float)AnT.Height / (float)AnT.Width;

            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);

            // сохранение коэффициентов, которые нам необходимы для перевода координат указателя в оконной системе в координаты, 
            // принятые в нашей OpenGL сцене 
            devX = (float)ScreenW / (float)AnT.Width;
            devY = (float)ScreenH / (float)AnT.Height;

            // установка объектно-видовой матрицы 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            // старт счетчика, отвечающего за вызов функции визуализации сцены 
            PointlnGrap.Start();

            // Заполнение выпадающего списка графиков
            graphChoose.Items.AddRange(new string[] {
            "y = sin(x) * 3 + 1",
            "y = (-10 / |x - 3|) * sin(25 * x)",
            "y = tan(|x|)"
            });
            graphChoose.SelectedIndex = 0;

        }

        private void PointlnGrap_Tick(object sender, EventArgs e)
        {
            // если мы дошли до последнего элемента массива 
            if (pointPosition == elements_count - 1)
                pointPosition = 0; // переходим к начальному элементу 

            // функция визуализации 
            Draw();

            // переход к следующему элементу массива 
            pointPosition++;

        }
    }
}
