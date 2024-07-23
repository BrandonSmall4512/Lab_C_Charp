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
using Tao.DevIl;

namespace lab1
{
    public partial class Form1 : Form
    {
        double x = -8, y = 0, z = -30, a = 120, zoom = 1;
        int os_x = 1, os_y = 1, os_z = 1, d = 5, n = 4, m = 5;
        bool wire = false;
        bool col_tex = false;
        int up = 0, down = 0, left = 0, right = 0;
        int xold = 0, yold = 0;
        int xdif = 0, ydif = 0;
        int amount_of_apples = 0;
        
        // флаг - загружена ли текстура
        bool textureIsLoad = false;

        // имя текстуры
        public string texture_name = "";
        // индефекатор текстуры
        public int imageId = 0;

        // текстурный объект
        public uint mGlTextureObject1 = 0;
        public uint mGlTextureObject2 = 0;
        public uint mGlTextureObject3 = 0;
        //@@@

        // for snake
        int[,] arr;
        int[,] snake_arr;
        int snake_size = 10;
        int snake_direction = 4;
        int x_fruit = 0;
        int y_fruit = 0;

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            d = (int)d_numericUpDown.Value;
            n = (int)n_numericUpDown.Value;
            m = (int)m_numericUpDown.Value;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            up = 0; down = 1; left = 0; right = 0;
            snake_direction = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            up = 1; down = 0; left = 0; right = 0;
            snake_direction = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            up = 0; down = 0; left = 0; right = 1;
            snake_direction = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            up = 0; down = 0; left = 1; right = 0;
            snake_direction = 2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            up = 0; down = 0; left = 0; right = 0;
            snake_direction = 4;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            { col_tex = true; }
            else
            { col_tex = false; }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // открываем окно выбора файла
            DialogResult res = openFileDialog1.ShowDialog();

            // есл файл выбран - и возвращен результат OK
            if (res == DialogResult.OK)
            {
                // создаем изображение с индификатором imageId
                Il.ilGenImages(1, out imageId);
                // делаем изображение текущим
                Il.ilBindImage(imageId);

                // адрес изображения полученный с помощью окна выбра файла
                string url = openFileDialog1.FileName;

                // пробуем загрузить изображение
                if (Il.ilLoadImage(url))
                {
                    // если загрузка прошла успешно
                    // сохраняем размеры изображения
                    int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                    int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                    // определяем число бит на пиксель
                    int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                    switch (comboBox3.SelectedIndex)
                    {
                        case 0:
                            {
                                switch (bitspp) // в зависимости оп полученного результата
                                {
                                    // создаем текстуру используя режим GL_RGB или GL_RGBA
                                    case 24:
                                        mGlTextureObject1 = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                                        break;
                                    case 32:
                                        mGlTextureObject1 = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                                        break;
                                }
                                break;
                            }
                        case 1:
                            {
                                switch (bitspp) // в зависимости оп полученного результата
                                {
                                    // создаем текстуру используя режим GL_RGB или GL_RGBA
                                    case 24:
                                        mGlTextureObject2 = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                                        break;
                                    case 32:
                                        mGlTextureObject2 = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                                        break;
                                }
                                break;
                            }
                        case 2:
                            {
                                switch (bitspp) // в зависимости оп полученного результата
                                {
                                    // создаем текстуру используя режим GL_RGB или GL_RGBA
                                    case 24:
                                        {
                                            mGlTextureObject3 = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                                            break;
                                        }
                                    case 32:
                                        {
                                            mGlTextureObject3 = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                                            break;
                                        }
                                }
                                break;
                            }

                    };

                }
                // активируем флаг, сигнализирующий загрузку текстуры
                textureIsLoad = true;
                // очищаем память
                Il.ilDeleteImages(1, ref imageId);



            }

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            a = (double)trackBar4.Value;
            anglelabel.Text = "angle= " + a.ToString();

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            y = (double)trackBar2.Value;
            ylabel.Text = "y= " + y.ToString();

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            z = (double)trackBar3.Value;
            zlabel.Text = "z= " + z.ToString();

        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            zoom = (double)trackBar5.Value;
            zoomlabel.Text = "zoom= " + zoom.ToString();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            { wire = true; }
            else
            { wire = false; }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        os_x = 1;
                        os_y = 0;
                        os_z = 0;
                        break;
                    }
                case 1:
                    {
                        os_x = 0;
                        os_y = 1;
                        os_z = 0;
                        break;
                    }
                case 2:
                    {
                        os_x = 0;
                        os_y = 0;
                        os_z = 1;
                        break;
                    }
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            x = (double)trackBar1.Value;
            xlabel.Text = "x= " + x.ToString();

        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            // вызываем функцию отрисовки сцены 
            Draw();
            //DrawApple();
            //DrawApple2(); /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            tick();
            Snake();
            if (amount_of_apples < 1)
            {
                placeFruct();
                amount_of_apples++;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // инициализация библиотеки openIL
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 5;
            checkBox1.Checked = true;

            d = (int)d_numericUpDown.Value;
            n = (int)n_numericUpDown.Value;
            m = (int)m_numericUpDown.Value;

            n = 4;
            m = 5;
            d = 5;
            comboBox3.SelectedIndex = 0;

            //for snake
            arr = new int[20, 20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    arr[i, j] = 0;
                }
            }
            
            snake_arr = new int[100, 2];
            snake_arr[0, 0] = 0;
            for (int i = 0; i < 4; i++)
            {
                snake_arr[i, 0] += d;
                snake_arr[i, 1] = 0;
            }
            for (int i = 4; i < 100; i++)
            {
                snake_arr[i, 0] = 0;
                snake_arr[i, 1] = 0;
            }

            //placeFruct();

            RenderTimer.Start();


        }

        // функция отрисовки 
        private void Draw()
        {
            int i, j;
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();

            Gl.glPushMatrix();
            Gl.glTranslated(x, y, z);
            Gl.glRotated(a, os_x, os_y, os_z);
            Gl.glScaled(zoom, zoom, zoom);

            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    {
                        if (wire)
                            Glut.glutWireSphere(2, 16, 16);
                        else
                            Glut.glutSolidSphere(2, 16, 16);
                        break;
                    }

                case 2:
                    {
                        if (wire)
                            Glut.glutWireCube(2);
                        else
                            Glut.glutSolidCube(2);
                        break;
                    }

                case 5:
                    {
                        if (wire)
                        {
                            //d = 2;
                            for (i = 1; i <= n; i++)
                            {
                                Gl.glTranslated(d, (-d * m), 0);
                                for (j = 1; j <= m; j++)
                                {
                                    Glut.glutWireCube(d);
                                    Gl.glTranslated(0, d, 0);

                                }
                            }

                        }
                        else
                            Glut.glutSolidCube(2);
                        break;
                    }

            }


            AnT.Invalidate();
        }

        private void DrawApple()
        {

            float[] color1 = new float[4] { 1, 0, 0, 1 };
            float[] color2 = new float[4] { 0, 0, 0, 1 };
            float[] color3 = new float[4] { 0, 1, 0, 1 };
            float[] shininess = new float[1] { 30 };

            if (!col_tex)
            {
                if (wire)
                {

                    Gl.glTranslated(xold, yold, -d);
                    Gl.glTranslated(left * d - right * d, up * d - down * d, 0);
                    Glut.glutWireSphere(d / 2, 16, 16);
                    Gl.glTranslated(0, 0, -d);
                    Glut.glutWireCylinder((double)(d * 0.05), (double)(d * 0.7), 8, 8);
                    Gl.glTranslated(0, -d * 0.5, d * 0.3);
                    Gl.glRotated(0, 0, 1, 0);
                    Glut.glutWireCylinder((double)(d * 0.7), (double)(d * 0.1), 4, 1);
                    xold = xold + left * d - right * d;
                    yold = yold + up * d - down * d;


                }
                else
                /*
                {
                    Gl.glTranslated(xold, yold, -d);
                    Gl.glTranslated(left * d - right * d, up * d - down * d, 0);

                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, color1);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color1);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess);

                    Glut.glutSolidSphere(d / 2, 16, 16);
                    Gl.glTranslated(0, 0, -d);

                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, color2);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color2);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess);

                    Glut.glutSolidCylinder((double)(d * 0.05), (double)(d * 0.7), 8, 8);
                    Gl.glTranslated(0, -d * 0.5, d * 0.3);
                    Gl.glRotated(30, 0, 1, 0);

                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, color3);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color3);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess);

                    Glut.glutSolidCylinder((double)(d * 0.5), (double)(d * 0.1), 4, 1);
                    xold = xold + left * d - right * d;
                    yold = yold + up * d - down * d;

                }
                */

                {
                    Gl.glTranslated(xold, yold, -d);
                    Gl.glTranslated(left * d - right * d, up * d - down * d, 0);

                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, color1);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color1);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess);

                    Glut.glutSolidSphere(d / 2, 16, 16);
                    Gl.glTranslated(0, 0, -d);

                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, color2);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color2);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess);

                    Glut.glutSolidCylinder((double)(d * 0.05), (double)(d * 0.7), 8, 8);
                    Gl.glTranslated(0, -d * 0.5, d * 0.3);
                    Gl.glRotated(30, 0, 1, 0);

                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, color3);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color3);
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess);

                    Glut.glutSolidCylinder((double)(d * 0.5), (double)(d * 0.1), 4, 1);
                    xold = xold + left * d - right * d;
                    yold = yold + up * d - down * d;

                }
                AnT.Invalidate();

            }
        }
        private void DrawApple2()
        {   
            if (textureIsLoad&&col_tex)      
            {
                Glu.GLUquadric quadr1, quadr2, quadr3;

                // сохраняем состояние матрицы
                Gl.glPushMatrix();

                Gl.glTranslated(xold, yold, -d);
                Gl.glTranslated(left * d - right * d, up * d - down * d, 0); // movement


                //Отрисовка сферы с текстурой:
                quadr1 = Glu.gluNewQuadric();
                Glu.gluQuadricTexture(quadr1, Gl.GL_TRUE);
                // включаем режим текстурирования
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                // включаем режим текстурирования , указывая индификатор mGlTextureObject
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject1);
                Glu.gluSphere(quadr1, d / 2, 32, 32);
                Glu.gluDeleteQuadric(quadr1);
                // отключаем режим текстурирования
                Gl.glDisable(Gl.GL_TEXTURE_2D);

                Gl.glTranslated(0, 0, -d);

                quadr2 = Glu.gluNewQuadric();
                Glu.gluQuadricTexture(quadr2, Gl.GL_TRUE);
                // включаем режим текстурирования
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject2);

                Glu.gluCylinder(quadr2, (double)(d * 0.05), (double)(d * 0.03), (double)(d * 0.7), 8, 8);
                Glu.gluDeleteQuadric(quadr2);
                // отключаем режим текстурирования
                Gl.glDisable(Gl.GL_TEXTURE_2D);

                Gl.glTranslated(d * 0.1, -d * 0.1, d * 0.3);
                Gl.glRotated(30, 0, 1, 0);

                //quadr3 = Glu.gluNewQuadric();
                //Glu.gluQuadricTexture(quadr3, Gl.GL_TRUE);
                // включаем режим текстурирования
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject3);

                // отрисовываем полигон
                Gl.glBegin(Gl.GL_POLYGON);
                // указываем поочередно вершины и текстурные координаты
                Gl.glVertex3d((double)(d * 0.7), (double)(d * 0.7), 0);
                Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glVertex3d((double)(d * 0.7), (double)(d * 0.4), 0);
                Gl.glTexCoord2f(0.7f, 0.0f);
                Gl.glVertex3d((double)(d * -0.2), (double)(d * 0.0), 0);
                Gl.glTexCoord2f(1.0f, 1.0f);
                Gl.glVertex3d((double)(d * -0.1), (double)(d * 0.4), 0);
                Gl.glTexCoord2f(0.0f, 1.0f);
                // завершаем отрисовку
                Gl.glEnd();

                //Glu.gluCylinder(quadr3, (double)(d * 0.5), (double)(d * 0.0), (double)(d * 0.1), 4, 2);
                // отключаем режим текстурирования
                Gl.glDisable(Gl.GL_TEXTURE_2D);



                /*
                // отрисовываем полигон
                Gl.glBegin(Gl.GL_POLYGON);

                // указываем поочередно вершины и текстурные координаты
                Gl.glVertex3d((double)(d * 0.9), (double)(d * 0.9), 0);
                //Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glTexCoord2f(0.1f, 0.7f);
                Gl.glVertex3d((double)(d * 0.8), (double)(d * 0.5), 0);
                //Gl.glTexCoord2f(0.4f, 0.4f);
                Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glVertex3d((double)(d * 0.2),0, 0);
                //Gl.glTexCoord2f(0.9f, 0.3f);
                Gl.glTexCoord2f(0.4f, 0.4f);
                Gl.glVertex3d((double)(d * -0.2),0, 0);
                //Gl.glTexCoord2f(1.0f, 0.7f);
                Gl.glTexCoord2f(0.9f, 0.3f);
                Gl.glVertex3d((double)(d * -0.1), (double)(d * 0.5), 0);
                //Gl.glTexCoord2f(0.7f, 1.0f);
                Gl.glTexCoord2f(1.0f, 0.7f);
                Gl.glVertex3d((double)(d * 0.2), (double)(d * 0.7), 0);
                //Gl.glTexCoord2f(0.1f, 0.7f);
                Gl.glTexCoord2f(0.7f, 1.0f);

                // завершаем отрисовку
                Gl.glEnd();
                 */
                //Glu.gluDeleteQuadric(quadr3);

                xold = xold + left * d - right * d;
                yold = yold + up * d - down * d;

                // возвращаем матрицу
                Gl.glPopMatrix();

            }
            AnT.Invalidate();
        }

        private void placeFruct()  // to genenerate fructs on the map
        {
            
            Random rnd = new Random();
            {
                x_fruit = rnd.Next(0, n-1);
                y_fruit = rnd.Next(0, m-1);
            }while (arr[x_fruit, y_fruit] != 0) ;  //placing apple on a free place
            arr[x_fruit, y_fruit] = 1;
            drawFruct(x_fruit, y_fruit);
        }

        private void drawFruct(int i, int j)
        {
        Gl.glTranslated(i, j, 0);
        Glut.glutSolidSphere(d, 16, 16);
        Gl.glTranslated(-i, -j, 0); 
        }
        private void Snake() //show snake on the screen
        {
            Gl.glClearColor(255, 255, 255, 1);
            // сохраняем состояние матрицы
            Gl.glPushMatrix();

            Gl.glTranslated(xold, yold, 0);
            Gl.glTranslated(left - right, up - down, 0);
            //draw head
            Glut.glutSolidSphere(d/2, 16, 16);
            for (int i = 1; i < snake_size-1; i++)
            {
                xdif = snake_arr[i - 1, 0] - snake_arr[i, 0];
                ydif = snake_arr[i - 1, 1] - snake_arr[i, 1];
                Gl.glTranslated(xdif, ydif, 0);
                Glut.glutSolidSphere(d/2, 16, 16);
            }
            xdif = snake_arr[snake_size-1, 0] - snake_arr[snake_size, 0];
            ydif = snake_arr[snake_size - 1, 1] - snake_arr[snake_size, 1];
            xold += left - right;
            yold += up - down;
            Glut.glutSolidSphere(d/2, 16, 16);

        }

        private void tick()
        {
            for (int i = snake_size; i > 0; --i) // движение змеи. Система остроумна и проста : блок перемешается вперед, а остальные X блоков, на X+1( 2 блок встанет на место 1, 3 на место 2 и т.д...) 
            {
                snake_arr[i, 0] = snake_arr[i - 1, 0];
                snake_arr[i, 1] = snake_arr[i - 1, 1];
            }
            // далее у нас система направлений. 
            if (snake_direction == 0) snake_arr[0, 1] += d;
            else if (snake_direction == 1) snake_arr[0, 1] -= d;
            else if (snake_direction == 2) snake_arr[0, 0] -= d;
            else if (snake_direction == 3) snake_arr[0, 0] += d;

            /*
            if ((int)snake_arr[0, 0] >= 0 && (int)snake_arr[0, 1] >= 0 && (arr[(int)snake_arr[0,0], (int)snake_arr[0,1]] != 0))////
            {
                snake_size++;
                placeFruct();
            }
            /*
            for (int i = 1; i < snake_size; i++)
            {  // с помощью этого цикла мы "обрежем" змею, если она заползет сама на себя 
                if (snake_arr[0, 0] == snake_arr[i,0] && snake_arr[0,1] == snake_arr[i,1])
                {  // проверка координат частей змеи, если X и Y координата головной части равно координате любого 
                    snake_size = i; // другого блока змеи, то задаем ей длину, при которой "откушенная" часть отпадает. 
                }
            }
            */

        }

        // создание текстуры в памяти openGL static 
        private uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            // индетефекатор текстурного объекта
            uint texObject;

            // генерируем текстурный объект
            Gl.glGenTextures(1, out texObject);

            // устанавливаем режим упаковки пикселей
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

            // устанавливаем режим фильтрации и повторения текстуры
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            // создаем RGB или RGBA текстуру
            switch (Format)
            {
                case Gl.GL_RGB:
                    {
                        Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                        break;
                    }

                case Gl.GL_RGBA:
                    {
                        Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                        break;
                    }
            }

            // возвращаем индетефекатор текстурного объекта

            return texObject;
        }

    }
}


