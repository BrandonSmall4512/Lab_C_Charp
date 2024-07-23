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

namespace _2laba
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация Glut 
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);

            // очистка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будет сконфигурированный настройки проекции 
            if ((float)AnT.Width <= (float)AnT.Height)
            {
                Glu.gluOrtho2D(0.0, 30.0 * (float)AnT.Height / (float)AnT.Width, 0.0, 30.0);
            }
            else
            {
                Glu.gluOrtho2D(0.0, 30.0 * (float)AnT.Width / (float)AnT.Height, 0.0, 30.0);
            }
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // очищаем буфер цвета 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // очищаем текущую матрицу 
            Gl.glLoadIdentity();
            // устанавливаем текущий цвет - красный 
            Gl.glColor3f(1.0f, 0, 0);


            // активируем режим рисования линий на основе 
            // последовательного соединения всех вершин в отрезки 
            Gl.glBegin(Gl.GL_LINE_LOOP);
            // первая вершина будет находиться в начале координат 

            Gl.glVertex2d(8, 7);
            Gl.glVertex2d(15, 27);
            Gl.glVertex2d(17, 27);
            Gl.glVertex2d(23, 7);
            Gl.glVertex2d(21, 7);
            Gl.glVertex2d(19, 14);
            Gl.glVertex2d(12.5, 14);
            Gl.glVertex2d(10, 7);

            // завершаем режим рисования 
            Gl.glEnd();

            // вторая линия 

            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(18.5, 16);
            Gl.glVertex2d(16, 25);
            Gl.glVertex2d(13.2, 16);

            // завершаем режим рисования 
            Gl.glEnd();

            // дожидаемся конца визуализации кадра 
            Gl.glFlush();

            // посылаем сигнал перерисовки элемента AnT. 
            AnT.Invalidate();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void drawB(float x0, float y0, float w, float h)
        {

            //..1 loop...

            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0, y0);
            Gl.glVertex2d(x0, y0 + h);
            Gl.glVertex2d(x0 + w, y0 + h);
            Gl.glVertex2d(x0 + w, y0 + 0.833f * h);
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.833f * h);  //5
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.5f * h);  //6
            Gl.glVertex2d(x0 + w, y0 + 0.5f * h); //7
            Gl.glVertex2d(x0 + w, y0);

            Gl.glEnd();

            //..2 loop....
            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.167f * h);
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.33f * h);
            Gl.glVertex2d(x0 + 0.75f * w, y0 + 0.33f * h);
            Gl.glVertex2d(x0 + 0.75f * w, y0 + 0.167f * h);


            Gl.glEnd();

        }
        private void drawV(float x0, float y0, float w, float h)
        {

            //..1 loop...

            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0, y0);
            Gl.glVertex2d(x0, y0 + h);
            Gl.glVertex2d(x0 + w, y0 + h);
            Gl.glVertex2d(x0 + w, y0 + 0.667f*h);
            Gl.glVertex2d(x0 + 0.5f*w, y0 + 0.5f*h);
            Gl.glVertex2d(x0 + w, y0 + 0.33f*h);
            Gl.glVertex2d(x0 + w, y0);

            Gl.glEnd();

            //..2 loop....
            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.667f * h);
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.833f * h);
            Gl.glVertex2d(x0 + 0.75f * w, y0 + 0.833f * h);

            Gl.glEnd();

            //..3 loop........
            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.1667f * h);
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.333f * h);
            Gl.glVertex2d(x0 + 0.75f * w, y0 + 0.1667f * h);

            Gl.glEnd();

        }
        private void drawA(float x0, float y0, float w, float h)
        {
            //..1 loop...

            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0, y0);
            Gl.glVertex2d(x0 + 0.375f*w, y0 + h);
            Gl.glVertex2d(x0 + 0.625f*w, y0 + h);
            Gl.glVertex2d(x0 + w, y0);
            Gl.glVertex2d(x0 + 0.75f * w, y0);
            Gl.glVertex2d(x0 + 0.625f*w, y0 + 0.25f * h);
            Gl.glVertex2d(x0 + 0.375*w, y0 + 0.25f * h);
            Gl.glVertex2d(x0 + 0.25 * w, y0);

            Gl.glEnd();


            //..2 loop....
            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0 + 0.375f * w, y0 + 0.4167f * h);
            Gl.glVertex2d(x0 + 0.5f * w, y0 + 0.667f * h);
            Gl.glVertex2d(x0 + 0.625f * w, y0 + 0.4167f * h);

            Gl.glEnd();
        }
        private void button3_Click(object sender, EventArgs e)
        {

            // очищаем буфер цвета 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // очищаем текущую матрицу 
            Gl.glLoadIdentity();
            // устанавливаем текущий цвет 
            Gl.glColor3f(0, 0, 1.0f);


            // custom variables
            float h = 20;
            float w = 3;
            float x0 = 2;
            float y0 = 2;
            float d = 10;

            drawB(x0, y0, w, h);
            drawV(x0+ d + w, y0, w, h);
            drawA(x0 + (w+d)*2, y0, w, h);

            // дожидаемся конца визуализации кадра 
            Gl.glFlush();

            // посылаем сигнал перерисовки элемента AnT. 
            AnT.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // очищаем буфер цвета 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // очищаем текущую матрицу 
            Gl.glLoadIdentity();
            // устанавливаем текущий цвет 
            Gl.glColor3f(0.4f, 0, 1.0f);


            // custom variables
            float h = 15;
            float w = 15;
            float x0 = 10;
            float y0 = 3;

            //..1 loop...

            Gl.glBegin(Gl.GL_LINE_LOOP);

            Gl.glVertex2d(x0 + 0.1f*w, y0 + 0.35f *h);
            Gl.glVertex2d(x0 + 0.1f * w, y0 + 0.6f * h);
            Gl.glVertex2d(x0 + 0.2f * w, y0 + 0.75f * h);
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.8f * h);
            Gl.glVertex2d(x0 + 0.5f * w, y0 + 0.85f * h);   //5
            Gl.glVertex2d(x0 + 0.5f * w, y0 + 0.75f * h);
            Gl.glVertex2d(x0 + 0.6f * w, y0 + 0.825f * h);
            Gl.glVertex2d(x0 + 0.7f * w, y0 + 0.8f * h);
            Gl.glVertex2d(x0 + 0.6f * w, y0 + 0.65f * h);
            Gl.glVertex2d(x0 + 0.55f * w, y0 + 0.5f * h);  //10
            Gl.glVertex2d(x0 + 0.65f * w, y0 + 0.35f * h);
            Gl.glVertex2d(x0 + 0.9f * w, y0 + 0.35f * h);
            Gl.glVertex2d(x0 + 0.85f * w, y0 + 0.2f * h);
            Gl.glVertex2d(x0 + 0.7f * w, y0 + 0.1f * h);
            Gl.glVertex2d(x0 + 0.55f * w, y0 + 0.05f * h);  //15
            Gl.glVertex2d(x0 + 0.4f * w, y0 + 0.05f * h);
            Gl.glVertex2d(x0 + 0.2f * w, y0 + 0.15f * h);

            Gl.glEnd();

            //..2 line.....
            //Gl.glColor3f(0, 1.0f, 0);

            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glVertex2d(x0 + 0.35f * w, y0 + 0.8f * h);
            Gl.glVertex2d(x0 + 0.5f * w, y0 + 0.75f * h);
            Gl.glVertex2d(x0 + 0.5f * w, y0 + 0.85f * h);
            Gl.glVertex2d(x0 + 0.65f * w, y0 + 0.95f * h);
            Gl.glVertex2d(x0 + 0.65f * w, y0 + 0.85f * h);
            Gl.glVertex2d(x0 + 0.5f * w, y0 + 0.75f * h);
            Gl.glVertex2d(x0 + 0.65f * w, y0 + 0.8f * h);

            Gl.glEnd();

            //...3 line...............

            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glVertex2d(x0 + 0.7f * w, y0 + 0.8f * h);
            Gl.glVertex2d(x0 + 0.7f * w, y0 + 0.6f * h);
            Gl.glVertex2d(x0 + 0.75f * w, y0 + 0.45f * h);
            Gl.glVertex2d(x0 + 0.9f * w, y0 + 0.35f * h);

            Gl.glEnd();

            //...4 line...

            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glVertex2d(x0 + 0.4f * w, y0 + 0.4f * h);
            Gl.glVertex2d(x0 + 0.3f * w, y0 + 0.45f * h);
            Gl.glVertex2d(x0 + 0.3f * w, y0 + 0.4f * h);
            Gl.glVertex2d(x0 + 0.4f * w, y0 + 0.4f * h);
            Gl.glVertex2d(x0 + 0.45f * w, y0 + 0.5f * h);
            Gl.glVertex2d(x0 + 0.4f * w, y0 + 0.55f * h);
            Gl.glVertex2d(x0 + 0.35f * w, y0 + 0.55f * h);
            Gl.glVertex2d(x0 + 0.45f * w, y0 + 0.5f * h);

            Gl.glEnd();

            //..5 line..

            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glVertex2d(x0 + 0.3f * w, y0 + 0.45f * h);
            Gl.glVertex2d(x0 + 0.25f * w, y0 + 0.55f * h);
            Gl.glVertex2d(x0 + 0.3f * w, y0 + 0.65f * h);
            Gl.glVertex2d(x0 + 0.35f * w, y0 + 0.6f * h);
            Gl.glVertex2d(x0 + 0.35f * w, y0 + 0.55f * h);

            Gl.glEnd();

            //.. eye..

            Gl.glBegin(Gl.GL_POINTS);

            Gl.glVertex2d(x0 + 0.3f * w, y0 + 0.6f * h);

            Gl.glEnd();

            //... mouth...

            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glVertex2d(x0 + 0.3f * w, y0 + 0.55f * h);
            Gl.glVertex2d(x0 + 0.35f * w, y0 + 0.6f * h);

            Gl.glEnd();


            // дожидаемся конца визуализации кадра 
            Gl.glFlush();

            // посылаем сигнал перерисовки элемента AnT. 
            AnT.Invalidate();

        }
    }
}
