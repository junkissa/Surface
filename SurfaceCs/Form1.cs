using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Tao.FreeGlut;

namespace plane2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			AnT.InitializeContexts();
		}

		private void AnT_Load(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// инициализация Glut 
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			// очитка окна 
			Gl.glClearColor(255, 255, 255, 1);

			// установка порта вывода в соотвествии с размерами элемента anT 
			Gl.glViewport(0, 0, AnT.Width, AnT.Height);


			// настройка проекции 
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			// настройка параметров OpenGL для визуализации 
			Gl.glEnable(Gl.GL_DEPTH_TEST);
 
		}
		static double a = 1, b = 1, c = 1;
		void Draw()
		{

			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glLoadIdentity();
			Gl.glColor3d(205.0/255.0, 92.0/255.0, 92.0/255.0);

			Gl.glPushMatrix();
			Gl.glTranslated(0, 0, -c * 3 - a/2 - b/2);
			Gl.glRotated(90 + Zz, 1, 0, 0);
			Gl.glRotated(Xx, 0, 0, 1);
			double x = 0, y = 0, z = 0;
			double step_big = 0.1, step_lit = 0.03;
			
			Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
			for (double u = 0; u < 2 * Math.PI; u += step_big)
			{
				Gl.glBegin(Gl.GL_POLYGON);
				for (double v = 0; v < 2 * Math.PI; v+=step_lit)
				{
					x = a*Math.Cos(u)*Math.Cos(v) + b*Math.Sin(u)*Math.Sin(v);
					y = a*Math.Cos(u)*Math.Sin(v) - b*Math.Sin(u)*Math.Cos(v);
					z = c*Math.Sin(u);
					Gl.glVertex3d(x, y, z);
				}
				Gl.glEnd();
			}
			for (double v = 0; v < 2 * Math.PI; v += step_big)
			{
				Gl.glBegin(Gl.GL_POLYGON);
				for (double u = 0; u < 2 * Math.PI; u+=step_lit)
				{
					x = a*Math.Cos(u)*Math.Cos(v) + b*Math.Sin(u)*Math.Sin(v);
					y = a*Math.Cos(u)*Math.Sin(v) - b*Math.Sin(u)*Math.Cos(v);
					z = c*Math.Sin(u);
					Gl.glVertex3d(x, y, z);
				}
				Gl.glEnd();
			}

				Gl.glPopMatrix();
			Gl.glFlush();
			AnT.Invalidate();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Draw();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			a = (double)trackBar1.Value/10.0;
			label6.Text = a.ToString();
			Draw();
		}

		private void trackBar2_Scroll(object sender, EventArgs e)
		{
			b = (double)trackBar2.Value/10.0;
			label5.Text = b.ToString();
			Draw();
		}

		private void trackBar3_Scroll(object sender, EventArgs e)
		{
			c = (double)trackBar3.Value/10.0;
			label4.Text = c.ToString();
			Draw();
		}

		double Xx = 0;
		double Zz = 0;
		double rotstep = 5;

		private void AnT_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.W)
			{
				Gl.glPushMatrix();
				Gl.glRotated(rotstep + 90, 0, 0, 1);
				Zz += rotstep;
				Draw();
				Gl.glPopMatrix();
				Gl.glEnd();
			}
			if (e.KeyCode == Keys.Down)
			{
				Zz -= rotstep;
				Draw();
			}
			if (e.KeyCode == Keys.Left)
			{
				Xx += rotstep;
				Draw();
			}
			if (e.KeyCode == Keys.Right)
			{
				Xx -= rotstep;
				Draw();
			}
		}

		

		private void AnT_KeyUp(object sender, KeyEventArgs e)
		{
				if (e.KeyCode == Keys.W)
			{
				Gl.glPushMatrix();
				Gl.glRotated(rotstep + 90, 0, 0, 1);
				Zz += rotstep;
				Draw();
				Gl.glPopMatrix();
				Gl.glEnd();
			}
			if (e.KeyCode == Keys.S)
			{
				Zz -= rotstep;
				Draw();
			}
			if (e.KeyCode == Keys.A)
			{
				Xx += rotstep;
				Draw();
			}
			if (e.KeyCode == Keys.D)
			{
				Xx -= rotstep;
				Draw();
			}
		}
		
		

	}
}
