using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        int xn, yn, xk, yk;
        int thickness = 1;
        const int n = 500;
        bool solid = true;
        /// <summary>
        /// Обычный ЦДА
        /// </summary>
        /// <param name="x1">Начальная координата по X</param>
        /// <param name="y1">Начальная координата по Y</param>
        /// <param name="x2">Конечная координата по X</param>
        /// <param name="y2">Конечная координата по Y</param>
        private void UsualDDA(int x1, int y1, int x2, int y2)
        {
            if (radioButton2.Checked == true)
            {
                if (textBox6.Text == "")
                {
                    MessageBox.Show("Введите толщину");
                    return;
                }
                thickness = int.Parse(textBox6.Text);
            }
            else
            {
                thickness = 1;
            }
            if (radioButton5.Checked == true)
            {
                solid = true;
            }
            else
            {
                solid = false;
            }
            string comboBoxColor = comboBox1.Text;
            Color color = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(comboBoxColor);
            xn = x1;
            yn = y1;
            xk = x2;
            yk = y2;
            double dx = xk - xn;
            double dy = yk - yn;
            double xt = xn;
            double yt = yn;
            int count = 0;
            int gap = thickness * 10;
            for (int i = 1; i <= n; i++)
            {
                if (!solid)
                {
                    if ((count <= gap) && (count >= 0))
                    {
                        Pen myPen = new Pen(color, 1);
                        Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                        g.DrawRectangle(myPen, (int)xt, (int)yt, thickness, thickness);
                        count++;
                    }
                    else if (count <= gap * 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    Pen myPen = new Pen(color, 1);
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                    g.DrawRectangle(myPen, (int)xt, (int)yt, thickness, thickness);
                }
                xt = xt + dx / n;
                yt = yt + dy / n;
            }
        }

        /// <summary>
        /// Несимметричный ЦДА
        /// </summary>
        /// <param name="x1">Начальная координата по X</param>
        /// <param name="y1">Начальная координата по Y</param>
        /// <param name="x2">Конечная координата по X</param>
        /// <param name="y2">Конечная координата по Y</param>
        private void AsymmetricalDDA(int x1, int y1, int x2, int y2)
        {
            if (radioButton2.Checked == true)
            {
                if (textBox6.Text == "")
                {
                    MessageBox.Show("Введите толщину");
                    return;
                }
                thickness = int.Parse(textBox6.Text);
            }
            else
            {
                thickness = 1;
            }
            if (radioButton5.Checked == true)
            {
                solid = true;
            }
            else
            {
                solid = false;
            }
            string comboBoxColor = comboBox1.Text;
            Color color = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(comboBoxColor);
            xn = x1;
            yn = y1;
            xk = x2;
            yk = y2;
            double dx = xk - xn;
            double dy = yk - yn;
            double xt = xn;
            double yt = yn;
            int count = 0;
            int gap = thickness * 10;
            while (xt < xk)
            {
                if (!solid)
                {
                    if ((count <= gap) && (count >= 0))
                    {
                        Pen myPen = new Pen(color, 1);
                        Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                        g.DrawRectangle(myPen, (int)xt, (int)yt, thickness, thickness);
                        count++;
                    }
                    else if (count <= gap * 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    Pen myPen = new Pen(color, 1);
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                    g.DrawRectangle(myPen, (int)xt, (int)yt, thickness, thickness);
                }
                xt = xt + 1.0;
                yt = yt + dy / dx;
            }
        }

        /// <summary>
        ///  Алгоритм Брезенхема для генерации отрезка
        /// </summary>
        /// <param name="x1">Начальная координата по X</param>
        /// <param name="y1">Начальная координата по Y</param>
        /// <param name="x2">Конечная координата по X</param>
        /// <param name="y2">Конечная координата по Y</param>
        private void BrezenhamSegment(int x1, int y1, int x2, int y2)
        {
            if (radioButton2.Checked == true)
            {
                if (textBox6.Text == "")
                {
                    MessageBox.Show("Введите толщину");
                    return;
                }
                thickness = int.Parse(textBox6.Text);
            }
            else
            {
                thickness = 1;
            }
            if (radioButton5.Checked == true)
            {
                solid = true;
            }
            else
            {
                solid = false;
            }
            string comboBoxColor = comboBox1.Text;
            Color color = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(comboBoxColor);
            double dx = Math.Abs(x2 - x1);
            double dy = Math.Abs(y2 - y1);
            int signX, signY;
            if (x1 < x2)
            {
                signX = 1;
            }
            else
            {
                signX = -1;
            }
            if (y1 < y2)
            {
                signY = 1;
            }
            else
            {
                signY = -1;
            }
            double error = dx-dy;
            int count = 0;
            int gap = thickness * 10;
            Pen myPen = new Pen(color, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.DrawRectangle(myPen, (int)x2, (int)y2, thickness, thickness);
            while (x1 != x2 || y1 != y2)
            {
                if (!solid)
                {
                    if ((count <= gap) && (count >= 0))
                    {
                        g.DrawRectangle(myPen, (int)x1, (int)y1, thickness, thickness);
                        count++;
                    }
                    else if (count <= gap * 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    g.DrawRectangle(myPen, (int)x1, (int)y1, thickness, thickness);
                }
                double error2 = error * 2;
                if (error2 > -dy)
                {
                    error = error - dy;
                    x1 = x1 + signX;
                }
                if (error2 < dx)
                {
                    error = error + dx;
                    y1 = y1 + signY;
                }
            }
        }

        /// <summary>
        /// Алгоритм Брезенхема для генерации окружности
        /// </summary>
        /// <param name="x0">Центр окружности по X</param>
        /// <param name="y0">Центр окружности по Y</param>
        /// <param name="radius">Радиус окружности, который задается тем, как далеко отодвинули курсор</param>
        private void BrezenhamCircle(int x0, int y0, int radius)
        {
            if (radioButton2.Checked == true)
            {
                if (textBox6.Text == "")
                {
                    MessageBox.Show("Введите толщину");
                    return;
                }
                thickness = int.Parse(textBox6.Text);
            }
            else
            {
                thickness = 1;
            }
            if (radioButton5.Checked == true)
            {
                solid = true;
            }
            else
            {
                solid = false;
            }
            string comboBoxColor = comboBox1.Text;
            Color color = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(comboBoxColor);
            int x = 0;
            int y = radius;
            int delta = 1 - 2 * radius;
            int error;
            int count = 0;
            int gap = thickness * 10;
            while (y >= 0)
            {
                if (!solid)
                {
                    if ((count <= gap) && (count >= 0))
                    {
                        Pen myPen = new Pen(color, 1);
                        Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                        g.DrawRectangle(myPen, (int)(x0 + x), (int)(y0 + y), thickness, thickness);
                        g.DrawRectangle(myPen, (int)(x0 + x), (int)(y0 - y), thickness, thickness);
                        g.DrawRectangle(myPen, (int)(x0 - x), (int)(y0 + y), thickness, thickness);
                        g.DrawRectangle(myPen, (int)(x0 - x), (int)(y0 - y), thickness, thickness);
                        count++;
                    }
                    else if (count <= gap * 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    Pen myPen = new Pen(color, 1);
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                    g.DrawRectangle(myPen, (int)(x0 + x), (int)(y0 + y), thickness, thickness);
                    g.DrawRectangle(myPen, (int)(x0 + x), (int)(y0 - y), thickness, thickness);
                    g.DrawRectangle(myPen, (int)(x0 - x), (int)(y0 + y), thickness, thickness);
                    g.DrawRectangle(myPen, (int)(x0 - x), (int)(y0 - y), thickness, thickness);
                    count++;
                }
                error = 2 * (delta + y) - 1;
                if (delta < 0 && error <= 0)
                {
                    x++;
                    delta = delta + 2 * x + 1;
                    continue;
                }
                error = 2 * (delta - x) - 1;
                if (delta > 0 && error > 0)
                {
                    y--;
                    delta = delta + 1 - 2 * y;
                    continue;
                }
                x++;
                delta = delta + 2 * (x - y);
                y--;
            }
        }

        /// <summary>
        /// Очистка экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        /// <summary>
        /// Построение отрезка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "")|| (textBox3.Text == "") || (textBox4.Text == ""))
            {
                MessageBox.Show("Введите все координаты отрезка");
                return;
            }
            xn= int.Parse(textBox1.Text);
            yn = int.Parse(textBox2.Text);
            xk = int.Parse(textBox3.Text);
            yk = int.Parse(textBox4.Text);            
            if ((xn>500) || (yn > 500) || (xk > 500) || (yk > 500))
            {
                MessageBox.Show("Координаты за пределом области холста, введите координаты меньше 500");
                return;
            }
            if (radioButton1.Checked == true)
            {
                UsualDDA(xn, yn, xk, yk);
            }
            if (radioButton6.Checked == true)
            {
                AsymmetricalDDA(xn, yn, xk, yk);
            }
            if (radioButton7.Checked == true)
            {
                BrezenhamSegment(xn, yn, xk, yk);
            }
            if (radioButton8.Checked == true)
            {
                MessageBox.Show("Переключите алгоритм на любой, из указанных выше");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            MessageBox.Show("Вам нельзя пытаться изменить имя цвета");
            comboBox1.SelectedIndex = 0;
            return;
        }

        /// <summary>
        /// Построение заранее заданного отрезка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            xn = 10;
            yn = 25;
            xk = 310;
            yk = 25;
            UsualDDA(10, 25, 310, 25);
        }

        /// <summary>
        /// Построение заранее заданного прямоугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            UsualDDA(10, 50, 310, 50);
            UsualDDA(310, 50, 310, 100);
            UsualDDA(310, 100, 10, 100);
            UsualDDA(10, 100, 10, 50);
        }

        /// <summary>
        /// Построение заранее заданного квадрата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            UsualDDA(10, 125, 60, 125);
            UsualDDA(60, 125, 60, 175);
            UsualDDA(60, 175, 10, 175);
            UsualDDA(10, 175, 10, 125);
        }

        /// <summary>
        /// Построение заранее заданного треугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            UsualDDA(10, 250, 50, 200);
            UsualDDA(50, 200, 90, 250);
            UsualDDA(90, 250, 10, 250);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UsualDDA(20, 20, 80, 40);
            UsualDDA(80, 40, 35, 100);
            UsualDDA(35, 100, 35, 75);
            UsualDDA(35, 75, 10, 100);
            UsualDDA(10, 100, 20, 20);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            xk = e.X; 
            yk = e.Y;
            if (radioButton1.Checked == true)
            {
                UsualDDA(xn, yn, xk, yk);
            }
            if (radioButton6.Checked == true)
            {
                AsymmetricalDDA(xn, yn, xk, yk);
            }
            if (radioButton7.Checked == true)
            {
                BrezenhamSegment(xn, yn, xk, yk);
            }
            if (radioButton8.Checked == true)
            {
                int radius = Convert.ToInt32(Math.Sqrt((xn - xk) * (xn - xk) + (yn - yk) * (yn - yk)));
                BrezenhamCircle(xn, yn, radius);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            xn = e.X;
            yn = e.Y;
        }
    }
}
